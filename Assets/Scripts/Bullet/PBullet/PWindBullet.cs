using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PWindBullet : PBullet
{
    public GameObject windField;
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy") ||collision.gameObject.CompareTag("Block"))
        {
            if (collision.gameObject.GetComponent<Enemy>())
                collision.gameObject.GetComponent<Character>().MDanage(bulletDamage);
            SetSomeActive_beforeWind();
            
        }
    }

    public void SetSomeActive_beforeWind()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        windField.GetComponent<Animator>().SetTrigger("Appear");
        windField.SetActive(true);
        
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        Invoke(nameof(RomoveBullet), windField.GetComponent<WindField>().fieldExistTime);
    }
}
