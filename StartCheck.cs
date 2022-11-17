using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCheck : MonoBehaviour
{
    [SerializeField] private Quests loidHat;
    [SerializeField] private LoidHat loid;
    void Update()
    {
        if(loidHat.started)
        {
            loid.started = true;
        }
    }
}
