using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NightMare : Player
{
    private void FixedUpdate()
    {
        base.InputMove();
        
    }
    protected void Update()
    {
        RotateWeapon();
        base.InputShoot();
        base.InputSkill();
    }
}
