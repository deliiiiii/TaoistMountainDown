using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightMare : Player
{
    private void FixedUpdate()
    {
        base.InputMove();
        
    }
    private void Update()
    {
        base.InputShoot();
        base.InputSkill();
    }
}
