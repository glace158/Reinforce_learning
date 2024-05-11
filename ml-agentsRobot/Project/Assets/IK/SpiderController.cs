using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpiderController : MonoBehaviour
{
    [Header("Walk Speed")]
    [Range(0f, m_maxWalkingSpeed)]

    [SerializeField]
    private float m_TargetWalkingSpeed = m_maxWalkingSpeed;

    const float m_maxWalkingSpeed = 1.2f; //The max walking speed

    //The current target walking speed. Clamped because a value of zero will cause NaNs
    public float TargetWalkingSpeed
    {
        get { return m_TargetWalkingSpeed; }
        set { m_TargetWalkingSpeed = Mathf.Clamp(value, .1f, m_maxWalkingSpeed); }
    }

    public float _walk_speed = 0.5f;
    public float _run_speed = 1f;
    public float _trun_gain = 60f;
    public float smoothness = 5f;
    public Transform look_target;
   
    public float defaultup = 0.5f;
    Vector3 movement;
    private bool turn_mode = true;

    public float h;
    public float v;
    void Start(){
        initParameter();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //keyboardController();
        randomController();
        borderSensing();
        fixedup();
    }

    void keyboardController(){
        float speed = 0;

        if (Input.GetKey(KeyCode.E))
        {
            turn_mode = !turn_mode;
        }

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = _run_speed;
        }
        else
        {
            speed = _walk_speed;
        }

        if (turn_mode)
        {
            turn_move(h, v, speed); 
        }
        else
        {
            move(h, v, speed);
        }
    }

    void initParameter(){
        TargetWalkingSpeed = Random.Range(0f, 2.0f);
        turn_mode = Random.Range(0, 2) == 1? true : false; 
        do{
            h = Random.Range(-1f, 1f) ;
            v = Random.Range(-1f, 1f) ;
		}while(h == 0 && v == 0);
    }

    void changeParameter(){
        float num = Random.Range(0.0f, 1.0f);
        if (num < 0.01){
            TargetWalkingSpeed = Random.Range(0f, m_maxWalkingSpeed);
            turn_mode = Random.Range(0, 2) == 1? true : false; 
            do{
                h = Random.Range(-1f, 2f) ;
                v = Random.Range(-1f, 2f) ;
		    }while(h == 0 && v == 0);
        }
    }

    void randomController(){
        changeParameter();

        if (turn_mode)
        {
            turn_move(h, v, TargetWalkingSpeed); 
        }
        else
        {
            move(h, v, TargetWalkingSpeed);
        }
    }

    void move(float h, float v, float speed)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    void borderSensing(){
        raycastSensing(transform.forward);
        raycastSensing(-transform.forward);
        raycastSensing(transform.right);
        raycastSensing(-transform.right);
        raycastSensing((transform.forward+transform.right).normalized);
        raycastSensing((-transform.forward+transform.right).normalized);
        raycastSensing((transform.forward-transform.right).normalized);
        raycastSensing((-transform.forward-transform.right).normalized);
    }

    void raycastSensing(Vector3 axis){
        Debug.Log(h.ToString() + " " +v.ToString() );
        int layerMask = 1 << LayerMask.NameToLayer("Border");
        Ray ray = new Ray(transform.position, transform.TransformDirection(axis));
        if (Physics.Raycast(ray, out RaycastHit hit, 1f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(axis), Color.red);
            Debug.Log("detect" + axis.ToString());
            h = Mathf.Abs(axis.x) != 0 ?  -h : h;
            v = Mathf.Abs(axis.z) != 0 ?  -v : v;
            
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(axis), Color.blue); 
    }

    void turn_move(float h, float v, float speed)
    {                                   
        if (h != 0)
        {
            look_target.Rotate(look_target.up * (speed * _trun_gain * Time.deltaTime * h));
            transform.rotation = look_target.rotation;
        }

        if (v != 0)
        {
            movement.Set(0f, 0f, v);
            movement = movement.normalized * speed * Time.deltaTime;
            transform.Translate(movement);
        }
    }

    void fixedup() {
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit info, 100))
        {
            if (Mathf.Abs(defaultup - Vector3.Distance(transform.localPosition, info.point)) > 0.0025)
            {
                float dis = defaultup - Vector3.Distance(transform.localPosition, info.point);
                Vector3 pos = Vector3.Lerp(transform.localPosition, transform.localPosition + (transform.up * dis), Time.deltaTime * 6.0f);
                transform.localPosition = pos;
            }
        }
    }
}
