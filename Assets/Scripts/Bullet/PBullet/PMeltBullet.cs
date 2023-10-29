using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMeltBullet : PBullet
{
    public GameObject meltField;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy") ||collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Door"))
        {
            if(collision.gameObject.GetComponent<Enemy>())
                collision.gameObject.GetComponent<Character>().MDanage(bulletDamage);
            SetSomeActive_beforeMelt();
        }
    }
    public void SetSomeActive_beforeMelt()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        meltField.SetActive(true);
        meltField.GetComponent<Animator>().SetTrigger("Appear");
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        Invoke(nameof(RomoveBullet), meltField.GetComponent<MeltField>().fieldExistTime);
    }
}
