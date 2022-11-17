using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRad : MonoBehaviour
{
    public static void Use(InventoryItem stats)//probably need the code to remove the icons from ui as well
    {
        stats.boxText += stats.target.memberName + " gets all negative debuffs removed ";

        if(stats.target.broken)
        {
            stats.target.broken = false;
            stats.target.brokenStacks = 0;
        }
        if(stats.target.blind)
        {
            stats.target.blind = false;
            stats.target.brokenStacks = 0;
        }
        if(stats.target.weak)
        {
            stats.target.weak = false;
            stats.target.brokenStacks = 0;
        }
        if(stats.target.stun)
        {
            stats.target.stun = false;
            stats.target.stunStacks = 0;
        }
    }
}
