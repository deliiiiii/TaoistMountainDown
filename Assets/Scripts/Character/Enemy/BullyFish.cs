using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BullyFish : DaydreamGhost
{
    GameObject shootLine;
    protected override void Awake()
    {
        base.Awake();
        shootLine = transform.GetChild(1).gameObject;
        shootLine.SetActive(false);
    }
    private void Update()
    {
        bulletDirection = (PlayerManager.instance.currentPlayer.transform.position - transform.position).normalized;
        float rotate_z = Mathf.Atan(bulletDirection.y / bulletDirection.x) * 180f / 3.1415926f;
        if (bulletDirection.x < 0)
            rotate_z += 180f;
        if (skill_1_ShotCount > 0)
            return;
        shootLine.transform.rotation = Quaternion.Euler(new(0, 0, rotate_z));
        //Debug.Log("rz = " + rotate_z);
        //Debug.Log("transform rz = " + shootLine.transform.localRotation.z);
    }
    public override void StartSkill_1()
    {
        if (skill_emit[1] == false)
        {
            skill_emit[1] = true;
            readyToShoot.SetActive(true);
            shootLine.SetActive(true);
            Invoke(nameof(StartSkill_1), shootPreTime);
            return;
        }

        if (skill_1_ShotCount >= skill_1_ShotTimes)
        {
            EndSkill_1();
            return;
        }
        if ((skill_1_ShotCount == 0 && CheckNear(transform.position, PlayerManager.instance.currentPlayer.transform.position, skill_range[1]))
           || skill_1_ShotCount != 0)
        {
            Skill_1_Procedure_1();
            Invoke(nameof(StartSkill_1), skill_usingMaxTime[1] / skill_1_ShotTimes);
            return;
        }
        Invoke(nameof(StartSkill_1), 0.02f);
    }
    public new void EndSkill_1()
    {
        shootLine.SetActive(false);
        base.EndSkill_1();
        
    }
}
