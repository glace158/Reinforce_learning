using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Events;


namespace Unity.MLAgentsRobot{
public class AnimController : MonoBehaviour
{
        [Header("Walk Speed")]
        [Range(0f, m_maxWalkingSpeed)]

        [SerializeField]
        private float m_TargetWalkingSpeed = m_maxWalkingSpeed;
        
        const float m_maxWalkingSpeed = 1.2f; //The max walking speed

        private ProceduralAnimation proceduralAnimation;

        //The current target walking speed. Clamped because a value of zero will cause NaNs
        public float TargetWalkingSpeed
        {
            get { return m_TargetWalkingSpeed; }
            set { m_TargetWalkingSpeed = Mathf.Clamp(value, .1f, m_maxWalkingSpeed); }
        }

        [SerializeField]
        private float _trun_gain = 80f;
        
        public Transform look_target;
    
        public float defaultUp = 0.33f;
        
        public bool turn_mode = true;

        public int walkMode = 0;

        public bool useKeyboard = false;

        [SerializeField]
        private float h = 0;
        [SerializeField]
        private float v = 0;

        private Vector3 startingPos = new Vector3(0, 0, 0);

        void Awake(){
            proceduralAnimation = GetComponentInChildren<ProceduralAnimation>();
            walkMode = proceduralAnimation.GetWalkMode();
            startingPos = transform.position;
            Debug.Log(startingPos);
            //randParameter();
        }

        public Vector2 GetDirection(){
            return new Vector2(h, v);
        }

        public float GetTargetSpeed(){
            return TargetWalkingSpeed;
        }

        public bool GetTurnMode(){
            return turn_mode;
        }

        void FixedUpdate()
        {
            if(useKeyboard){
                keyboardController();
            }
            else{
                randomController();
            }

            borderSensing();
            fixedup();
        }

        public void reset(){
            //transform.rotation = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
            look_target.rotation = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
            transform.position = startingPos;
            proceduralAnimation.reset(); 
            v = 0;
            h = 0;   
        }

        void keyboardController(){
            float speed = TargetWalkingSpeed;

            if (Input.GetKey(KeyCode.E))
            {
                turn_mode = !turn_mode;
            }

            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

            if (turn_mode)
            {
                turn_move(h, v, speed); 
            }
            else
            {
                move(h, v, speed);
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


        public void ChangeWalkMode(int mode){
            proceduralAnimation.SetWalkMode(mode);
            walkMode = proceduralAnimation.GetWalkMode();
        }


        void changeParameter(){
            float num = Random.Range(0.0f, 1.0f);
            if (num < 0.01){
                randParameter();
            }
        }

        void randParameter(){
                TargetWalkingSpeed = Random.Range(0f, m_maxWalkingSpeed);
                turn_mode = Random.Range(0, 2) == 1? true : false;
                h = Random.Range(-1f, 2f) ;
                v = Random.Range(-1f, 2f) ;
                //do{  
                //}while(h == 0 && v == 0);
        }

        

        void move(float h, float v, float speed)
        {
            Vector3 movement =new Vector3(0f,0f,0f);
            movement.Set(h, 0f, v);
            movement = movement.normalized * speed * Time.deltaTime;
            transform.Translate(movement);
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
                Vector3 movement =new Vector3(0f,0f,0f);
                movement.Set(0f, 0f, v);
                movement = movement.normalized * speed * Time.deltaTime;
                transform.Translate(movement);
            }
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
            int layerMask = 1 << LayerMask.NameToLayer("Border");
            Ray ray = new Ray(transform.position, transform.TransformDirection(axis));
            if (Physics.Raycast(ray, out RaycastHit hit, 1f, layerMask))
            {
                h = Mathf.Abs(axis.x) != 0 ?  -h : h;
                v = Mathf.Abs(axis.z) != 0 ?  -v : v;
            }
        }

        

        void fixedup() {
            Ray ray = new Ray(transform.position, -transform.up);
            if (Physics.Raycast(ray, out RaycastHit info, 100))
            {
                if (Mathf.Abs(defaultUp - Vector3.Distance(transform.localPosition, info.point)) > 0.0025)
                {
                    float dis = defaultUp - Vector3.Distance(transform.localPosition, info.point);
                    Vector3 pos = Vector3.Lerp(transform.localPosition, transform.localPosition + (transform.up * dis), Time.deltaTime * 6.0f);
                    transform.localPosition = pos;
                }
            }
        }
    }
}
