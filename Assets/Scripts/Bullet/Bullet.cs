using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletSize;
    
    public float bulletExistTime;
    protected float bulletExistTimer = 0;

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
    }
    protected void RomoveBullet()
    {
        RoomManager.instance.currentRoom.Value.existing_bullet.Remove(gameObject.GetComponent<Bullet>());
        Destroy(gameObject);
    }
}
