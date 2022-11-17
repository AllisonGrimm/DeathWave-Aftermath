using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Load")]
public class EnemyLoad : ScriptableObject
{
    public EnemyGroup group;
    public bool dugeonFight;
    public string lastScene;
}
