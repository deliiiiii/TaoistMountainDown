using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : Bullet
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            //TODO Damage to player
            RomoveBullet();
        }
    }
}
