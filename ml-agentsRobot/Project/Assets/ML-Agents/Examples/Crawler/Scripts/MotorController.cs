using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgentsExamples;
using Unity.MLAgents;
using System.Linq;
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

        public void SetJointAngle(float angle){
            var mo_drive = motor.xDrive;

            mo_drive.target = angle;

            motor.xDrive = mo_drive;
        }

        public float GetTargetValue(){
            return Mathf.InverseLerp(lowerLimit, upperLimit, motor.jointPosition[0] * 180 / Mathf.PI);
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
        //[Header("angle")]
        //[Range(-1f, 1f)]
        //[SerializeField]
        //private float Tangle = 0f;

        //public int targetJoint = 0;

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
        
        private List<float> previousMotorRotationList = new List<float>();
        
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
            SetupLink(FLFoot);
            SetupLink(RRFoot);
            SetupLink(FRFoot);
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

            previousMotorRotationList.Add(0f);

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

            mo.Reset(mo);
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

        public float GetJointVelocity(int index){
            List<RobotMotor> velocityList = motorsDict.Values.ToList();
            var x = velocityList[index].motor.jointPosition[0] * 180 / Mathf.PI;
            float deltaRotation = x - previousMotorRotationList[index];
            previousMotorRotationList[index] = x;

            deltaRotation *= Mathf.Deg2Rad;
            
            return  Mathf.Abs((1.0f / Time.deltaTime) * deltaRotation);
        }

        public float GetJointTorque(int index){
            List<RobotMotor> torqueList = motorsDict.Values.ToList();

            return torqueList[index].motor.driveForce[0];
        }

        public float GetMotorAngles(int index){
            List<RobotMotor> positionList = motorsDict.Values.ToList();
            return positionList[index].GetTargetValue();
        }

        public float GetJointAngles (int index){
            List<RobotMotor> positionList = motorsDict.Values.ToList();

            return positionList[index].motor.jointPosition[0] * 180 / Mathf.PI;
        }

        public Vector3 GetRootRotation(){
            var x = bodyLink.rotation.eulerAngles.x;
            var y = bodyLink.rotation.eulerAngles.y;
            var z = bodyLink.rotation.eulerAngles.z;
            x = x -180.0f > 0f ? (x -180.0f) -180f : (x -180.0f) + 180f;
            y = y -180.0f > 0f ? (y -180.0f) -180f : (y -180.0f) + 180f;
            z = z -180.0f > 0f ? (z -180.0f) -180f : (z -180.0f) + 180f;
            return new Vector3(x, y, z);
        }

        public Vector3 GetRootPosition(){
            return bodyLink.position;
        }

        public Vector3 GetRootForward(){
            return bodyLink.forward;
        }

        public bool GetFootContact(int index){
            List<RobotLink> foots = linkDict.Values.ToList();
            return foots[index + 1].groundContact.touchingGround;
        }

        public Vector3 GetFootPosition(int index){
            List<RobotLink> foots = linkDict.Values.ToList();
            return foots[index + 1].link.position;
        }
        
        public void SetMotorAngle(int index, float angle){
            List<RobotMotor> moList = motorsDict.Values.ToList();
            moList[index].SetMotorTarget(angle);
        }

        public void SetJointAngle(int index, float angle){
            List<RobotMotor> moList = motorsDict.Values.ToList();
            moList[index].SetJointAngle(angle);
        }

        public Vector2 GetOrientationRotation(){
            return new Vector2(m_OrientationCube.forward.x, m_OrientationCube.forward.z);
        }

        void OrientationCubeUpdate(){
            m_OrientationCube.rotation = Quaternion.Euler(0f, bodyLink.rotation.eulerAngles.y, 0f);
        }

        void FixedUpdate(){
            OrientationCubeUpdate();
            //linkDict[bodyLink].articulationLink.AddForce((bodyLink.right + bodyLink.up) * 100f, ForceMode.Force);
            //SetMotorAngle(targetJoint, Tangle);
        }
    }
}
