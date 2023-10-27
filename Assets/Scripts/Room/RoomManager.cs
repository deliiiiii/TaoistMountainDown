using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    public List<Room> existing_room = new();
    public ObservableValue<Room> currentRoom = new(new(), "Room");
    private int min_x;
    private int min_y;
    private int max_x;
    private int max_y;
    private int max_distance = -1;

    public GameObject grid;
    public GameObject room_default;
    private void Awake()
    {
        instance = this;
    }
    public Bullet GenerateBullet(GameObject user, Vector2 bulletDirection/*, Bullet newBullet*/)
    {
        Bullet newBullet = Instantiate(user.GetComponent<Character>().bullet);
        GameObject go_newBullet = newBullet.gameObject;
        go_newBullet.SetActive(true);

        go_newBullet.transform.localScale = new Vector3(newBullet.bulletSize, newBullet.bulletSize, 1);
        newBullet.transform.position = user.transform.position;
        newBullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * newBullet.GetComponent<Bullet>().bulletSpeed;

        go_newBullet.GetComponent<Bullet>().bulletExistTime = newBullet.bulletExistTime;

        currentRoom.Value.existing_bullet.Add(newBullet);
        return newBullet;
    }
}
