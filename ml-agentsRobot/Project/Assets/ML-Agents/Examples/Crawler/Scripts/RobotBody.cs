using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.MLAgentsRobot{
    public class RobotBody : MonoBehaviour
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

        public Transform m_OrientationCube;
    }
}