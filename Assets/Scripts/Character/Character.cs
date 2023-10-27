using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody2D rb;

    public Bullet bullet;

    

    public float moveSpeed;
    public Vector2 frictionSpeed;
    protected void FrictionSlowDown()
    {
        float t_x = rb.velocity.x, t_y = rb.velocity.y;
        if (t_x > 0)
        {
            t_x = t_x - frictionSpeed.x > 0 ? t_x - frictionSpeed.x : 0;
        }
        else
        {
            t_x = t_x + frictionSpeed.x < 0 ? t_x + frictionSpeed.x : 0;
        }
        if (t_y > 0)
        {
            t_y = t_y - frictionSpeed.y > 0 ? t_y - frictionSpeed.y : 0;
        }
        else
        {
            t_y = t_y + frictionSpeed.y < 0 ? t_y + frictionSpeed.y : 0;
        }
        rb.velocity = new Vector2(t_x, t_y);
    }

    
    public bool CheckNear(Vector3 pos1, Vector3 pos2, float f_distance)
    {
        float distance = Vector3.Distance(pos1, pos2);
        if (distance > f_distance)
            return false;
        return true;
    }
}
