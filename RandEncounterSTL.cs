using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandEncounterSTL : MonoBehaviour
{
    [SerializeField] private SceneTransitionBattle transition;
    [SerializeField] private EnemyLoad load;
    [SerializeField] private EnemyGroup e1;
    [SerializeField] private EnemyGroup e2;
    [SerializeField] private EnemyGroup e3;
    [SerializeField] private EnemyGroup e4;
    [SerializeField] private EnemyGroup e5;
    [SerializeField] private EnemyGroup e6;
    [SerializeField] private EnemyGroup e7;
    [SerializeField] private EnemyGroup e8;
    [SerializeField] private EnemyGroup e9;
    [SerializeField] private EnemyGroup e10;
    [SerializeField] private EnemyGroup e11;
    [SerializeField] private string last;

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.CompareTag("Player"))
        {
            int encounter = Random.Range(1, 101);
            if(encounter>94)
            {
                SelectEncounter();
                //encounters something
            }
        }
    }

    private void SelectEncounter()
    {
        int encounter = Random.Range(1, 101);
        if (encounter < 15)
        {
            load.group = e1;
        }
        else if (encounter < 30)
        {
            load.group = e2;
        }
        else if (encounter < 45)
        {
            load.group = e3;
        }
        else if (encounter < 55)
        {
            load.group = e4;
        }
        else if (encounter < 65)
        {
            load.group = e5;
        }
        else if (encounter < 73)
        {
            load.group = e6;
        }
        else if (encounter < 81)
        {
            load.group = e7;
        }
        else if (encounter < 89)
        {
            load.group = e8;
        }
        else if (encounter < 93)
        {
            load.group = e9;
        }
        else if (encounter < 97)
        {
            load.group = e10;
        }
        else
        {
            load.group = e11;
        }
        load.lastScene = last;
        transition.BattleTransition();
    }
}
