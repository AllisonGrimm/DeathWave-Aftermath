using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificEncounter : MonoBehaviour
{
    [SerializeField] private SceneTransitionBattle transition;
    [SerializeField] private EnemyLoad load;
    [SerializeField] private EnemyGroup group;
    [SerializeField] private GameObject trigger;
    [SerializeField] private bool oneTime;
    [SerializeField] private bool dungeon;
    [SerializeField] private string last;

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("Player"))
        {
            load.group = group;
            if (dungeon)
            {
                load.dugeonFight = true;
            }
            if (oneTime)
            {
                trigger.gameObject.SetActive(false);
            }
            load.lastScene = last;
            transition.BattleTransition();
            
        }
    }
}
