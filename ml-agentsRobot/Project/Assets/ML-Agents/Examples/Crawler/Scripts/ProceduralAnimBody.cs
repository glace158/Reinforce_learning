using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.MLAgentsRobot{

    public enum AXIS{ X, Y, Z }

    [System.Serializable]
    public class AnimJoint{
        public Transform t;
        public float offset = 0f;
        public int axis;
        
        public bool isInverse = false;

        private int val = 1;

        public AnimJoint(Transform t, int axis, bool isInverse){
            this.t = t;
            this.axis = axis;
            this.isInverse = isInverse;
            val = isInverse ? -1 : 1;
            
            
            
            switch (axis){
                case 0:
                    this.offset = t.rotation.eulerAngles.x;
                    break;
                case 1:
                    this.offset = t.rotation.eulerAngles.y;
                    break;
                case 2:
                    this.offset = t.rotation.eulerAngles.z;
                    break;
            }

            
        }
        
        public float GetAngle(){
            switch (axis){
                case 0:
                    return (t.rotation.eulerAngles.x - offset) * val;
                case 1:
                    return (t.rotation.eulerAngles.y - offset) * val;
                case 2:
                    return (t.rotation.eulerAngles.z - offset) * val;
                default:
                    return 0f;
            }
        }
    }

    public class ProceduralAnimBody : MonoBehaviour
    {
        public Transform root;
        public Transform animModel;
        public Transform OrientationCube;

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

        private List<AnimJoint> jointList = new List<AnimJoint>();

        private float FLOffset;
        private float RLOffset;

        Vector3 RLaxis = Vector3.zero;
        Vector3 FLaxis = Vector3.zero;

        private void Start(){
            initSet();
        }
        
        public void initSet(){
            jointList.Clear();
            SetupJoint(FRHip, (int)AXIS.Z,false);
            SetupJoint(FRLegUpper, (int)AXIS.X,true);
            SetupJoint(FRLegLower, (int)AXIS.X,true);

            SetupJoint(FLHip, (int)AXIS.X, true);
            SetupJoint(FLLegUpper, (int)AXIS.X, true);
            SetupJoint(FLLegLower, (int)AXIS.X,true);

            SetupJoint(RRHip, (int)AXIS.Z,false);
            SetupJoint(RRLegUpper, (int)AXIS.X,true);
            SetupJoint(RRLegLower, (int)AXIS.X,true);

            SetupJoint(RLHip, (int)AXIS.X, true);
            SetupJoint(RLLegUpper, (int)AXIS.X, true);
            SetupJoint(RLLegLower, (int)AXIS.X, true);

            FLHip.rotation.ToAngleAxis( out FLOffset, out FLaxis);
            RLHip.rotation.ToAngleAxis( out RLOffset, out RLaxis);

            FLaxis = FLHip.right;
            RLaxis = RLHip.right;
        }

        public void SetOrientationCube(Transform t){
            OrientationCube = t;
        }

        void SetupJoint(Transform t, int axis, bool isInverse){
            var joint = new AnimJoint(t, axis, isInverse);
            jointList.Add(joint);
        }
        
        public float GetJointAngle(int index){            
            var angle = jointList[index].GetAngle();

            if(jointList[index].t == FLHip){
                var a = 0f;
                FLHip.rotation.ToAngleAxis( out a, out FLaxis);
                a -= FLOffset;
                angle = (0 < a) ? angle : -angle;
            }
            if(jointList[index].t == RLHip){
                var a = 0f;
                RLHip.rotation.ToAngleAxis( out a, out RLaxis);
                a -= RLOffset;
                angle = (0 < a) ? -angle : angle;
            }

            return angle;
        }
        

        public Vector3 GetInitPosition(Vector3 offSet){
            return root.position + root.TransformDirection(offSet);
        }

        public Vector3 GetRootPosition(Vector3 rootOffset){
            return new Vector3(root.position.x, root.position.y, root.position.z) + root.TransformDirection(rootOffset);
        }

        public Vector3 GetRootRotation(){
            return new Vector3(animModel.rotation.eulerAngles.x,OrientationCube.rotation.eulerAngles.y,animModel.rotation.eulerAngles.z);
        }

        public Vector2 GetOrientationRotation(){
            return new Vector2(OrientationCube.forward.x, OrientationCube.forward.z);
        }

        public Vector3 GetRootForward(){
            return root.up;
        }

        public Vector3 GetFootPosition(int num, Vector3 footOffset){
            switch (num){
                case 0:
                    return new Vector3(FLFoot.position.x,FLFoot.position.y,FLFoot.position.z) + footOffset;
                case 1:
                    return new Vector3(RRFoot.position.x,RRFoot.position.y,RRFoot.position.z) + footOffset;
                case 2:
                    return new Vector3(FRFoot.position.x,FRFoot.position.y,FRFoot.position.z) + footOffset;
                case 3:
                    return new Vector3(RLFoot.position.x,RLFoot.position.y,RLFoot.position.z) + footOffset;
            }
            return new Vector3(0f,0f,0f);  
        }
    }
}