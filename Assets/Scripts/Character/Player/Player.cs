using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Bullet PBullet;
    public Bullet PFireBullet;
    public Bullet PWaterBullet;
    public Bullet PMudBullet;
    public Bullet PRockBullet;
    public Bullet PWindBullet;
    public Bullet PMeltBullet;
    public Dictionary<Element.TYPE, Bullet> dic_bullet;

    [Header("子弹射击CD")]
    public float bulletShootCD;
    [Header("高级子弹射击CD")]
    public float advancedBulletShootCD;
    private float bulletShootTimer = 9999f;

    [Header("吸收技能CD")]
    public float bulletAbsorbCD;
    public float bulletAbsorbTimer = 9999f;
    [Header("已吸收的元素")]
    public List<Element> list_absorb_elem = new();
    [HideInInspector]
    public ObservableValue<int> seleted_elem = new(-1, "seleted_elem");

    public GameObject absorb_circle;
    public GameObject weapon;
    public GameObject weaponHole;
    private float stillVelocity = 0.1f;//小于这个速度则视为静止
    
    public enum STATE
    {
        Idling,
        Moving,
        Dead,
        ChangingRoom,
    };
    [SerializeField]
    private STATE currentState;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHP = maxHP;
        dic_bullet = new()
        {
            { Element.TYPE.None, PBullet},
            {Element.TYPE.Fire, PFireBullet},
            {Element.TYPE.Water, PWaterBullet},
            {Element.TYPE.Mud, PMudBullet},
            {Element.TYPE.Rock, PRockBullet},
            {Element.TYPE.Wind, PWindBullet},
            {Element.TYPE.Melt, PMeltBullet},
        };
        bullet = dic_bullet[Element.TYPE.None];

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Room room = collision.GetComponent<Room>();
        if (room)
        {
            RoomManager.instance.currentRoom.Value = room;
            if (room.type == Room.ROOMTYPE.initial || room.type == Room.ROOMTYPE.destination)
                return;
            if (room.isExplored)
                return;
            
            room.isExplored = true;
            room.GenerateEnemy();
            room.SetDoorState(true);
        }
    }

    protected void RotateWeapon()
    {
        bulletDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float rotate_z = Mathf.Atan(bulletDirection.y / bulletDirection.x) * 180f / 3.1415926f;
        if (bulletDirection.x < 0)
            rotate_z += 180f;
        weapon.transform.rotation = Quaternion.Euler(new(0, 0, rotate_z));
    }

    public virtual void InputMove()
    {
        if (currentState == STATE.Dead || currentState == STATE.ChangingRoom)
        {
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
            currentState = STATE.Moving;
            SetAnim_Move("Move");
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
            currentState = STATE.Moving;
            SetAnim_Move("Move");
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            currentState = STATE.Moving;
            SetAnim_Move("Move");
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            currentState = STATE.Moving;
            SetAnim_Move("Move");
        }

        if (currentState == STATE.Moving)
        {
            if (!IsStill())
            {
                FrictionSlowDown();
                return;
            }
            else
            {
                rb.velocity = Vector2.zero;
                currentState = STATE.Idling;
                SetAnim_Move(null);
                //anim.SetTrigger("Idle");
            }
        }
    }
    public virtual void InputShoot()
    {
        float true_bulletShootCD = bulletShootCD;
        if(seleted_elem.Value == 0)
            if (IsAdvancedElem(list_absorb_elem[0].type))
                true_bulletShootCD = advancedBulletShootCD;
        if (bulletShootTimer < true_bulletShootCD)
        {
            bulletShootTimer += Time.deltaTime;
            return;
        }
        if (Input.GetMouseButton(0)) 
        {
            bulletDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

            //Debug.Log("player pos = " + transform.position);
            //Debug.Log("target = " + target);
            //Debug.Log("bulletDirection = " + bulletDirection);

            RoomManager.instance.GenerateBullet(gameObject, bulletDirection);
            if(seleted_elem.Value != -1)
            {
                list_absorb_elem[seleted_elem.Value].amount.Value--;
            }
            bulletShootTimer = 0;
            //Debug.Log("bulletVelocity = " + newBullet.GetComponent<Rigidbody2D>().velocity);

            //Debug.Log("Player pos : " + transform.position);
            //Debug.Log("Mouse pos : " + target);
        }
        
    }
    public virtual void InputSkill()
    {
        if (bulletAbsorbTimer < bulletAbsorbCD)
        {
            bulletAbsorbTimer += Time.deltaTime;
        }
        else if(Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(1))//吸收
        {
            absorb_circle.SetActive(true);
            bulletAbsorbTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.E)) //合成
        {
            Element adv_elem = BindElement();
            if(adv_elem != null)
            {
                Debug.Log("Bind Elem = " +  adv_elem.type);
                list_absorb_elem.RemoveAt(0);
                list_absorb_elem.RemoveAt(0);
                list_absorb_elem.Add(adv_elem);
                list_absorb_elem[0].amount.Value += Element.dic_elem[adv_elem.type];
                seleted_elem.Value = -1;
                seleted_elem.Value = 0;
                //UIManager.instance.RefreshElementUI();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)) //选中元素1
        {
            if(list_absorb_elem.Count >= 1)
            {
                if (seleted_elem.Value == -1 || seleted_elem.Value == 1)
                    seleted_elem.Value = 0;
                else if (seleted_elem.Value == 0)
                    seleted_elem.Value = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) //选中元素2
        {
            if (list_absorb_elem.Count >= 2)
            {
                if (seleted_elem.Value == -1 || seleted_elem.Value == 0)
                    seleted_elem.Value = 1;
                else if(seleted_elem.Value == 1)
                    seleted_elem.Value = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
            RoomManager.instance.currentRoom.Value.GenerateEnemy(EnemyManager.instance.prefabs_enemy[0]);
        if (Input.GetKeyDown(KeyCode.Keypad2))
            RoomManager.instance.currentRoom.Value.GenerateEnemy(EnemyManager.instance.prefabs_enemy[1]);
        if (Input.GetKeyDown(KeyCode.Keypad3))
            RoomManager.instance.currentRoom.Value.GenerateEnemy(EnemyManager.instance.prefabs_enemy[2]);
    }
    public void RefreshSelectedBullet()
    {
        Element.TYPE type;
        if (seleted_elem.Value == -1)
            type = Element.TYPE.None;
        else
            type = list_absorb_elem[seleted_elem.Value].type;
        bullet = dic_bullet[type];
        UIManager.instance.selected_elem.text = type.ToString();
    }
    public bool AbsorbElement(Element.TYPE type)
    {
        int index_elem = ExistElement(type);
        if(index_elem != -1)
        {
            if(IsAdvancedElem(list_absorb_elem[0].type))
                return false;
            if (list_absorb_elem[index_elem].amount.Value >= Element.dic_elem[type])
                return false;
            if (type == Element.TYPE.Water)
                list_absorb_elem[index_elem].amount.Value += Element.dic_elem[type]/5;
            else
                list_absorb_elem[index_elem].amount.Value++;
            if (list_absorb_elem[index_elem].amount.Value > Element.dic_elem[type])
                list_absorb_elem[index_elem].amount.Value = Element.dic_elem[type];
            //Debug.Log("Absorbed " + type + " Count = " + list_absorb_elem[index_elem].amount.Value);

        }
        else
        {
            if (list_absorb_elem.Count == 2)
                return false;
            list_absorb_elem.Add(new Element(type));
            if (type == Element.TYPE.Water)
                list_absorb_elem[index_elem].amount.Value += Element.dic_elem[type] / 5;
            else
                list_absorb_elem[index_elem].amount.Value++;
            //Debug.Log("Absorbed " + type + " Count = " + list_absorb_elem[list_absorb_elem.Count - 1].amount.Value);

        }
        return true;
    }
    public Element BindElement()
    {
        if(list_absorb_elem.Count < 2)
            return null;
        for(int i = 0; i < 2;i++)
        {
            if (list_absorb_elem[i].amount.Value < Element.dic_elem[list_absorb_elem[i].type])
                return null;
        }
        if(list_absorb_elem[0].type == Element.TYPE.Fire)
        {
            if (list_absorb_elem[1].type == Element.TYPE.Water)
                return new(Element.TYPE.Wind);
            if (list_absorb_elem[1].type == Element.TYPE.Mud)
                return new(Element.TYPE.Melt);
        }
        if (list_absorb_elem[0].type == Element.TYPE.Water)
        {
            if (list_absorb_elem[1].type == Element.TYPE.Fire)
                return new(Element.TYPE.Wind);
            if (list_absorb_elem[1].type == Element.TYPE.Mud)
                return new(Element.TYPE.Rock);
        }
        if (list_absorb_elem[0].type == Element.TYPE.Mud)
        {
            if (list_absorb_elem[1].type == Element.TYPE.Fire)
                return new(Element.TYPE.Melt);
            if (list_absorb_elem[1].type == Element.TYPE.Water)
                return new(Element.TYPE.Rock);
        }
        return null;
    }
    public int ExistElement(Element.TYPE type)
    {
        for(int i = 0;i<list_absorb_elem.Count;i++)
        {
            if (list_absorb_elem[i].type == type)
                return i;
        }
        return -1;
    }
    public bool IsAdvancedElem(Element.TYPE type)
    {
        if (type == Element.TYPE.Fire || type == Element.TYPE.Water || type == Element.TYPE.Mud)
            return false;
        return true;
    }
    public bool IsStill()
    {
        if(rb.velocity.magnitude <= stillVelocity)
        {
            return true;
        }
        return false;
    }
    public void SetAnim_Move(string name)
    {
        //anim.SetBool("Move_W", false);
        //anim.SetBool("Move_S", false);
        //anim.SetBool("Move_A", false);
        //anim.SetBool("Move_D", false);
        anim.SetBool("Move", false);
        if (name != null)
            anim.SetBool(name, true);
    }
}
