using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicator2 : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
    }

    public void updateDirection(float h, float v){
        Vector3 direction = new Vector3(transform.position.x + h, 0f,transform.position.z + v);
        //Vector3 targetPos = target.position + new Vector(0, offsetY, 0);
        transform.rotation = Quaternion.LookRotation(direction.normalized);
    }
}
