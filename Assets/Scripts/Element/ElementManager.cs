using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    public static ElementManager instance;
    private void Awake()
    {
        instance = this;
    }

    
}
