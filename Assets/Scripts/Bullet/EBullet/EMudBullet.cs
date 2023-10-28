using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMudBullet : EBullet
{
    [SerializeField][Header("霰弹末速度")]
    private float bullet_v1 = 0.2f;
    [SerializeField][Header("霰弹加速度")]
    private float bullet_A = 1f;
    // Start is called before the first frame update
    void Start()
    {
        float ran_delta_v0 = 1f + UnityEngine.Random.Range(-0.1f, 0.1f);
        GetComponent<Rigidbody2D>().velocity *= ran_delta_v0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = GetComponent<Rigidbody2D>().velocity;
        if (v.magnitude >= bullet_v1)
            GetComponent<Rigidbody2D>().velocity = v.normalized*(v.magnitude - bullet_A * Time.deltaTime);
    }
}
