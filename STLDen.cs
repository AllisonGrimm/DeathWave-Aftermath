using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STLDen : MonoBehaviour
{
    [SerializeField] private SceneTransitionBattle transition;
    [SerializeField] private EnemyLoad load;
    [SerializeField] private EnemyGroup e1;
    [SerializeField] private EnemyGroup e2;
    [SerializeField] private EnemyGroup e3;
    [SerializeField] private string last;

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("Player"))
        {
            int encounter = Random.Range(1, 101);
            if (encounter > 94)
            {
                SelectEncounter();
                //encounters something
            }
        }
    }

    private void SelectEncounter()
    {
        int encounter = Random.Range(1, 101);
        if (encounter < 33)
        {
            load.group = e1;
        }
        else if (encounter < 66)
        {
            load.group = e2;
        }
        else if (encounter < 101)
        {
            load.group = e3;
        }
        load.lastScene = last;
        load.dugeonFight = true;
        transition.BattleTransition();
    }
}
