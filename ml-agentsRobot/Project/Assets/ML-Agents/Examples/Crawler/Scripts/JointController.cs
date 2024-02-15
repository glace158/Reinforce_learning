using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.MLAgentsRobot{
    [System.Serializable]
    public class RobotPart{
        public ArticulationBody joint;
        [HideInInspector] public float startingPos;

        public float maxAngularVelocity;

        public void Reset(RobotPart rp){
            var rp_drive = rp.joint.xDrive;
            
            rp_drive.target = rp.startingPos;

            rp.joint.xDrive = rp_drive;
        }

        public void SetJointTarget(float target, float damping, float stiffness, float velocity){
            var rp_drive = joint.xDrive;
            
            rp_drive.target = target;
            rp_drive.damping = damping;
            rp_drive.stiffness = stiffness;
            rp_drive.targetVelocity = velocity;

            joint.xDrive = rp_drive;
        }
    }

    public class JointController : MonoBehaviour
    {
        public float maxJointSpring;
        public float jointDampen;
        public float maxJointForceLimit;

        public float maxAngularVelocity;
        [HideInInspector] public Dictionary<Transform, RobotPart> robotPartsDict = new Dictionary<Transform, RobotPart>();
        [HideInInspector] public List<RobotPart> robotPartsList = new List<RobotPart>();
        
        public void SetupRobotPart(Transform t){
            var rp = new RobotPart{
                joint = t.GetComponent<ArticulationBody>(),
                startingPos = 0.0f
            };
            rp.maxAngularVelocity = maxAngularVelocity;

            robotPartsDict.Add(t, rp);
            robotPartsList.Add(rp);
        }

        public void GetCurrentJointInfo(){

        }
    }
}