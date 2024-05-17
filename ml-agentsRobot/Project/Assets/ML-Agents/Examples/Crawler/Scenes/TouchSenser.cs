using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSenser : MonoBehaviour
{
    bool touchingGround = false;
    const string k_Ground = "ground";
    void OnCollisionEnter(Collision col)
    {
        touchingGround = true;
        Debug.Log(touchingGround);
    }

    void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag(k_Ground))
        {
            touchingGround = false;
        }
    }
}
