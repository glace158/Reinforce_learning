using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointController : MonoBehaviour
{
    [HideInInspector] public Vector3 startingPos;
    [HideInInspector] public Quaternion startingRot;

    public float maxJointSpring;
    public float jointDampen;
    public float maxJointForceLimit;

    const float k_MaxAngularVelocity = 50.0f;

    public void move(){
        /*
        var RR_HipDrive = RR_Hip.xDrive;
        
        RR_HipDrive.target = -45.0f;
        RR_HipDrive.damping = 1f;

        RR_Hip.xDrive = RR_HipDrive;
        */
    }
}
