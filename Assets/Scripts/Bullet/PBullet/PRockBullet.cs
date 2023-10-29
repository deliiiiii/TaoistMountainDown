using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRockBullet : PBullet
{
    public Block stone;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        
        int pos_this_x = Mathf.RoundToInt(transform.localPosition.x+0.5f);
        int pos_this_y = Mathf.RoundToInt(transform.localPosition.y+0.5f);
        //Debug.Log("Rock Pos : " + pos_this_x + " , " + pos_this_y);
        if (collision.gameObject.CompareTag("Wall"))
        {
            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                    if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_this_x, pos_this_y))
                    {
                        RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_this_x, pos_this_y);
                        dy = dx = 2;
                    }
            RomoveBullet();
        }
            

        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Character>().MDanage(bulletDamage);

            Vector2 delta = transform.position - collision.transform.position;
            int dx = delta.x >= 0 ? 1 : -1;
            int dy = delta.y >= 0 ? 1 : -1;
            //将敌人的位置四舍五入
            int pos_x = Mathf.RoundToInt(collision.transform.localPosition.x+0.5f);
            int pos_y = Mathf.RoundToInt(collision.transform.localPosition.y + 0.5f);

            //Debug.Log("Rock - Enemy pos_x :"+pos_x+" pos_y :"+pos_y);
            float angle = Mathf.Atan2(delta.y, delta.x)*180/3.1415926f;
            if(angle >= 45f && angle <= 135f || (angle+180f) >= 45f && (angle + 180f) <= 135f)
            {
                if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_x + 0, pos_y + 0))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_x + 0, pos_y + 0);
                else if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_x +0, pos_y+dy))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_x + 0, pos_y + dy);
                else if(!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_x + dx, pos_y + dy))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_x + dx, pos_y + dy);
                else if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_x + dx, pos_y + 0))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_x + dx, pos_y + 0);
            }
            else
            {
                if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_x + 0, pos_y + 0))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_x + 0, pos_y + 0);
                else if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_x + dx, pos_y + 0))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_x + dx, pos_y + 0);
                else if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_x + dx, pos_y + dy))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_x + dx, pos_y + dy);
                else if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_x + 0, pos_y + dy))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_x + 0, pos_y + dy);
            }
            RomoveBullet();
        }
        else if (collision.gameObject.CompareTag("Block"))
        {
            Block block = collision.gameObject.GetComponent<Block>();
            if (block.isTearable == true)
                collision.gameObject.GetComponent<Block>().MDamage(1);
            Vector2 v = GetComponent<Rigidbody2D>().velocity;
            //在子弹的来向上生成一个石头
            float angle = Mathf.Atan2(v.y, v.x) * 180 / 3.1415926f;
            int dx = v.x >= 0 ? -1 : 1;
            int dy = v.y >= 0 ? -1 : 1;
            if (angle >= 45f && angle <= 135f || (angle + 180f) >= 45f && (angle + 180f) <= 135f)
            {
                if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_this_x + 0, pos_this_y + 0))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_this_x + 0, pos_this_y + 0);
                else if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_this_x + 0, pos_this_y + dy))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_this_x + 0, pos_this_y + dy);
                else if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_this_x + dx, pos_this_y + dy))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_this_x + dx, pos_this_y + dy);
                else if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_this_x + dx, pos_this_y + 0))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_this_x + dx, pos_this_y + 0);
            }
            else
            {
                if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_this_x + 0, pos_this_y + 0))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_this_x + 0, pos_this_y + 0);
                else if(!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_this_x + dx, pos_this_y + 0))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_this_x + dx, pos_this_y + 0);
                else if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_this_x + dx, pos_this_y + dy))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_this_x + dx, pos_this_y + dy);
                else if (!RoomManager.instance.currentRoom.Value.CheckExistBlock(pos_this_x + 0, pos_this_y + dy))
                    RoomManager.instance.currentRoom.Value.GenerateBlock(stone, pos_this_x + 0, pos_this_y + dy);
            }
            RomoveBullet();
        }
    }
}
