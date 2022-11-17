using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewResponses", menuName = "Dialog/Response")]
public class Response : ScriptableObject
{
    public string responseS;
    public Dialog associatedDialog;
}