using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgentsExamples;
using Unity.MLAgentsRobot;

public class TestScript : MonoBehaviour
{

    public Transform cubeA;
    public Transform cubeB;
    float timer = 0.0f;
    
    float waitingTime = 3f;

    [Header("Robot Controller")]
    private MotorController m_MoController;

    [Header("Target Animation")]
    public GameObject AnimCharator;
    public Transform targetAnim;
    public Transform lookTargetCube;
    private ProceduralAnimBody proceduralAnimBody;
    private AnimController animController;

    public bool is_reset =false;
    private void Awake()
    {
        m_MoController = GetComponent<MotorController>();
        
        proceduralAnimBody = targetAnim.GetComponent<ProceduralAnimBody>();
        animController = targetAnim.GetComponent<AnimController>();
    }
    private void Start(){
        EpisodeReset();
    }

    private void FixedUpdate() {
        //CheckRootRotation();
        if (is_reset){
            EpisodeReset();
            
            is_reset = false;
            GetAnimJointAngle();
        }
        //proceduralAnimBody.initSet();
        //m_MoController.RobotReset(proceduralAnimBody.GetInitPosition(new Vector3(0f, 0.05f, 0.01f)), Quaternion.Euler(proceduralAnimBody.GetRootRotation()));
        //FootContact() ;
        //RootAngleCompare();
        //GetBobyHeight();
        //RootPositionCompare();
        //FootPositionCompare();
        //GetAnimJointAngle();
        //AngleCompare();
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {
            //RandomRot();
        timer = 0;
        }

        Debug.Log(m_MoController.GetJointVelocity()[2]);
    }
    
    void GetAnimJointAngle(){
        m_MoController.RobotReset(proceduralAnimBody.GetInitPosition(new Vector3(0f, 0.0f, 0.01f)), Quaternion.Euler(proceduralAnimBody.GetRootRotation()));    
        //Debug.Log(targetAngle);
        //Debug.Log("3: " + proceduralAnimBody.GetJointAngle()[3]);
        for (int i = 0; i < 12; i++){
            var targetAngle = proceduralAnimBody.GetJointAngle()[i];
            m_MoController.SetJointAngle(i, targetAngle);
        }
    }
    void EpisodeReset(){
        Destroy(targetAnim.gameObject);
        targetAnim = Instantiate(AnimCharator, new Vector3(0, 0.8322765f, 0), Quaternion.identity).transform;
        proceduralAnimBody = targetAnim.GetComponent<ProceduralAnimBody>();
        animController = targetAnim.GetComponent<AnimController>();
        animController.SetLookTarget(lookTargetCube);
        proceduralAnimBody.SetOrientationCube(lookTargetCube);
        targetAnim.SetParent(this.transform.parent);
        //animController.reset();

        m_MoController.RobotReset(proceduralAnimBody.GetInitPosition(new Vector3(0f, 0.05f, 0.01f)), Quaternion.Euler(proceduralAnimBody.GetRootRotation()));    
    }
    void FootPositionCompare(){
        float distance = 0f;
        for (int i = 0; i < 4; i++){
            distance += Mathf.Pow(Mathf.Abs(Vector3.Distance(proceduralAnimBody.GetFootPosition(i, new Vector3(0f,0f,0f)),m_MoController.GetFootPosition(i))),2);
            //distance += Mathf.Clamp(Vector3.Distance(proceduralAnimBody.GetFootPosition(i, new Vector3(0f,0f,0f)),m_MoController.GetFootPosition(i)), 0f, 1f);
        }
        //distance = Mathf.Pow(1 - Mathf.Pow(distance / 4f, 2), 2);
        distance = Mathf.Exp(-40 * distance);
        Debug.Log(distance);
    }

    void AngleCompare(){
        var targetAngle = 0f;
        Debug.Log("=====================");
        for (int i = 0; i < 12; i++){
            targetAngle += Mathf.Pow(Mathf.Abs(m_MoController.GetJointAngles()[i] - proceduralAnimBody.GetJointAngle()[i]),2);
        }
        //Debug.Log(proceduralAnimBody.GetJointAngle()[0]);
        Debug.Log(Mathf.Exp(-0.02f * targetAngle));//12
    }

    void RootPositionCompare(){
        float distance = Vector3.Distance(proceduralAnimBody.GetRootPosition(new Vector3(0f, 0.05f, 0.01f)),m_MoController.GetRootPosition()); 
        //var positionMagnitude = Mathf.Clamp(Vector3.Distance(proceduralAnimBody.GetRootPosition(new Vector3(0f, 0.05f, 0.01f)),m_MoController.GetRootPosition()), 0f, 1f);
        //float distance = Mathf.Pow(1 - Mathf.Pow(positionMagnitude / 1f, 2), 2);
        Debug.Log("=====================");
        //Debug.Log(distance);
        distance = Mathf.Exp(-40f * Mathf.Pow(distance,2));
        
        Debug.Log(distance);
    }

    void RootAngleCompare(){
        //float angle = (Vector3.Dot(proceduralAnimBody.GetOrientationRotation(), m_MoController.GetOrientationRotation()) + 1) * .5F;
        //float angle = Vector3.Dot(proceduralAnimBody.GetRootRotation(), m_MoController.GetRootRotation());
        float angle = Vector2.Angle(proceduralAnimBody.GetOrientationRotation(), m_MoController.GetOrientationRotation());
        Debug.Log(angle);
        angle = Mathf.Exp(-0.01f * angle);
        //float angle = (Vector3.Dot(proceduralAnimBody.GetRootForward(), m_MoController.GetRootForward()) + 1) * .5f;
        Debug.Log(angle);
    }
    void CheckRootRotation(){
        Debug.Log(m_MoController.GetRootRotation().x.ToString()+ " "+ m_MoController.GetRootRotation().y.ToString() + " "+ m_MoController.GetRootRotation().z.ToString());
        //m_MoController.GetRootRotation().y -360 * 180 + 180 / 0 - 360 -180.0f;
    }

    void FootContact(){
        Debug.Log(m_MoController.GetFootContact(0) + " " + m_MoController.GetFootContact(1) + " " + m_MoController.GetFootContact(2) + " " +m_MoController.GetFootContact(3));
    }
    int value = 0;
    void RandomRot(){
        m_MoController.RobotReset(proceduralAnimBody.GetInitPosition(new Vector3(0f, 0.05f, 0.01f)), Quaternion.Euler(Random.Range(0.0f, 360f), value++, Random.Range(0.0f, 360f)));
    
        //m_MoController.RobotReset(proceduralAnimBody.GetInitPosition(new Vector3(0f, 0.05f, 0.01f)), Quaternion.Euler(0f, Random.Range(0.0f, 360f), 0f));
    }

    void GetBobyHeight(){
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit info, 100))
        {
            Debug.Log(Vector3.Distance(transform.position, info.point));
        }
    }
}
