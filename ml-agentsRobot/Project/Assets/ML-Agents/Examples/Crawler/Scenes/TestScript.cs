using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgentsExamples;
using Unity.MLAgentsRobot;

public class TestScript : MonoBehaviour
{
    [Header("angle")]
    [Range(-1f, 1f)]
    [SerializeField]
    private float m_targetAngle = 0f;

    public float targetAngle
    {
        get { return m_targetAngle; }
        set { m_targetAngle = value; }
    }
    public ArticulationBody targetJoint;

    public Transform t_body;
    public Transform t_foot;

    public Transform player;
    public Transform m_body;
    public Transform m_foot;

    

    

    public Transform orientationCube;
    public Transform m_OrientationCube2;
    
    public ArticulationBody body;
    public ArticulationBody foot;
    // Start is called before the first frame update
    public ArticulationBody joint;


    public Transform TestJoint;
    MotorController m_MoController;

    public int startingPos = 45;

    public Transform pointA;
    public Transform pointB;

    float timer = 0;
    int waitingTime = 3;

    void Start()
    {
        m_MoController = GetComponent<MotorController>();
        m_MoController.SetupMotor(TestJoint);
        rotFunc();
        /*
        var rp_drive = joint.xDrive;
            
            rp_drive.target = startingPos;

            joint.xDrive = rp_drive;
            */
    }

    // Update is called once per frame
    void Update()
    {
        m_OrientationCube2.rotation = Quaternion.Euler(0f, t_body.rotation.eulerAngles.y, 0f);
        //Debug.Log(transform.TransformDirection(t_body.position));
        //Debug.Log(Vector3.Distance(new Vector3(m_body.position.x, m_body.position.y+0.01f, m_body.position.z + 0.05f),t_body.position));
        //Debug.Log(m_foot.position-t_foot.position);
        //Debug.Log(m_body.TransformDirection(new Vector3(0f, 0.05f, 0f)));
        //Debug.Log(Vector3.Distance(new Vector3(m_foot.position.x, m_foot.position.y-0.02f, m_foot.position.z),t_foot.position));
        pointA.position = orientationCube.forward;
        pointB.position = t_body.forward;
        //Debug.Log(Vector2.Angle(new Vector2(orientationCube.forward.x, orientationCube.forward.z), new Vector2(m_OrientationCube2.forward.x, m_OrientationCube2.forward.z)));
        //Debug.Log(body.transform.rotation.eulerAngles.x - 180);
        //body.AddForce(new Vector3(200f,0f,0f));
        //checkvel();
        updateJoint();
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
        //body.TeleportRoot( m_body.position + m_body.TransformDirection(new Vector3(0f, 0.05f, 0.01f)), Quaternion.Euler(player.rotation.eulerAngles.x,orientationCube.rotation.eulerAngles.y,player.rotation.eulerAngles.z));
        //Vector3 co_body = transform.TransformDirection(m_body.position);
        body.TeleportRoot( new Vector3(0f, 0.88f, 0f), Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
        //foot.TeleportRoot( new Vector3(m_foot.position.x, m_foot.position.y, m_foot.position.z), m_foot.rotation);
        //t_body.rotation = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
    }

    void updateJoint(){
        float value = (targetAngle + 1f) * 0.5f;

        var rot = Mathf.Lerp(-45, 45, value);

        var mo_drive = targetJoint.xDrive;

        mo_drive.target = rot;

        targetJoint.xDrive = mo_drive;
    }
    void jointfunc(){
        var moDict = m_MoController.motorsDict;
        moDict[TestJoint].SetMotorTarget(45f);
        //Debug.Log("pos: " + (joint.jointPosition[0] * 180 / Mathf.PI).ToString()+ " vel: " + joint.jointVelocity[0].ToString() + " tor: " + joint.driveForce[0].ToString());
        /*
        var rp_drive = joint.xDrive;
            startingPos *= -1;
            rp_drive.target = startingPos;

            joint.xDrive = rp_drive;
        */
    }

    public float GetAngle (Vector2 vStart, Vector2 vEnd)
    {
        Vector2 v = vEnd - vStart;
 
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
}
