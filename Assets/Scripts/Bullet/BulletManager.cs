using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;
    public List<Bullet> prefabs_bullet = new();
    public List<Bullet> existing_bullet = new();
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    public void Initialize()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            prefabs_bullet.Add(transform.GetChild(i).GetComponent<Bullet>());
        }
    }
    
}
