using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public List<Player> prefabs_player = new();
    public Player currentPlayer;
    private void Awake()
    {
        instance = this;
        
    }

    public void Initialize()
    {
        for(int i=0;i< transform.childCount;i++)
        {
            prefabs_player.Add(transform.GetChild(i).GetComponent<Player>());
        }
        currentPlayer = prefabs_player[0];
    }
}
