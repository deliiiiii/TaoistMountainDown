using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using static UnityEditor.Progress;

public class Room : MonoBehaviour
{
    public int length, width;//房间大小
    public int pos_x;//小地图坐标(1,1)
    public int pos_y;
    public Vector3 pos_world;//中心点的世界坐标
    //public List<Door> doors = new();
    //public List<Block> blocks = new();
    //public List<Item> items = new();
    public List<Bullet> existing_bullet = new();
    public List<Character> existing_enemy = new();
    public List<Block> existing_block = new();
    //public List<ObservableValue<int>> state_door = new() { new(0,5), new(0, 5), new(0, 5), new(0, 5), new(0, 5) };
    public bool hasExplored = false;
    public enum ROOMTYPE
    {
        initial,
        battle,
        item,
        treasure,
        boss,
        one_connected,
        two_connected,
        three_connected,
        four_connected,
    };
    public ROOMTYPE type;
    private void Awake()
    {
        pos_world = new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, transform.position.z);
    }
    public void GenerateBlock(Block block,int x,int y)
    {
        existing_block.Add(Instantiate(block,
                        new (x,y,0f),
                        Quaternion.identity,
                        transform).GetComponent<Block>());
        existing_block[^1].gameObject.SetActive(true);
        existing_block[^1].pos_x = x;
        existing_block[^1].pos_y = y;
        existing_block[^1].transform.localPosition = new (x-0.5f, y - 0.5f, 0f);
    }
    
    public bool CheckExistBlock(int x, int y)
    {
        for (int i = 0; i < existing_block.Count; i++)
            for(int dx = 0; dx < existing_block[i].size_x; dx++)
                for(int dy = 0;dy < existing_block[i].size_y;dy++)
                    if (existing_block[i].pos_x+dx == x && existing_block[i].pos_y+dy == y)
                        return true;
        Debug.Log("Block NOT exist at (" + x + "," + y + ")");
        return false;
    }
    public void GenerateEnemy(Enemy enemy)
    {
        int x = UnityEngine.Random.Range(2,length);
        int y = UnityEngine.Random.Range(2,width);
        existing_enemy.Add(Instantiate(enemy,
                           new Vector3(x,y,0f),
                           Quaternion.identity,
                           transform).GetComponent<Enemy>());
        existing_enemy[^1].gameObject.SetActive(true);
        existing_enemy[^1].transform.localPosition = new (x - 0.5f, y - 0.5f,0f);
    }
}
