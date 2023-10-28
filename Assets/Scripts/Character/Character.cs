using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody2D rb;

    [Header("最大血量")]
    public int maxHP;
    [Header("当前血量")]
    public int curHP;
    public Bullet bullet;
    [HideInInspector]
    public Vector3 bulletDirection;

    [Header("移动速度")]
    public float moveSpeed;
    [Header("摩擦力")]
    public float frictionSpeed; 

    public void MDanage(int damage)
    {
        if (GameManager.instance.isGUA)
            return;
        curHP -= damage;
        if (curHP <= 0)
        {
            curHP = 0;
            if(GetComponent<Enemy>())
            {
                RoomManager.instance.currentRoom.Value.existing_enemy.Remove(GetComponent<Enemy>());
                Destroy(gameObject);
                if(RoomManager.instance.currentRoom.Value.existing_enemy.Count == 0)
                {
                    RoomManager.instance.currentRoom.Value.SetDoorState(false);
                }
            }
            if(GetComponent<Player>())
            {
                Debug.Log("GAME OVER");
            }
        }
    }
    protected void FrictionSlowDown()
    {
        float t_x = rb.velocity.x, t_y = rb.velocity.y;
        if (t_x > 0)
        {
            t_x = t_x - frictionSpeed > 0 ? t_x - frictionSpeed : 0;
        }
        else
        {
            t_x = t_x + frictionSpeed < 0 ? t_x + frictionSpeed : 0;
        }
        if (t_y > 0)
        {
            t_y = t_y - frictionSpeed > 0 ? t_y - frictionSpeed : 0;
        }
        else
        {
            t_y = t_y + frictionSpeed < 0 ? t_y + frictionSpeed : 0;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(t_x, t_y);
    }

    
    public bool CheckNear(Vector3 pos1, Vector3 pos2, float f_distance)
    {
        float distance = Vector3.Distance(pos1, pos2);
        if (distance > f_distance)
            return false;
        return true;
    }
}
