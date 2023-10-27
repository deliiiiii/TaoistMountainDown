using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    public float bulletCD;
    private float stillVelocity = 0.1f;//小于这个速度则视为静止
    public enum STATE
    {
        Idling,
        Moving,
        Dead,
        ChangingRoom,
    };
    [SerializeField]
    private STATE currentState;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    public virtual void InputMove()
    {
        if (currentState == STATE.Dead || currentState == STATE.ChangingRoom)
        {
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
            currentState = STATE.Moving;
            SetAnim_Move("Move");
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
            currentState = STATE.Moving;
            SetAnim_Move("Move");
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            currentState = STATE.Moving;
            SetAnim_Move("Move");
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            currentState = STATE.Moving;
            SetAnim_Move("Move");
        }

        if (currentState == STATE.Moving)
        {
            if (!IsStill())
            {
                FrictionSlowDown();
                return;
            }
            else
            {
                rb.velocity = Vector2.zero;
                currentState = STATE.Idling;
                SetAnim_Move(null);
                anim.SetTrigger("Idle");
            }
        }
    }
    public virtual void InputShoot()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           

            Vector3 bulletDirection = ((Vector3)target - transform.position).normalized;

            //Debug.Log("player pos = " + transform.position);
            //Debug.Log("target = " + target);
            //Debug.Log("bulletDirection = " + bulletDirection);

            GameObject newBullet = RoomManager.instance.GenerateBullet(gameObject, bulletDirection).gameObject;
            
            //Debug.Log("bulletVelocity = " + newBullet.GetComponent<Rigidbody2D>().velocity);

            //Debug.Log("Player pos : " + transform.position);
            //Debug.Log("Mouse pos : " + target);
        }
        
    }
    public bool IsStill()
    {
        if(rb.velocity.magnitude <= stillVelocity)
        {
            return true;
        }
        return false;
    }
    public void SetAnim_Move(string name)
    {
        //anim.SetBool("Move_W", false);
        //anim.SetBool("Move_S", false);
        //anim.SetBool("Move_A", false);
        //anim.SetBool("Move_D", false);
        anim.SetBool("Move", false);
        if (name != null)
            anim.SetBool(name, true);
    }
}
