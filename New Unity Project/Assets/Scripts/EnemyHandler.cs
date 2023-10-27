using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    Rigidbody2D rb;
    
    [Header("Stats")]
    public Stats enemyStats;
    float health;
    float attack;
    float speed;

    [Header("Recoil")]
    [SerializeField]float recoilLength;
    [SerializeField]float recoilFactor;
    [SerializeField]float recoilTimer;
    [SerializeField]bool isRecoiling;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        updateStats();
    }

    void Update() 
    {
        Die();
        recoil();
    }
    
    void Die()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void EnemyHit(float damageDone, Vector2 hitDirection, float hitForce)
    {
        health -= damageDone;
        if(!isRecoiling)
        {
            rb.AddForce(-hitForce * recoilFactor * hitDirection);
        }
    }

    void recoil()
    {
        if(isRecoiling)
        {
            if(recoilTimer < recoilLength)
            {
                recoilTimer += Time.deltaTime;
            }
            else
            {
                isRecoiling = false;
                recoilTimer = 0;
            }
        }
    }
    void updateStats()
    {
        health = enemyStats.health;
        attack = enemyStats.attack;
        speed = enemyStats.speed;
    }
}
