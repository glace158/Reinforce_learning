using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgentsExamples;
using Unity.MLAgentsRobot;

public class TestScript : MonoBehaviour
{
    float timer = 0;
    int waitingTime = 3;

    void Start()
    {

    }

    private void FixedUpdate() {
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {

        timer = 0;
        }    
    }
}
