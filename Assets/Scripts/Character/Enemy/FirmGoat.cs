using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirmGoat : DaydreamGhost
{
    public override void Skill_1_Procedure_1()
    {
        skill_1_ShotCount++;
        bulletDirection = (PlayerManager.instance.currentPlayer.transform.position - transform.position).normalized;

        float rad_emitAngle = emitAngle * 3.1415926f / 180.0f;
        
        for (int i = 0; i < skill_1_ShotAtOneTime; i++)
        {
            float ran_deltaAngle = UnityEngine.Random.Range(-rad_emitAngle / 2, rad_emitAngle / 2);
            Vector2 trueDirection = new(bulletDirection.x * Mathf.Cos(ran_deltaAngle) + bulletDirection.y * Mathf.Sin(ran_deltaAngle),
                                        -bulletDirection.x * Mathf.Sin(ran_deltaAngle) + bulletDirection.y * Mathf.Cos(ran_deltaAngle));
            floorManager.instance.GenerateBullet(gameObject, trueDirection);
            ////将bulletDirection逆时针旋转 emitAngle/2 的角度
            //trueDirection = new Vector2(trueDirection.x * Mathf.Cos(rad_emitAngle / (skill_1_ShotAtOneTime - 1)) - trueDirection.y * Mathf.Sin(rad_emitAngle / (skill_1_ShotAtOneTime - 1)),
            //                            trueDirection.x * Mathf.Sin(rad_emitAngle / (skill_1_ShotAtOneTime - 1)) + trueDirection.y * Mathf.Cos(rad_emitAngle / (skill_1_ShotAtOneTime - 1)));
        }


    }
}
