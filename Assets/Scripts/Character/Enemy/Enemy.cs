using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
    private Vector2 v;//移动中的速度

    [Header("技能CD(技能释放期间后不会回复)")]
    public List<float> skill_loadCD;
    [Header("技能充能条,初始值就是技能初动")]
    public List<float> skill_loadTimer;
    [Header("技能持续时间")]
    public List<float> skill_usingMaxTime;
    [Header("技能阻回条(已经释放的时长)")]
    public List<float> skill_usingTimer;
    [Header("技能攻击范围")]
    public List<float> skill_range;
    [Header("是否正在释放技能")]
    public List<bool> skill_emit;
    protected delegate void SkillFuncs();
    protected List<SkillFuncs> skillFuncs;
    protected Vector3 target;

   
    public GameObject readyToShoot;
    [Header("攻击前摇时间")]
    public float shootPreTime = 0.2f;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        curHP = maxHP;
        skillFuncs = new() { Skill_0 };
    }
    public void Skill_0()
    {
        //anim.SetBool("Move", true);
        //anim.SetBool("Idle", false);
        Player player = PlayerManager.instance.currentPlayer;
        if (!player)
            return;
        skill_emit[0] = true;
        Vector3 delta_v;
        if (CheckNear(player.transform.position, transform.position, skill_range[0]))
        {
            //Debug.Log("Next to player : MOVE!!");
            delta_v = player.transform.position - transform.position;
            target = player.transform.position;
        }
        else
        {
            //Debug.Log("# Random MOVE!!");
            delta_v = new(Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0f);
            //transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            target = new(1e9f, 1e9f, 0f);
        }
        v = delta_v.normalized;
        transform.GetComponent<Rigidbody2D>().velocity = v * moveSpeed;
        EndSkill_0();
    }
    public void EndSkill_0()
    {
        //anim.SetBool("Idle", true);
        //anim.SetBool("Move", false);
        if (!CheckNear(transform.position, target, 0.1f) && skill_usingTimer[0] <= skill_usingMaxTime[0])
        {
            transform.GetComponent<Rigidbody2D>().velocity = v * moveSpeed;
            skill_usingTimer[0] += 0.02f;
            Invoke(nameof(EndSkill_0), 0.02f);
            return;
        }
        skill_emit[0] = false;
        skill_usingTimer[0] = 0;
        skill_loadTimer[0] = 0;
        //Invoke(nameof(StopMove), slidingTime);
    }
    public virtual void EmitSkill()//默认技能0是向玩家移动
    {
        for (int i = 0; i < skill_loadTimer.Count; i++)
        {
            skill_loadTimer[i] += Time.deltaTime;
            if (skill_loadTimer[i] >= skill_loadCD[i] && !skill_emit[i])
            {
                skillFuncs[i]();
            }
        }
    }
}
