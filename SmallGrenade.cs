using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGrenade : MonoBehaviour
{
   public static void Boom(InventoryItem item)//pass in player in target in item menu, pass in enemy group in item menu as well into actual item scriptable
    {
        for(int i = 0; i<item.group.enemyGroup.Count;i++)
        {
            Damage.DamageTarget(Mathf.RoundToInt(item.target.damMin * 0.5f), Mathf.RoundToInt(item.target.damMax * 0.5f), item.group.enemyGroup[i], null, null, item.target,item.results);
            //somehow add wait
            //attempt to 50% weapon damage each enemy
        }
    }
}
