using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public Stats enemyStats;
    public float health;
    public float attack;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        updateStats();
    }

    void Update() 
    {
        updateStats();
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void EnemyHit(float damageDone)
    {
        health -= damageDone;
    }

    void updateStats()
    {
        health = enemyStats.health;
        attack = enemyStats.attack;
        speed = enemyStats.speed;
    }
}
