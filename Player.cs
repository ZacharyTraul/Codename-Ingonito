using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
    public float speed;
    public float jump;
    public float CameraRemember;
    public Animator anim;
    public GameObject caughtspot;

    private bool grounded;
    private float v;
    private float j;
    private Rigidbody rb;
    private Vector3 lastplace;

    public static float h;
    public static bool caught;
    public static float alarm;
    public static int state;
    public static bool By;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        caught = false;
        alarm = 0;
    }

    void Update() {
        h = Input.GetAxis("Horizontal");
        if (state == 0)
        {
            if (grounded == true)
            {
                j = Input.GetAxis("Jump");
                rb.velocity = new Vector3(h * speed, j * jump, 0f);
            }
            else
            {
                rb.velocity = new Vector3(h * speed, rb.velocity.y, 0f);
            }


            if (j > 0)
            {
                anim.SetBool("Up", true);
                anim.SetBool("Down", false);
            }
            if ((transform.position.y < lastplace.y) && grounded == false)
            {
                anim.SetBool("Up", false);
                anim.SetBool("Down", true);
            }


            if (!(h == 0))
            {
                if (h > 0)
                {
                    transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
                }
                else if (h < 0)
                {
                    transform.localScale = new Vector3(-0.45f, 0.45f, 0.45f);
                }
                if (grounded == true)
                {
                    anim.SetBool("Moving", true);
                }
            }
            else
            {
                anim.SetBool("Moving", false);
            }
        }

        if (state == 1)
        {
            v = Input.GetAxis("Vertical");
            rb.velocity = new Vector3(h * speed, v * speed, 0);
        }

        if (alarm > 0)
        {
            alarm -= 1;
        }

        lastplace = transform.position;
        j = 0;
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            grounded = false;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            grounded = true;
            anim.SetBool("Down", false);
        }
        else
        {
            grounded = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "taser")
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            Destroy(other.gameObject);
            anim.SetBool("Moving", false);
            transform.position = caughtspot.transform.position;
        }

        if (other.tag == "cameracone")
        {
            if(alarm < CameraRemember)
            {
                alarm = CameraRemember;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladder")
        {
            state = 0;
            rb.useGravity = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if ((other.tag == "Ladder") && (Input.GetKey("w")))
        {
            state = 1;
            rb.useGravity = false;
        }        
    }
}
