using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltField : MonoBehaviour
{
    [Header("爆炸半径")]
    //public float fieldRadius = 3f;
    [Header("爆炸持续最大时间")]
    public float fieldExistTime = 1.2f;
    //[Header("爆炸持续时长")]
    private float fieldExistTimer = 0f;
    [Header("爆炸伤害")]
    public int fieldDamage = 6;

    private void Update()
    {
        //transform.localScale = new Vector3(fieldRadius, fieldRadius, 1);
        if (!gameObject.activeSelf)
            return;
        if (fieldExistTimer < fieldExistTime)
        {
            fieldExistTimer += Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
            fieldExistTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Debug.Log("WindField OnTriggerEnter2D");
            collision.gameObject.GetComponent<Character>().MDanage(fieldDamage);
        }

        if (collision.CompareTag("Block"))
        {
            collision.GetComponent<Block>().MDamage(1);
        }
    }
}
