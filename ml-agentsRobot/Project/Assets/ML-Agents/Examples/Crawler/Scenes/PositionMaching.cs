using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMaching : MonoBehaviour
{
    [Header("Anime Part")]
    public Transform animFoot;
    public Transform animRoot;
    [Header("Robot Part")]
    public Transform robotFoot;
    public Transform robotRoot;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(GetMatchingFootPosition());
        //Debug.Log(GetMatchingRootPosition());
        Debug.Log(GetMatchingRootAngle());
        
    }

    float GetMatchingFootPosition(){
        float distance = 0f;
        distance += Vector3.Distance(animFoot.position , robotFoot.position);
        
        return Mathf.Exp(-40 * distance);
    }

    float GetMatchingRootPosition(){
        float distance = Vector3.Distance(animRoot.position - new Vector3(0f, 0f, -0.05f), robotRoot.position); 
        
        return Mathf.Exp(-40 * distance);
    }

    float GetMatchingRootAngle(){
        float angle = Vector2.Angle(animRoot.forward, robotRoot.forward);
        Debug.Log("angle: " + angle);
        angle = Mathf.Exp(-0.01f * Mathf.Pow(angle,2));
        return angle;
    }
}
