using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGUA = false;
    private void Awake()
    {
        instance = this;
        
    }
    private void Start()
    {
        PlayerManager.instance.Initialize();
        EnemyManager.instance.Initialize();
        BulletManager.instance.Initialize();
        RoomManager.instance.GenerateFloor();
    }
}
