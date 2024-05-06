using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.MLAgentsRobot{

    [System.Serializable]
    public class Motor{
        public ArticulationBody motor;

        public int startingAngle;


        public void Reset(){
            var mo_drive = motor.xDrive;
            
            mo_drive.target = startingAngle;

            motor.xDrive = mo_drive;
        }

        public void SetMotorTarget(float targetAngle){
            var mo_drive = motor.xDrive;

            mo_drive.target = targetAngle;

            motor.xDrive = mo_drive;
        }
    }

    public class MotorController : MonoBehaviour
    {

        [HideInInspector] public Dictionary<Transform, Motor> motorsDict = new Dictionary<Transform, Motor>();
        
        public void SetupMotor(Transform t){
            var mo = new Motor
            {
                motor = t.GetComponent<ArticulationBody>(),
                startingAngle = 0
            };

            motorsDict.Add(t, mo);
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
