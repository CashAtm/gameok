using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public Stats test;
    
    // Start is called before the first frame update
    void Start()
    {
        test = (Stats)ScriptableObject.CreateInstance(typeof(Stats));
    }

    void update {
        // if hit by attack, check damage stat
        // if 0 then die
        // otherwise subtract damage from health
    }
}
