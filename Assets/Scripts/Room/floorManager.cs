using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorManager : MonoBehaviour
{
    public static floorManager instance;
    private GameObject currentGrid = null;
    public GameObject floor;
    public List<Room> existing_room = new();
    public ObservableValue<Room> currentRoom = new(new(), "Room");
    //[SerializeField]
    //private float interval_x = 24;
    //[SerializeField]
    //private float interval_y = 24;
    //private int min_x;
    //private int min_y;
    //private int max_x;
    //private int max_y;
    //private int max_distance = -1;

    public List<GameObject> prefab_grids;
    //public GameObject room_default;
    private void Awake()
    {
        instance = this;
    }
    public void Initialize()
    {
        ReadFloor();
        
        //GenerateRoom(Room.ROOMTYPE.initial);
        //int ran = /*UnityEngine.Random.Range(1, 2)*/1;
        //switch (ran)
        //{
        //    case 1:
        //        {
        //            for (int i = 0; i < 3; i++)
        //            {
        //                GenerateRoom(Room.ROOMTYPE.battle);
        //            }
        //            for (int i = 0; i < 2; i++)
        //            {
        //                GenerateRoom(Room.ROOMTYPE.item);
        //            }
        //            if (!GenerateRoom(Room.ROOMTYPE.boss))
        //            {
        //                Debug.Log("Generate Boss Room Failed!");
        //                GenerateFloor();
        //                return;
        //            }
        //            break;
        //        }

        //    default:
        //        break;
        //}

       
        //CameraController.instance.CallRefreshPosition();
        
        //RefreshDoorState();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        StartFloor(1);
    //    }
    //}
    void ClearFloor()
    {
        if(currentGrid)
            Destroy(currentGrid);
        existing_room.Clear();
    }
    private void ReadFloor()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            prefab_grids.Add(transform.GetChild(i).gameObject);
        }
    }
    public void StartFloor(int index)
    {
        ClearFloor();
        currentGrid = Instantiate(prefab_grids[index], floor.transform);
        currentGrid.SetActive(true);
        for (int i = 0; i < currentGrid.transform.childCount;i++)
            if(currentGrid.transform.GetChild(i).GetComponent<Room>())
            existing_room.Add(currentGrid.transform.GetChild(i).GetComponent<Room>());
        currentRoom.Value = existing_room[0];
        PlayerManager.instance.currentPlayer.transform.position = new Vector3(currentRoom.Value.length / 2, currentRoom.Value.width / 2, 0f);
    }
    //private void ClearFloor()
    //{
    //    for (int i = 1; i < grid.transform.childCount; i++)
    //    {
    //        Destroy(grid.transform.GetChild(i).gameObject);
    //    }
    //    existing_room.Clear();
    //    //min_x = min_y = max_x = max_y = 0;
    //    //max_distance = -1;
    //}
    //private bool GenerateRoom(Room.ROOMTYPE roomType)
    //{
    //    if (roomType == Room.ROOMTYPE.initial)
    //    {
    //        existing_room.Add(Instantiate(room_default, new Vector3(0 * interval_x, 0 * interval_y, 0), Quaternion.identity, grid.transform).GetComponent<Room>());
    //        //existing_room[^1].pos_x = 0;
    //        //existing_room[^1].pos_y = 0;
    //    }
    //    //else
    //    //{
    //    //    int ran_x = -1;
    //    //    int ran_y = -1;
    //    //    int max_trial = 66;
    //    //    while (max_trial > 0)
    //    //    {
    //    //        max_trial--;
    //    //        ran_x = UnityEngine.Random.Range(min_x - 1, max_x + 2);
    //    //        ran_y = UnityEngine.Random.Range(min_y - 1, max_y + 2);
    //    //        //Debug.Log("RandomRoom Trial :" + max_trial + " (" + ran_x + "," + ran_y + ")");
    //    //        if (RoomDistance(ran_x, ran_y, 0, 0) < max_distance * 0.65f)
    //    //            continue;
    //    //        if (roomType == Room.ROOMTYPE.boss && RoomDistance(ran_x, ran_y, 0, 0) < max_distance - 2)
    //    //            continue;
    //    //        if (roomType == Room.ROOMTYPE.boss && RoomDistance(ran_x, ran_y, 0, 0) <= 2)
    //    //            continue;
    //    //        if (FindRoom(ran_x, ran_y))
    //    //            continue;

    //    //        int direction;
    //    //        int count_exit = 0;
    //    //        for (direction = 1; direction <= 4; direction++)
    //    //        {
    //    //            Room hasFoundRoom = FindRoom(ran_x + dir_or_index_door[0, direction], ran_y + dir_or_index_door[1, direction]);
    //    //            if (hasFoundRoom != null)
    //    //            {
    //    //                count_exit++;
    //    //                if (hasFoundRoom.type == Room.ROOMTYPE.item)
    //    //                {
    //    //                    count_exit = 0;
    //    //                    break;
    //    //                }
    //    //            }

    //    //        }
    //    //        if (count_exit == 0)
    //    //            continue;
    //    //        if (count_exit >= 2 && (roomType == Room.ROOMTYPE.item || roomType == Room.ROOMTYPE.boss))
    //    //            continue;
    //    //        else
    //    //            break;
    //    //    }
    //    //    if (max_trial == 0)
    //    //        return false;
    //    //    Debug.Log("New Room : " + " (" + ran_x + "," + ran_y + ")" + roomType);
    //    //    existing_room.Add(Instantiate(room_default, new Vector3(ran_x * 30, ran_y * 18, 0), Quaternion.identity, grid.transform).GetComponent<Room>());
    //    //    existing_room[^1].pos_x = ran_x;
    //    //    existing_room[^1].pos_y = ran_y;
    //    //    min_x = Mathf.Min(min_x, ran_x);
    //    //    min_y = Mathf.Min(min_y, ran_y);
    //    //    max_x = Mathf.Max(max_x, ran_x);
    //    //    max_y = Mathf.Max(max_y, ran_y);
    //    //    max_distance = Mathf.Max(max_distance, RoomDistance(ran_x, ran_y, 0, 0));
    //    //}

    //    existing_room[^1].type = roomType;
    //    existing_room[^1].gameObject.SetActive(true);

    //    return true;
    //}
    public Bullet GenerateBullet(GameObject user, Vector2 bulletDirection/*, Bullet newBullet*/)
    {
        Bullet newBullet = Instantiate(user.GetComponent<Character>().bullet, currentRoom.Value.transform);
        GameObject go_newBullet = newBullet.gameObject;
        go_newBullet.SetActive(true);

        go_newBullet.transform.localScale = new Vector3(newBullet.bulletSize, newBullet.bulletSize, 1);
        if (user.GetComponent<Player>())
            newBullet.transform.position = user.GetComponent<Player>().weaponHole.transform.position;
        else
            newBullet.transform.position = user.GetComponent<Character>().transform.position;
        newBullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * newBullet.GetComponent<Bullet>().bulletSpeed;

        //go_newBullet.GetComponent<Bullet>().bulletExistTime = newBullet.bulletExistTime;

        currentRoom.Value.existing_bullet.Add(newBullet);
        return newBullet;
    }
}
