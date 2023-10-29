using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public class Element
{
    public enum TYPE
    {
        None,   //无
        Fire,   //火
        Water,  //水
        Mud,    //土
        Rock,   //岩
        Wind,   //风
        Melt    //熔
    };
    public static Dictionary<TYPE, int> dic_elem= new ()
    {
        {TYPE.Fire, 20},
        {TYPE.Water, 20},
        {TYPE.Mud, 20},
        {TYPE.Rock, 20},
        {TYPE.Wind, 5},
        {TYPE.Melt, 10},
    };
    public Element(TYPE type)
    {
        this.type = type;
        this.amount.Value = 0;
    }
    public TYPE type;
    public ObservableValue <int> amount = new(0,"Element");


}
