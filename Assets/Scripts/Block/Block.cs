using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Block : MonoBehaviour
{
    public int index = -1;
    public int size_x = -1;
    public int size_y = -1;
    public int pos_x = -1;
    public int pos_y = -1;
    public bool? isBombable = null;
    public bool? isTearable = null;
    public int HP = -1;
    public List<Sprite> sprite_HP = new();
    public void MDamage(int damage)
    {
        HP -= damage;
        Refresh();
    }
    void Refresh()
    {
        if (HP <= 0)
        {
            HP = 0;
            DisableAllCollider();
            //RoomManager.instance.currentRoom.Value.existing_block.Remove(this);
            //Destroy(gameObject);
        }
        GetComponent<SpriteRenderer>().sprite = sprite_HP[HP];
    }
    public void DisableAllCollider()
    {
        if (GetComponent<BoxCollider2D>() != null)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        if (GetComponent<CircleCollider2D>() != null)
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
