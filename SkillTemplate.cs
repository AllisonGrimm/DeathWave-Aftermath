using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/Skill")]
public class SkillTemplate : ScriptableObject
{
    public string skillName;
    public string description;
    public string boxText;
    public int ammoUse;
    public int energyUse;
    public bool targetAlly;
    public bool noTarget;

    public Stats user;
    public Stats target;
    public EnemyStats eTarget;
    public EnemyGroup group;

    public UnityEvent skill;
    public GameObject animation;

    public BattleResults results;

    public void Use()
    {
        skill.Invoke();
    }
}
