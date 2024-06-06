using UnityEngine;
using Unity.MLAgents;

public class BodyContact : MonoBehaviour
{
    [HideInInspector] public Agent agent;

    public float groundBodyPenalty =-0.01f; // Penalty amount (ex: -1).
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
            //Physics.IgnoreLayerCollision(2, 2, true);
            touchingbody = true;

            agent.AddReward(groundBodyPenalty);
            //Debug.Log(this.gameObject.name +" " + col.gameObject.name + " body touch");
        }
    }

    /// <summary>
    /// Check for end of ground collision and reset flag appropriately.
    /// </summary>
    
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(k_body))
        {
            //Debug.Log(this.gameObject.name +" " + other.gameObject.name + " body untouch");
            touchingbody = false;
            //Physics.IgnoreLayerCollision(2, 2, false);
        }
    }
}
