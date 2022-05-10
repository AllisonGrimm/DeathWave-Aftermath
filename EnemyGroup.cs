using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyGroup", menuName = "Enemy")]
public class EnemyGroup : ScriptableObject
{
    public List<EnemyStats> enemyGroup = new List<EnemyStats>();
}
