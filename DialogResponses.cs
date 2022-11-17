using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewResponses", menuName = "Dialog/DResponses")]
public class DialogResponses : ScriptableObject
{
    public List<Response> respon = new List<Response>();
}
