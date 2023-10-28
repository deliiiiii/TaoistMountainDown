using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : Bullet
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Character>().MDanage(bulletDamage);
            RomoveBullet();
        }
    }
}
