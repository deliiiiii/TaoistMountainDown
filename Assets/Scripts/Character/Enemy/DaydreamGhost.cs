using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaydreamGhost : Enemy
{
    [Header("散弹次数")]
    public int skill_1_ShotTimes;
    [Header("散弹个数")]
    public int skill_1_ShotAtOneTime;
    [Header("发射子弹的分散角")]
    public float emitAngle = 90f;

    protected int skill_1_ShotCount = 0;
    protected Vector2 bulletDirection = Vector2.zero;
    protected override void Awake()
    {
        base.Awake();
        skillFuncs.Add(delegate { StartSkill_1(); });
    }
    private void FixedUpdate()
    {
        EmitSkill();
    }
    public override void EmitSkill()
    {
        base.EmitSkill();
    }
    public virtual void StartSkill_1()
    {
        if (skill_emit[1] == false)
        {
            skill_emit[1] = true;
            readyToShoot.SetActive(true);
            Invoke(nameof(StartSkill_1), shootPreTime);
            return;
        }
       
        if (skill_1_ShotCount >= skill_1_ShotTimes)
        {
            //Debug.Log("EndSkill_1");
            EndSkill_1();
            return;
        }
        if((skill_1_ShotCount == 0 && CheckNear(transform.position, PlayerManager.instance.currentPlayer.transform.position, skill_range[1]))
           || skill_1_ShotCount != 0)
        {
            Skill_1_Procedure_1();
            Invoke(nameof(StartSkill_1), skill_usingMaxTime[1] / skill_1_ShotTimes);
            return;
        }
        Invoke(nameof(StartSkill_1), 0.02f);
    }
    public virtual void Skill_1_Procedure_1()
    {
        skill_1_ShotCount++;
        bulletDirection = (PlayerManager.instance.currentPlayer.transform.position - transform.position).normalized;

        float rad_emitAngle = emitAngle * 3.1415926f / 180.0f;
        Vector2 trueDirection = new Vector2(bulletDirection.x * Mathf.Cos(rad_emitAngle / 2) + bulletDirection.y * Mathf.Sin(rad_emitAngle / 2),
                                            -bulletDirection.x * Mathf.Sin(rad_emitAngle / 2) + bulletDirection.y * Mathf.Cos(rad_emitAngle / 2));
        for(int i = 0; i < skill_1_ShotAtOneTime; i++)
        {
            RoomManager.instance.GenerateBullet(gameObject, trueDirection);
            //将bulletDirection逆时针旋转 emitAngle/2 的角度
            trueDirection = new Vector2(trueDirection.x * Mathf.Cos(rad_emitAngle / (skill_1_ShotAtOneTime-1)) - trueDirection.y * Mathf.Sin(rad_emitAngle / (skill_1_ShotAtOneTime - 1)),
                                        trueDirection.x * Mathf.Sin(rad_emitAngle / (skill_1_ShotAtOneTime - 1)) + trueDirection.y * Mathf.Cos(rad_emitAngle / (skill_1_ShotAtOneTime - 1)));
        }
        

    }
    public void EndSkill_1()
    {
        readyToShoot.SetActive(false);
        skill_1_ShotCount = 0;
        skill_emit[1] = false;
        skill_usingTimer[1] = 0;
        skill_loadTimer[1] = 0;

        bulletDirection = Vector2.zero;
    }
}
