using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamGhost : Enemy
{
    [Header("散弹次数")]
    public int skill_1_ShotMax;
    [Header("散弹个数")]
    public int skill_1_ShotAtOnce;
    [Header("发射子弹的分散角")]
    public float emitAngle = 90f;

    private int skill_1_ShotCount = 0;
    private Vector2 bulletDirection = Vector2.zero;
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
    void StartSkill_1()
    {
        skill_emit[1] = true;
        if (skill_1_ShotCount >= skill_1_ShotMax)
        {
            EndSkill_1();
            return;
        }
        if((skill_1_ShotCount == 0 && CheckNear(transform.position, PlayerManager.instance.currentPlayer.transform.position, skill_range[1]))
           || skill_1_ShotCount != 0)
        {
            Skill_1_Procedure_1();
            Invoke(nameof(StartSkill_1), skill_usingMaxTime[1] / skill_1_ShotMax);
            return;
        }
        Invoke(nameof(StartSkill_1), 0.02f);

    }
    void Skill_1_Procedure_1()
    {
        skill_1_ShotCount++;
        Debug.Log("ShotCount = " + skill_1_ShotCount);
        //if(bulletDirection == Vector2.zero)
        bulletDirection = (PlayerManager.instance.currentPlayer.transform.position - transform.position).normalized;

        float rad_emitAngle = emitAngle * 3.1415926f / 180.0f;
        Vector2 trueDirection = new Vector2(bulletDirection.x * Mathf.Cos(rad_emitAngle / 2) + bulletDirection.y * Mathf.Sin(rad_emitAngle / 2),
                                            -bulletDirection.x * Mathf.Sin(rad_emitAngle / 2) + bulletDirection.y * Mathf.Cos(rad_emitAngle / 2));
        for(int i = 0; i < skill_1_ShotAtOnce; i++)
        {
            Debug.Log("trueDirection = " + trueDirection);
            RoomManager.instance.GenerateBullet(gameObject, trueDirection);
            //将bulletDirection逆时针旋转 emitAngle/2 的角度
            trueDirection = new Vector2(trueDirection.x * Mathf.Cos(rad_emitAngle / (skill_1_ShotAtOnce-1)) - trueDirection.y * Mathf.Sin(rad_emitAngle / (skill_1_ShotAtOnce - 1)),
                                        trueDirection.x * Mathf.Sin(rad_emitAngle / (skill_1_ShotAtOnce - 1)) + trueDirection.y * Mathf.Cos(rad_emitAngle / (skill_1_ShotAtOnce - 1)));
        }
        

    }
    void EndSkill_1()
    {
        skill_1_ShotCount = 0;
        skill_emit[1] = false;
        skill_usingTimer[1] = 0;
        skill_loadTimer[1] = 0;

        bulletDirection = Vector2.zero;
    }
}
