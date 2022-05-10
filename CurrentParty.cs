using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewParty", menuName = "Party")]
public class CurrentParty : ScriptableObject
{
    public List<Stats> myParty = new List<Stats>();
}
