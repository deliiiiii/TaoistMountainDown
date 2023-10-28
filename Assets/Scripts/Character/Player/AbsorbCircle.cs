using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbCircle : MonoBehaviour
{
    [Header("最大存在时长")]
    public float existTime;
    [Header("已经存在时长")]
    public float existTimer = 0f;
    [Header("吸收半径")]
    public float radius;
    private void Update()
    {
        if(existTimer < existTime && gameObject.activeSelf)
        {
            existTimer += Time.deltaTime;
        }
        else
        {
            existTimer = 0f;
            gameObject.SetActive(false);
        }

        transform.localScale = new Vector3(radius, radius, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("EBullet"))
        {
            Element.TYPE type = collision.GetComponent<Bullet>().bulletElement.type;
            if (!PlayerManager.instance.currentPlayer.AbsorbElement(type))
                return;
            Destroy(collision.gameObject);
        }
    }
}
