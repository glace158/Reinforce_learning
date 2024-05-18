using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform t_body;
    public Transform t_foot;

    public Transform player;
    public Transform m_body;
    public Transform m_foot;

    public Transform orientationCube;
    public ArticulationBody body;
    public ArticulationBody foot;
    // Start is called before the first frame update
    public ArticulationBody joint;
    public int startingPos = 45;


    float timer = 0;
    int waitingTime = 3;

    void Start()
    {
        rotFunc();
        var rp_drive = joint.xDrive;
            
            rp_drive.target = startingPos;

            joint.xDrive = rp_drive;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.TransformDirection(t_body.position));
        //Debug.Log(Vector3.Distance(new Vector3(m_body.position.x, m_body.position.y+0.01f, m_body.position.z + 0.05f),t_body.position));
        //Debug.Log(m_foot.position-t_foot.position);
        //Debug.Log(m_body.TransformDirection(new Vector3(0f, 0.05f, 0f)));
        //Debug.Log(Vector3.Distance(new Vector3(m_foot.position.x, m_foot.position.y-0.02f, m_foot.position.z),t_foot.position));
        Debug.Log(GetAngle(new Vector3(-m_body.position.x, m_body.position.z,m_body.position.y), t_body.position));
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
        body.TeleportRoot( m_body.position + m_body.TransformDirection(new Vector3(0f, 0.05f, 0.01f)), Quaternion.Euler(player.rotation.eulerAngles.x,orientationCube.rotation.eulerAngles.y,player.rotation.eulerAngles.z));
        //Vector3 co_body = transform.TransformDirection(m_body.position);
        //body.TeleportRoot( new Vector3(co_body.x, co_body.y, co_body.z), orientationCube.rotation);
        //foot.TeleportRoot( new Vector3(m_foot.position.x, m_foot.position.y, m_foot.position.z), m_foot.rotation);
        //t_body.rotation = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
    }
    void jointfunc(){
        
        //Debug.Log("pos: " + (joint.jointPosition[0] * 180 / Mathf.PI).ToString()+ " vel: " + joint.jointVelocity[0].ToString() + " tor: " + joint.driveForce[0].ToString());
        
        var rp_drive = joint.xDrive;
            startingPos *= -1;
            rp_drive.target = startingPos;

            joint.xDrive = rp_drive;
        
    }

    public float GetAngle (Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;
 
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
}
