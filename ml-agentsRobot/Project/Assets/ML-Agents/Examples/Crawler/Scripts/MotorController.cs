using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgentsExamples;
using Unity.MLAgents;

namespace Unity.MLAgentsRobot{

    [System.Serializable]
    public class RobotMotor{
        public ArticulationBody motor;

        public int startingAngle;

        private float lowerLimit = 0f;
        private float upperLimit = 0f;

        public GroundContact groundContact;

        public void Reset(RobotMotor m){
            var mo_drive = m.motor.xDrive;
            lowerLimit = mo_drive.lowerLimit;
            upperLimit = mo_drive.upperLimit;
            
            mo_drive.target = startingAngle;

            m.motor.xDrive = mo_drive;

            if (m.groundContact)
            {
                m.groundContact.touchingGround = false;
            }
        }

        public void SetMotorTarget(float targetAngle){
            float value = (targetAngle + 1f) * 0.5f;

            var rot = Mathf.Lerp(lowerLimit, upperLimit, value);

            var mo_drive = motor.xDrive;

            mo_drive.target = rot;

            motor.xDrive = mo_drive;
        }
    }

    [System.Serializable]
    public class RobotLink{
        public Transform link;

        public ArticulationBody articulationLink;

        public GroundContact groundContact;

        public void Reset(RobotLink l){
            if (l.groundContact)
            {
                l.groundContact.touchingGround = false;
            }
        }
        
        public void SetRootPosition(Vector3 position, Quaternion rotation){
            articulationLink.TeleportRoot(position, rotation);
        }
    }

    public class MotorController : MonoBehaviour
    {

        [Header("Body Parts")][Space(12)] 
        public Transform bodyLink;
        public Transform FRHip;
        public Transform FRLegUpper;
        public Transform FRLegLower;
        public Transform FRFoot;
        public Transform FLHip;
        public Transform FLLegUpper;
        public Transform FLLegLower;
        public Transform FLFoot;
        public Transform RRHip;
        public Transform RRLegUpper;
        public Transform RRLegLower;
        public Transform RRFoot;
        public Transform RLHip;
        public Transform RLLegUpper;
        public Transform RLLegLower;
        public Transform RLFoot;

        [Header("Robot Config")][Space(12)] 
        public float defaultHeight = 0.85f;
        public Transform m_OrientationCube;

        [HideInInspector] public Dictionary<Transform, RobotMotor> motorsDict = new Dictionary<Transform, RobotMotor>();
        [HideInInspector] public Dictionary<Transform, RobotLink> linkDict = new Dictionary<Transform, RobotLink>();
        
        private void Awake() {
            SetupMotor(FRHip);
            SetupMotor(FRLegUpper);
            SetupMotor(FRLegLower);
            SetupMotor(FLHip);
            SetupMotor(FLLegUpper);
            SetupMotor(FLLegLower);
            SetupMotor(RRHip);
            SetupMotor(RRLegUpper);
            SetupMotor(RRLegLower);
            SetupMotor(RLHip);
            SetupMotor(RLLegUpper);
            SetupMotor(RLLegLower);

            SetupLink(bodyLink);
            SetupLink(FRFoot);
            SetupLink(FLFoot);
            SetupLink(RRFoot);
            SetupLink(RLFoot);
        }

        public void RobotReset(Vector3 postion, Quaternion rotation){
            foreach (var motor in motorsDict.Values)
            {
                motor.Reset(motor);
            }

            foreach (var link in linkDict.Values){
                link.Reset(link);
            }

            transform.position = new Vector3(0, defaultHeight,0);
            
            MoveRoot(bodyLink, postion, rotation); 
        }

        public void SetupMotor(Transform t){
            var mo = new RobotMotor
            {
                motor = t.GetComponent<ArticulationBody>(),
                startingAngle = 0
            };

            mo.groundContact = t.GetComponent<GroundContact>();
            if (!mo.groundContact)
            {
                mo.groundContact = t.gameObject.AddComponent<GroundContact>();
                mo.groundContact.agent = gameObject.GetComponent<Agent>();
            }
            else
            {
                mo.groundContact.agent = gameObject.GetComponent<Agent>();
            }

            motorsDict.Add(t, mo);
        }

        public void SetupLink(Transform t){
            var li = new RobotLink
            {
                link = t,
                articulationLink = t.GetComponent<ArticulationBody>()
            };

            li.groundContact = t.GetComponent<GroundContact>();
            if (!li.groundContact)
            {
                li.groundContact = t.gameObject.AddComponent<GroundContact>();
                li.groundContact.agent = gameObject.GetComponent<Agent>();
            }
            else
            {
                li.groundContact.agent = gameObject.GetComponent<Agent>();
            }

            linkDict.Add(t, li);
        }

        public void MoveRoot(Transform t, Vector3 position, Quaternion rotation){
            linkDict[t].SetRootPosition(position, rotation);
        }

        public List<float> GetJointVelocity(){
            List<float> velocityList = new List<float>();
            foreach (var motor in motorsDict.Values)
            {
                velocityList.Add(motor.motor.jointVelocity[0]);
            }
            return velocityList;
        }

        public List<float> GetJointTorque(){
            List<float> torqueList = new List<float>();
            foreach (var motor in motorsDict.Values)
            {
                torqueList.Add(motor.motor.driveForce[0]);
            }
            return torqueList;
        }

        public List<float> GetJointAngles (){
            List<float> positionList = new List<float>();
            foreach (var motor in motorsDict.Values)
            {
                positionList.Add((motor.motor.jointPosition[0] * 180 / Mathf.PI));
            }
            return positionList;
        }

        public Vector3 GetRootRotation(){
            return bodyLink.rotation.eulerAngles;
        }

        public Vector3 GetRootPosition(){
            return bodyLink.position;
        }

        public bool GetFootContact(int index){
            List<RobotLink> foots = new List<RobotLink>();
            foreach (var link in linkDict.Values){
                foots.Add(link);
            }
            return foots[index + 1].groundContact;
        }

        public Vector3 GetFootPosition(int index){
            List<RobotLink> foots = new List<RobotLink>();
            foreach (var link in linkDict.Values){
                foots.Add(link);
            }
            return foots[index + 1].link.position;
        }
        
        public void SetMotorAngle(int index, float angle){
            List<RobotMotor> moList = new List<RobotMotor> (motorsDict.Values);
            moList[index].SetMotorTarget(angle);
        }

        public Vector2 GetOrientationRotation(){
            return new Vector2(m_OrientationCube.forward.x, m_OrientationCube.forward.z);
        }

        void OrientationCubeUpdate(){
            m_OrientationCube.rotation = Quaternion.Euler(0f, bodyLink.rotation.eulerAngles.y, 0f);
        }

        void FixedUpdate(){
            OrientationCubeUpdate();    
        }
    }
}
