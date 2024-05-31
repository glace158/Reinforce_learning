using System.Collections;
using System.Collections.Generic;
using Unity.MLAgentsRobot;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgentsExamples;
using Unity.MLAgents.Sensors;
using UnityEngine;
using System.Linq;

public class RobotAgent : Agent
{
    float timer = 0.0f;
    float waitingTime = 20f;

    [Header("Robot Controller")]
    private MotorController m_MoController;

    [Header("Target Animation")]
    public Transform targetAnim;
    private ProceduralAnimBody proceduralAnimBody;
    private AnimController animController;

    private float[] previousActions = new float[12] {0f,0f,0f,0f,0f,0f,0f,0f,0f,0f,0f,0f};
    private float[] currentActions = new float[12] {0f,0f,0f,0f,0f,0f,0f,0f,0f,0f,0f,0f};

    public override void Initialize()
    {
        m_MoController = GetComponent<MotorController>();
        proceduralAnimBody = targetAnim.GetComponent<ProceduralAnimBody>();
        animController = targetAnim.GetComponent<AnimController>();
    }

    public override void OnEpisodeBegin()
    {
        animController.reset();

        m_MoController.RobotReset(proceduralAnimBody.GetInitPosition(new Vector3(0f, 0.05f, 0.01f)), Quaternion.Euler(proceduralAnimBody.GetRootRotation()));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        foreach(var angle in m_MoController.GetJointAngles()){
            sensor.AddObservation(angle);//12
        }

        sensor.AddObservation(m_MoController.GetRootRotation().x);//1
        sensor.AddObservation(m_MoController.GetRootRotation().y);//1
        sensor.AddObservation(m_MoController.GetRootRotation().z);//1

        for (int i = 0; i < 4; i++){
            sensor.AddObservation(m_MoController.GetFootContact(i));//4
        }

        sensor.AddObservation(animController.GetDirection().x);//1
        sensor.AddObservation(animController.GetDirection().y);//1
        sensor.AddObservation(animController.GetTargetSpeed());//1
        sensor.AddObservation(animController.GetTurnMode());//1

        for (int i = 0; i < previousActions.Length; i++){//12
            sensor.AddObservation(previousActions[i]);
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var continuousActions = actionBuffers.ContinuousActions;
        var i = -1;

        m_MoController.SetMotorAngle(0,continuousActions[++i]);
        currentActions[i] = (continuousActions[i] + 1f) * 0.5f;
        m_MoController.SetMotorAngle(1,continuousActions[++i]);
        currentActions[i] = (continuousActions[i] + 1f) * 0.5f;
        m_MoController.SetMotorAngle(2,continuousActions[++i]);
        currentActions[i] = (continuousActions[i] + 1f) * 0.5f;
        m_MoController.SetMotorAngle(3,continuousActions[++i]);
        currentActions[i] = (continuousActions[i] + 1f) * 0.5f;
        m_MoController.SetMotorAngle(4,continuousActions[++i]);
        currentActions[i] = (continuousActions[i] + 1f) * 0.5f;
        m_MoController.SetMotorAngle(5,continuousActions[++i]);
        currentActions[i] = (continuousActions[i] + 1f) * 0.5f;
        m_MoController.SetMotorAngle(6,continuousActions[++i]);
        currentActions[i] = (continuousActions[i] + 1f) * 0.5f;
        m_MoController.SetMotorAngle(7,continuousActions[++i]);
        currentActions[i] = (continuousActions[i] + 1f) * 0.5f;
        m_MoController.SetMotorAngle(8,continuousActions[++i]);
        currentActions[i] = (continuousActions[i] + 1f) * 0.5f;
        m_MoController.SetMotorAngle(9,continuousActions[++i]);
        currentActions[i] = (continuousActions[i] + 1f) * 0.5f;
        m_MoController.SetMotorAngle(10,continuousActions[++i]);
        currentActions[i] = (continuousActions[i] + 1f) * 0.5f;
        m_MoController.SetMotorAngle(11,continuousActions[++i]);
        currentActions[i] = (continuousActions[i] + 1f) * 0.5f;

        //Debug.Log(currentActions[2]);

        var footReward = GetMatchingFootPosition();
        var rootReward = GetMatchingRootPosition();
        var rootAngleReward = GetMatchingRootAngle();

        var actionPenalty = GetActionPenalty();
        
        AddReward(footReward);
        AddReward(rootReward);
        AddReward(rootAngleReward);
        AddReward(-actionPenalty);
    }
    void FixedUpdate(){  
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {
            EndEpisode();
            timer = 0;
        } 
        
        var positionMagnitude = Mathf.Clamp(Vector3.Distance(proceduralAnimBody.GetRootPosition(new Vector3(0f, 0.05f, 0.01f)),m_MoController.GetRootPosition()), 0f, 10f);
        float distance = Mathf.Pow(1 - Mathf.Pow(positionMagnitude / 10f, 2), 2);
        if (distance < 0.01){
            SetReward(-1f);
            EndEpisode();
        }
    }

    float GetMatchingFootPosition(){
        float distance = 0f;
        for (int i = 0; i < 4; i++){
            distance += Mathf.Abs(Vector3.Distance(proceduralAnimBody.GetFootPosition(i, new Vector3(0f,0f,0f)),m_MoController.GetFootPosition(i)));
            //distance += Mathf.Clamp(Vector3.Distance(proceduralAnimBody.GetFootPosition(i, new Vector3(0f,0f,0f)),m_MoController.GetFootPosition(i)), 0f, 1f);
        }
        //distance = Mathf.Pow(1 - Mathf.Pow(distance / 4f, 2), 2);
        distance = Mathf.Exp(-40 * Mathf.Pow(distance,2));
        return distance;
    }

    float GetMatchingRootPosition(){
        float distance = Vector3.Distance(proceduralAnimBody.GetRootPosition(new Vector3(0f, 0.05f, 0.01f)),m_MoController.GetRootPosition()); 
        //var positionMagnitude = Mathf.Clamp(Vector3.Distance(proceduralAnimBody.GetRootPosition(new Vector3(0f, 0.05f, 0.01f)),m_MoController.GetRootPosition()), 0f, 1f);
        //float distance = Mathf.Pow(1 - Mathf.Pow(positionMagnitude / 1f, 2), 2);
        distance = Mathf.Exp(-20 * Mathf.Pow(distance,2));
        return distance;
    }

    float GetMatchingRootAngle(){
        //float lookAtTargetReward = (Vector3.Dot(proceduralAnimBody.GetOrientationRotation(), m_MoController.GetOrientationRotation()) + 1) * .5F;
        float angle = (Vector3.Dot(proceduralAnimBody.GetRootForward(), m_MoController.GetRootForward()) + 1) * .5F;
        //float angle = Vector2.Angle(proceduralAnimBody.GetOrientationRotation(), m_MoController.GetOrientationRotation());
        //angle = Mathf.Exp(-0.1f * angle);
        return angle;
    }
    
    float GetActionPenalty(){
        float penalty = 0f; 
        for (int i = 0; i < currentActions.Length; i++){
            penalty += Mathf.Pow(currentActions[i] - previousActions[i], 2);
        }
        //Debug.Log(penalty);
        previousActions = currentActions.ToArray();
        
        //Mathf.Pow(1 - Mathf.Pow(penalty / 12f, 2), 2);
        return penalty;
    }

    public override void Heuristic(in ActionBuffers actionsOut){

    }
}
