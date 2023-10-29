using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindField : MonoBehaviour
{
    [Header("风场半径")]
    //public float fieldRadius = 3f;
    [Header("风场持续最大时间")]
    public float fieldExistTime = 5f;
    //[Header("风场持续时长")]
    private float fieldExistTimer = 0f;
    [Header("风场力度")]
    public float windForce = 10f;
    private void Update()
    {
        //transform.localScale = new Vector3(fieldRadius, fieldRadius, 1);
        if (!gameObject.activeSelf)
            return;
        if(fieldExistTimer < fieldExistTime)
        {
            fieldExistTimer += Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
            fieldExistTimer = 0f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("WindField OnTriggerStay2D");
            Vector3 windCenter = transform.position;
            Vector3 enemyPos = collision.transform.position;
            Vector3 windDir = ( windCenter - enemyPos).normalized;
            collision.GetComponent<Rigidbody2D>().AddForce(windDir * windForce);

        }
    }
}
