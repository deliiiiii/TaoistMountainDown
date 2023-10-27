using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<Enemy> prefabs_enemy = new();
    private void Awake()
    {
        instance = this;
    }
    public void Initialize()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            prefabs_enemy.Add(transform.GetChild(i).GetComponent<Enemy>());
        }
    }
}
