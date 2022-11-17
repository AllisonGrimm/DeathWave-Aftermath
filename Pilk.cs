using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilk : MonoBehaviour
{
    public static void Apply(InventoryItem pilk)
    {
        pilk.boxText += pilk.target.memberName + " gains three stacks of adrenalin and three stacks of 10% health regen ";
        pilk.target.adrenStacks = 3;
        pilk.target.adrenalin = true;
        pilk.target.regenStacks = 3;
        pilk.target.regen = true;
        pilk.target.regenAmount = Mathf.RoundToInt(pilk.target.hpMax * 0.1f);
    }
}
