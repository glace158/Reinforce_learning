using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgentsRobot;
using Unity.MLAgentsExamples;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class HotdogAgent : Agent
{
    [Header("Walk Speed")]
    [Range(0.1f, m_maxWalkingSpeed)]
    [SerializeField]
    [Tooltip(
        "The speed the agent will try to match.\n\n" +
        "TRAINING:\n" +
        "For VariableSpeed envs, this value will randomize at the start of each training episode.\n" +
        "Otherwise the agent will try to match the speed set here.\n\n" +
        "INFERENCE:\n" +
        "During inference, VariableSpeed agents will modify their behavior based on this value " +
        "whereas the CrawlerDynamic & CrawlerStatic agents will run at the speed specified during training "
    )]
    //The walking speed to try and achieve
    private float m_TargetWalkingSpeed = m_maxWalkingSpeed;

    const float m_maxWalkingSpeed = 15; //The max walking speed

    //The current target walking speed. Clamped because a value of zero will cause NaNs
    public float TargetWalkingSpeed
    {
        get { return m_TargetWalkingSpeed; }
        set { m_TargetWalkingSpeed = Mathf.Clamp(value, .1f, m_maxWalkingSpeed); }
    }

    //The direction an agent will walk during training.
    [Header("Target To Walk Towards")]
    public Transform TargetPrefab; //Target prefab to use in Dynamic envs
    private Transform m_Target; //Target the agent will walk towards during training.

    [Header("Body Parts")][Space(10)] public Transform body;
    public Transform RR_Hip;
    public Transform RR_Upper;
    public Transform RR_Lower;
    public Transform RL_Hip;
    public Transform RL_Upper;
    public Transform RL_Lower;
    public Transform FR_Hip;
    public Transform FR_Upper;
    public Transform FR_Lower;
    public Transform FL_Hip;
    public Transform FL_Upper;
    public Transform FL_Lower;
    
    //This will be used as a stabilized model space reference point for observations
    //Because ragdolls can move erratically during training, using a stabilized reference transform improves learning
    OrientationCubeController m_OrientationCube;

    //The indicator graphic gameobject that points towards the target
    DirectionIndicator m_DirectionIndicator;

    JointController jointController;

    void SpawnTarget(Transform prefab, Vector3 pos)
    {
        m_Target = Instantiate(prefab, pos, Quaternion.identity, transform.parent);
    }

    public override void Initialize()
    {
        SpawnTarget(TargetPrefab, transform.position); //spawn target

        m_OrientationCube = GetComponentInChildren<OrientationCubeController>();
        m_DirectionIndicator = GetComponentInChildren<DirectionIndicator>();
        jointController = GetComponent<JointController>();

        //Setup each body part
        jointController.SetupRobotPart(body);
        jointController.SetupRobotPart(RR_Hip);
        jointController.SetupRobotPart(RR_Upper);
        jointController.SetupRobotPart(RR_Lower);
        jointController.SetupRobotPart(RL_Hip);
        jointController.SetupRobotPart(RL_Upper);
        jointController.SetupRobotPart(RL_Lower);
        jointController.SetupRobotPart(FR_Hip);
        jointController.SetupRobotPart(FR_Upper);
        jointController.SetupRobotPart(FR_Lower);
        jointController.SetupRobotPart(FL_Hip);
        jointController.SetupRobotPart(FL_Upper);
        jointController.SetupRobotPart(FL_Lower);
    }

    public override void OnEpisodeBegin()
    {
        foreach (var bodyPart in jointController.robotPartsDict.Values)
        {
            bodyPart.Reset(bodyPart);
        }

        body.rotation = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);

        UpdateOrientationObjects();

        TargetWalkingSpeed = Random.Range(0.1f, m_maxWalkingSpeed);
    }

    public void CollectObservationBodyPart(RobotPart rp, VectorSensor sensor)
    {
        //GROUND CHECK
        sensor.AddObservation(rp.groundContact.touchingGround); // Is this bp touching the ground

        if (rp.trans != body)
        {
            sensor.AddObservation(rp.currentStrength / jointController.maxJointForceLimit);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var cubeForward = m_OrientationCube.transform.forward;
        var velGoal = cubeForward * TargetWalkingSpeed;
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

    }

    void UpdateOrientationObjects()
    {
        m_OrientationCube.UpdateOrientation(body, m_Target);
        if (m_DirectionIndicator)
        {
            m_DirectionIndicator.MatchOrientation(m_OrientationCube.transform);
        }
    }

    void FixedUpdate()
    {

    }
}
