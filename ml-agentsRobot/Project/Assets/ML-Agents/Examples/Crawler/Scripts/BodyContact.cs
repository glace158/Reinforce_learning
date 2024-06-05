using UnityEngine;
using Unity.MLAgents;

public class BodyContact : MonoBehaviour
{
    [HideInInspector] public Agent agent;

    public float groundBodyPenalty; // Penalty amount (ex: -1).
    public bool touchingbody;
    const string k_body = "robot"; // Tag of ground object.

    /// <summary>
    /// Check for collision with ground, and optionally penalize agent.
    /// </summary>
    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.gameObject.tag);

        if (col.gameObject.CompareTag(k_body))
        {
            touchingbody = true;
            //agent.AddReward(groundBodyPenalty);
            Debug.Log("body touch");
        }
    }

    /// <summary>
    /// Check for end of ground collision and reset flag appropriately.
    /// </summary>
    void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag(k_body))
        {
            touchingbody = false;
        }
    }
}
