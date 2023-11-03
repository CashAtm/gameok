using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyHandler
{
    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 12f;
        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(!isRecoiling)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(PlayerHandler.Instance.transform.position.x, transform.position.y), speed * Time.deltaTime);
        }
    }

    public override void EnemyHit(float damageDone, Vector2 hitDirection, float hitForce)
    {
        base.EnemyHit(damageDone, hitDirection, hitForce);
    }
}
