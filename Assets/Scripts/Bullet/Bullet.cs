using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("子弹伤害")]
    public int bulletDamage;
    [Header("子弹速度")]
    public float bulletSpeed;
    [Header("子弹大小")]
    public float bulletSize;
    [Header("子弹存在时长")]
    public float bulletExistTime;
    protected float bulletExistTimer = 0;
    public Element bulletElement;
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckExistTime();
    }
    void CheckExistTime()
    {
        bulletExistTimer += Time.deltaTime;
        if(bulletExistTimer >= bulletExistTime)
        {
            RomoveBullet();
        }
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            RomoveBullet();
        if(collision.gameObject.CompareTag("Block"))
        {
            collision.gameObject.GetComponent<Block>().MDamage(1);
            RomoveBullet();
        }
    }
    protected void RomoveBullet()
    {
        RoomManager.instance.currentRoom.Value.existing_bullet.Remove(gameObject.GetComponent<Bullet>());
        Destroy(gameObject);
    }
}
