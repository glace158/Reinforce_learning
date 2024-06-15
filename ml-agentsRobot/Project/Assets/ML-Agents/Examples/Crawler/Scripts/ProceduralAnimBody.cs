using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.MLAgentsRobot{

    public enum AXIS{ X, Y, Z }

    [System.Serializable]
    public class AnimJoint{
        public Transform t;
        public Vector3 offset;
        public Vector3 axis;

        float previsousAngle; //전 프레임의 로테이션 값

        public AnimJoint(Transform t, Vector3 axis){
            this.t = t;
            this.axis = axis;
            
            this.offset = UnityEditor.TransformUtils.GetInspectorRotation(t);
        }
        
        public float GetAngle(){
            Vector3 axisAngle = (Vector3.Scale(UnityEditor.TransformUtils.GetInspectorRotation(t), axis) - Vector3.Scale(offset, axis));
            
            if(Mathf.Abs(axis.x) == 1){//x
                return axisAngle.x;
            }
            else if(Mathf.Abs(axis.y) == 1){//y
                return axisAngle.y;
            }
            else if(Mathf.Abs(axis.z) == 1){//z
                return axisAngle.z;
            }
            else{
                return -1f;
            }
        }

        public float GetAngularVelocity(){
            var axisAngle = UnityEditor.TransformUtils.GetInspectorRotation(t);
            var angle = -1f;
            if(Mathf.Abs(axis.x) == 1){//x
                angle = axisAngle.x;
            }
            else if(Mathf.Abs(axis.y) == 1){//y
                angle = axisAngle.y;
            }
            else if(Mathf.Abs(axis.z) == 1){//z
                angle = axisAngle.z;
            }

            float deltaRotation = angle - previsousAngle;
            previsousAngle = angle;

            
            //각도에서 라디안으로 변환
            deltaRotation *= Mathf.Deg2Rad;

            float Velocity = (1.0f / Time.deltaTime) * deltaRotation;

            //각속도 반환
            return Mathf.Abs(Velocity);
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
        private Vector3 m_LastPosition;

        private void Start(){
            initSet();
        }
        
        public void initSet(){
            jointList.Clear();
            SetupJoint(FRHip, Vector3.right);
            SetupJoint(FRLegUpper, Vector3.forward);
            SetupJoint(FRLegLower, Vector3.left);

            SetupJoint(FLHip, Vector3.left);
            SetupJoint(FLLegUpper, Vector3.back);
            SetupJoint(FLLegLower, Vector3.left);

            SetupJoint(RRHip, Vector3.right);
            SetupJoint(RRLegUpper, Vector3.forward);
            SetupJoint(RRLegLower, Vector3.left);

            SetupJoint(RLHip, Vector3.left);
            SetupJoint(RLLegUpper, Vector3.back);
            SetupJoint(RLLegLower, Vector3.left);
        }

        public void SetOrientationCube(Transform t){
            OrientationCube = t;
        }

        void SetupJoint(Transform t, Vector3 axis){
            var joint = new AnimJoint(t, axis);
            jointList.Add(joint);
        }
        
        public float GetJointAngle(int index){            
            return jointList[index].GetAngle();
        }

        public float GetJointVelocity(int index){
            return jointList[index].GetAngularVelocity();
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

        public float GetRootSpeed()
        {
            float speed = (((root.position - m_LastPosition).magnitude) / Time.deltaTime);
            m_LastPosition = root.position;

            return speed;
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
                    return FLFoot.position + footOffset;
                case 1:
                    return RRFoot.position + footOffset;
                case 2:
                    return FRFoot.position + footOffset;
                case 3:
                    return RLFoot.position + footOffset;
            }
            return new Vector3(0f,0f,0f);  
        }
    }
}