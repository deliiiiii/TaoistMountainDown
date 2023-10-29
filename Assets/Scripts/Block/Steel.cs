using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steel : Block
{
    private void Awake()
    {
        isBombable = isTearable = false;
    }
}
