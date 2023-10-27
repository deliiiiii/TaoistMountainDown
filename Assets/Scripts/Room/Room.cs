using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Room : MonoBehaviour
{
    public int pos_x;//小地图坐标(1,1)
    public int pos_y;
    public Vector3 pos_world;//中心点的世界坐标
    //public List<Door> doors = new();
    //public List<Block> blocks = new();
    //public List<Item> items = new();
    public List<Bullet> existing_bullet = new();
    public List<Character> existing_enemy = new();
    //public List<ObservableValue<int>> state_door = new() { new(0,5), new(0, 5), new(0, 5), new(0, 5), new(0, 5) };
    public bool hasExplored = false;
    public enum ROOMTYPE
    {
        initial,
        battle,
        item,
        treasure,
        boss,
    };
    public ROOMTYPE type;
}
