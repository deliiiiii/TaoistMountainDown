using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using static UnityEditor.Progress;

public class Room : MonoBehaviour
{
    public int length, width;//房间大小
    //public int pos_x;//初始房间小地图坐标:(1,1)
    //public int pos_y;
    //public Vector3 pos_world;//房间中心位置的世界坐标
    //public List<Door> doors = new();
    //public List<Block> blocks = new();
    //public List<Item> items = new();
    public List<Bullet> existing_bullet = new();
    public List<Character> existing_enemy = new();
    public List<Block> existing_block = new();
    public List<GameObject> existing_door = new();
    //public List<ObservableValue<int>> state_door = new() { new(0,5), new(0, 5), new(0, 5), new(0, 5), new(0, 5) };
    public bool isExplored = false;
    public enum ROOMTYPE
    {
        initial,
        destination,
        fire_water,
        fire_mud,
        water_mud,
        fire_water_mud,
    };
    public ROOMTYPE type;
    public int enemyCount = 0;
    [Header("生成敌人的随机数偏差 20/2 = 10  => [7,13]")]
    public int delta_random = 3;
    private void Awake()
    {
        //pos_world = new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, transform.position.z);
        for(int i=0;i<transform.childCount;i++)
        {
            existing_door.Add(transform.GetChild(i).gameObject);
            existing_door[^1].SetActive(false);
        }
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
        RoomManager.instance.currentRoom.Value.SetDoorState(true);
    }
    public void GenerateEnemy()
    {
        int count_enemyType = 2;
        if (type == ROOMTYPE.fire_water)
        {
            int c1 = UnityEngine.Random.Range(enemyCount/ count_enemyType - delta_random, enemyCount / count_enemyType + delta_random + 1);
            if(c1 < 0) c1 = enemyCount / count_enemyType;
            int c2 = enemyCount - c1;
            for (int i = 0; i < c1; i++)
                GenerateEnemy(EnemyManager.instance.prefabs_enemy[0]);
            for (int i = 0; i < c2; i++)
                GenerateEnemy(EnemyManager.instance.prefabs_enemy[1]);
        }
        else if (type == ROOMTYPE.fire_mud)
        {
            int c1 = UnityEngine.Random.Range(enemyCount / count_enemyType - delta_random, enemyCount / count_enemyType + delta_random + 1);
            if (c1 < 0) c1 = enemyCount / count_enemyType;
            int c2 = enemyCount - c1;
            for (int i = 0; i < c1; i++)
                GenerateEnemy(EnemyManager.instance.prefabs_enemy[0]);
            for (int i = 0; i < c2; i++)
                GenerateEnemy(EnemyManager.instance.prefabs_enemy[2]);
        }
        else if(type == ROOMTYPE.water_mud)
        {
            int c1 = UnityEngine.Random.Range(enemyCount / count_enemyType - delta_random, enemyCount / count_enemyType + delta_random + 1);
            if (c1 < 0) c1 = enemyCount / count_enemyType;
            int c2 = enemyCount - c1;
            for (int i = 0; i < c1; i++)
                GenerateEnemy(EnemyManager.instance.prefabs_enemy[1]);
            for (int i = 0; i < c2; i++)
                GenerateEnemy(EnemyManager.instance.prefabs_enemy[2]);
        }
        else if (type == ROOMTYPE.fire_water_mud)
        {
            count_enemyType = 3;
            int c1 = UnityEngine.Random.Range(enemyCount / count_enemyType - delta_random, enemyCount / count_enemyType + delta_random + 1);
            if (c1 < 0) c1 = enemyCount / count_enemyType;
            int c2 = UnityEngine.Random.Range(enemyCount / count_enemyType - delta_random, enemyCount / count_enemyType + delta_random + 1);
            if (c2 < 0) c2 = enemyCount / count_enemyType;
            int c3 = enemyCount - c1 - c2;
            for (int i = 0; i < c1; i++)
                GenerateEnemy(EnemyManager.instance.prefabs_enemy[0]);
            for (int i = 0; i < c2; i++)
                GenerateEnemy(EnemyManager.instance.prefabs_enemy[1]);
            for (int i = 0; i < c3; i++)
                GenerateEnemy(EnemyManager.instance.prefabs_enemy[2]);
        }
    }
    public void SetDoorState(bool isOpen)
    {
        for(int i=0;i<existing_door.Count;i++)
        {
            existing_door[i].SetActive(isOpen);
        }
    }
}
