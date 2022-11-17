using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/SkillList")]
public class SkillList : ScriptableObject
{
    public List<SkillTemplate> skillList = new List<SkillTemplate>();
}
