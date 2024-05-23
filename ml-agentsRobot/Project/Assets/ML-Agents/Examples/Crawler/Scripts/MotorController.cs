using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgentsExamples;
using Unity.MLAgents;

namespace Unity.MLAgentsRobot{

    [System.Serializable]
    public class RobotPart{
        public ArticulationBody motor;

        public int startingAngle;

        private float lowerLimit = 0f;
        private float upperLimit = 0f;

        [Header("Ground & Target Contact")]
        [Space(10)]
        public GroundContact groundContact;

        public void Reset(RobotPart m){
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

    public class MotorController : MonoBehaviour
    {

        [HideInInspector] public Dictionary<Transform, RobotPart> motorsDict = new Dictionary<Transform, RobotPart>();
        [HideInInspector] public Dictionary<Transform, RobotPart> linkDict = new Dictionary<Transform, RobotPart>();
        
        public void SetupMotor(Transform t){
            var mo = new RobotPart
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
            var mo = new RobotPart
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

            linkDict.Add(t, mo);
        }

        public List<float> GetVelocity(){
            List<float> velocityList = new List<float>();
            foreach (var motor in motorsDict.Values)
            {
                velocityList.Add(motor.motor.jointVelocity[0]);
            }
            return velocityList;
        }

        public List<float> GetTorque(){
            List<float> torqueList = new List<float>();
            foreach (var motor in motorsDict.Values)
            {
                torqueList.Add(motor.motor.driveForce[0]);
            }
            return torqueList;
        }

        public List<float> GetPosition(){
            List<float> positionList = new List<float>();
            foreach (var motor in motorsDict.Values)
            {
                positionList.Add((motor.motor.jointPosition[0] * 180 / Mathf.PI));
            }
            return positionList;
        }
    }
}
