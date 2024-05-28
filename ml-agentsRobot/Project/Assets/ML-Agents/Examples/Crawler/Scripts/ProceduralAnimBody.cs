using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.MLAgentsRobot{
    public class ProceduralAnimBody : MonoBehaviour
    {
        public Transform root;
        public Transform animModel;
        public Transform OrientationCube;

        public Transform FRFoot;
        public Transform FLFoot;
        public Transform RRFoot;
        public Transform RLFoot;

        public Vector3 GetInitPosition(Vector3 offSet){
            return root.position + root.TransformDirection(offSet);
        }

        public Vector3 GetRootPosition(Vector3 rootOffset){
            return new Vector3(root.position.x, root.position.y, root.position.z) + root.TransformDirection(rootOffset);
        }

        public Quaternion GetRootRotation(){
            return Quaternion.Euler(animModel.rotation.eulerAngles.x,OrientationCube.rotation.eulerAngles.y,animModel.rotation.eulerAngles.z);
        }

        public Vector2 GetOrientationRotation(){
            return new Vector2(OrientationCube.forward.x, OrientationCube.forward.z);
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