using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform t_body;

    public ArticulationBody body;
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
        
        //Debug.Log(body.transform.rotation.eulerAngles.x - 180);
        //body.AddForce(new Vector3(200f,0f,0f));
        //checkvel();
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {
        //jointfunc();
         
        //rotFunc();
        timer = 0;
        }
    }

    void checkvel(){
        Debug.Log("liner vel: " + body.velocity); 
    }

    void rotFunc(){
        body.TeleportRoot( new Vector3(0f,1f,0f), Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
        //t_body.rotation = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
        
    }
    void jointfunc(){
        
        //Debug.Log("pos: " + (joint.jointPosition[0] * 180 / Mathf.PI).ToString()+ " vel: " + joint.jointVelocity[0].ToString() + " tor: " + joint.driveForce[0].ToString());
        
        var rp_drive = joint.xDrive;
            startingPos *= -1;
            rp_drive.target = startingPos;

            joint.xDrive = rp_drive;
        
    }
}
