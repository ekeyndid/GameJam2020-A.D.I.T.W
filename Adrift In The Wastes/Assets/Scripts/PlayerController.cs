using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public GameObject Player;
    public static Vector3 PlayerPos;
    private Rigidbody rb;
    private int count;
    public bool Key1;
    public GameObject PresGroup;
    public GameObject PastGroup;
    public GameObject FixedGroup;
    public GameObject KeyOne;
    public float PlayerVelocity;
    public Camera PlayCam;
    private Vector3 offset;
    public float MaxSpeed;
    public float Accel;
    public float CamDist;
    private bool YAY;
    public float CurrentTimePhase = 0;
    public bool CanTimeShift = true;
    public bool Jumping = false;
    private float MoveV = 0.0f;
    public bool Falling = false;
    public static bool IsGrounded = true;
    public float TimeJump = 0;
    public Vector3 StartPos;
    private float MoveH;
    private float change;
    public static bool canjump = true;
    public Vector3 jump;
    void Start()
    {
        
        canjump = true;

        Key1 = false;
        Player.transform.position = new Vector3(0, 2, 0);
        StartPos = new Vector3(0, 0, 0);
        MoveV = 0;
        IsGrounded = true;
        rb = GetComponent<Rigidbody>();
        offset = PlayCam.transform.position - Player.transform.position;
        MaxSpeed = 4.0f;
        Accel = .2f;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        CamDist = -1;



    }


    void Update()
    {
        PlayerPos = Player.transform.position;
        switch (MoveH)
        {
            case 1:
                Player.transform.rotation = new Quaternion(0, 0, 0, 0);
                break;
            case -1:
                Player.transform.rotation = new Quaternion(0, 180, 0, 0);
                break;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Jump") && IsGrounded == true && canjump == true)
        {
            IsGrounded = false;
            //Accel = 4.0f;
            rb.AddForce(jump * 4.0f, ForceMode.Impulse);
            //print("flying up");
            Jumping = false;
            Falling = true;
            


        }
        if (Input.GetButton("Fire1") && CanTimeShift)
        {
            StartCoroutine(TimeShift());
        }




        //print(MoveV);
        //print(TimeJump);
        MoveH = Input.GetAxis("Horizontal");
       
            
        

        Vector3 Movement = new Vector3(MoveH*2, 0, 0.0f);
        if (IsGrounded)
        {
            if (rb.velocity.magnitude <= MaxSpeed)
            {
                rb.velocity += Movement * Accel;
            }
        }
        else if(!IsGrounded){
            if (rb.velocity.magnitude <= MaxSpeed+5 && !IsGrounded)
            {
                rb.velocity += Movement * Accel;
            }
        }

        PlayerVelocity = rb.velocity.magnitude;

  

       
    }

    private void LateUpdate()
    {

        PlayCam.transform.position = transform.position + offset + new Vector3(MoveH*1, 0, CamDist / 20);
    }

    void OnTriggerEnter(Collider boi)
    {



        if (boi.gameObject.CompareTag("ActivateChamber") && CurrentTimePhase != 2)
        {

            CurrentTimePhase = 2;
            StartCoroutine(TimeShift());
        }
        else if (boi.gameObject.CompareTag("Zoomy"))
        {
            StartCoroutine(ShiftCamera(boi.gameObject.GetComponent<ZoomValue>().Value));
            
            
        }
        else if (boi.gameObject.CompareTag("Key1") && boi.gameObject.GetComponent<rotateboi>().State != 1)
        {
            KeyOne = boi.gameObject;
            boi.gameObject.GetComponent<rotateboi>().State = 1;
            Key1 = true;

        }



    }


    private void OnCollisionStay(Collision boi)
    {
        
        //print("i hit something");
        if (boi.gameObject.CompareTag("Ground"))
        {
            print("I am grounded");
            
            TimeJump = 0;
            IsGrounded = true;
            Falling = false;
            canjump = true;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            Player.transform.parent = null;


        }
        else if (boi.gameObject.CompareTag("MovingP"))
        {
            print("I am grounded and im on movingp");
            
            TimeJump = 0;
            IsGrounded = true;
            Falling = false;
            canjump = true;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            Player.transform.parent = boi.transform;



        }
        else if (boi.gameObject.CompareTag("Hole1") && Key1)
        {
            KeyOne.gameObject.GetComponent<rotateboi>().State = 2;
            StartCoroutine(killkey());


        }
    }
    
    private void OnCollisionExit(Collision boi)
    {
        //print("i hit something");
        if (boi.gameObject.CompareTag("Ground"))
        {
            print("I am NOT grounded");
            Player.transform.parent = null;
            IsGrounded = false;



        }
        else if (boi.gameObject.CompareTag("MovingP"))
        {
            print("I am NOT grounded and i left movingp");
            Player.transform.parent = null;
            IsGrounded = false;



        }
    }
    IEnumerator ShiftCamera(float endo)
    {
        print("StartING cAM mOVE!");
        for (float i = 0; i < 1; i += .01f)
        {
            yield return new WaitForSecondsRealtime(.01f);
            float CurrentDist = CamDist;
            print(CurrentDist);
            CamDist = -EaseOutQuad(CurrentDist, endo, i);
        }
    }
    IEnumerator killkey()
    {
        yield return new WaitForEndOfFrame();
        Key1 = false;
        KeyOne = null;
    }
    IEnumerator TimeShift()
    {
        Player.transform.parent = null;
        rb.AddForce(jump * 1.0f, ForceMode.Impulse);
        //print("flying up");
        Jumping = false;
        Falling = true;
        CanTimeShift = false;
        switch (CurrentTimePhase)
        {
            case 0:
                CurrentTimePhase = 1;
                PresGroup.gameObject.SetActive(false);
                PastGroup.gameObject.SetActive(true);
                break;
            case 1:
                CurrentTimePhase = 0;
                PresGroup.gameObject.SetActive(true);
                PastGroup.gameObject.SetActive(false);
                break;
            case 2:
                PresGroup.gameObject.SetActive(false);
                PastGroup.gameObject.SetActive(false);
                FixedGroup.gameObject.SetActive(true);
                break;
        }
        StartCoroutine(TimeShiftCoolDown(2));
        yield return new WaitForSeconds(0);

    }

    IEnumerator TimeShiftCoolDown(float cooldown)
    {
        yield return new WaitForSecondsRealtime(cooldown);
        CanTimeShift = true;
        
    }



    public static float EaseOutQuad(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }



















}