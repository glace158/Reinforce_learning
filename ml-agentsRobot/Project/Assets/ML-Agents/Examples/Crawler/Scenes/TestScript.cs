using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform body;
    // Start is called before the first frame update
    public ArticulationBody joint;
    public int startingPos = 45;

    float timer = 0;
    int waitingTime = 3;

    void Start()
    {
        var rp_drive = joint.xDrive;
            
            rp_drive.target = startingPos;

            joint.xDrive = rp_drive;
    }

    // Update is called once per frame
    void Update()
    {
        jointfunc();
    }

    void jointfunc(){
        timer += Time.deltaTime;
        Debug.Log("pos: " + (joint.jointPosition[0] * 180 / Mathf.PI).ToString()+ " vel: " + joint.jointVelocity[0].ToString() + " tor: " + joint.driveForce[0].ToString());
        if(timer > waitingTime)
        {
        var rp_drive = joint.xDrive;
            startingPos *= -1;
            rp_drive.target = startingPos;

            joint.xDrive = rp_drive;
        timer = 0;
        }
    }
}
