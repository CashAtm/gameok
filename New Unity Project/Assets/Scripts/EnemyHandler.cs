using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    protected Rigidbody2D rb;
    
    [Header("Stats")]
    public Stats enemyStats;
    public float health;
    public float attack;
    public float speed;

    [Header("Recoil")]
    [SerializeField]protected float recoilLength;
    [SerializeField]protected float recoilFactor;
    protected float recoilTimer;
    [SerializeField]protected bool isRecoiling;

    [SerializeField]protected PlayerHandler player;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = PlayerHandler.Instance;
    }
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        updateStats();
    }

    public virtual void Update() 
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
    public virtual void EnemyHit(float damageDone, Vector2 hitDirection, float hitForce)
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
