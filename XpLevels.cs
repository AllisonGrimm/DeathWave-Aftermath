using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpLevels : MonoBehaviour
{
    public static void UpdateXp(Stats target)
    {
        //updates total xp needed xp doesn't reset to 0 when level up
        switch (target.level)
        {
            case 1:
                target.expNext = 100;
                break;
            case 2:
                target.expNext = 210;
                break;
            case 3:
                target.expNext = 331;
                break;
            case 4:
                target.expNext = 464;
                break;
            case 5:

                break;
            case 6:

                break;
            case 7:

                break;
            case 8:

                break;
            case 9:

                break;
            case 10:

                break;
            case 11:

                break;
            case 12:

                break;
            case 13:

                break;
            case 14:

                break;
            case 15:

                break;
            case 16:

                break;
            case 17:

                break;
            case 18:

                break;
            case 19:

                break;
            case 20:

                break;
        }
    }
}
