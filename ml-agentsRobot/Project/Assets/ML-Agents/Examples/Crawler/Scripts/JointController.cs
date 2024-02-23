using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Unity.MLAgentsExamples;
using Unity.MLAgents;
using Unity.Robotics.UrdfImporter;

namespace Unity.MLAgentsRobot{
    [System.Serializable]
    public class RobotPart{
        public ArticulationBody joint;

        //public UrdfJointRevolute urdf_joint;
        public Transform trans;
        [HideInInspector] public float startingPos;

        public float maxAngularVelocity;

        public GroundContact groundContact;

        [HideInInspector] public float currentStrength;

        public void Reset(RobotPart rp){
            var rp_drive = rp.joint.xDrive;
            
            rp_drive.target = rp.startingPos;

            rp.joint.xDrive = rp_drive;

            if (rp.groundContact)
            {
                rp.groundContact.touchingGround = false;
            }
        }

        public void SetJointTarget(float target, float damping, float stiffness, float velocity){
            var rp_drive = joint.xDrive;
            
            rp_drive.target = target;
            rp_drive.damping = damping;
            rp_drive.stiffness = stiffness;
            rp_drive.targetVelocity = velocity;

            joint.xDrive = rp_drive;

            //currentStrength = urdf_joint.Get
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
                trans = t,
                joint = t.GetComponent<ArticulationBody>(),
                startingPos = 0.0f
            };
            rp.maxAngularVelocity = maxAngularVelocity;

            rp.groundContact = t.GetComponent<GroundContact>();
            if(!rp.groundContact){
                rp.groundContact = t.gameObject.AddComponent<GroundContact>();
                rp.groundContact.agent = gameObject.GetComponent<Agent>();
            }
            else
            {
                rp.groundContact.agent = gameObject.GetComponent<Agent>();
            }

            robotPartsDict.Add(t, rp);
            robotPartsList.Add(rp);
        }

        public void GetCurrentJointInfo(){

        }
    }
}