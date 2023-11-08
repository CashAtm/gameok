using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [HideInInspector] public PlayerStateList pState;
    private Rigidbody2D rb;
    public Stats pStat;
    
    private float xAxis, yAxis;
    public static PlayerHandler Instance;
    float health;
    float attackStat;
    float speed;
    
    [Header("Ground Check Settings")]
    [SerializeField]private float jumpForce;
    [SerializeField]private Transform groundCheckPoint;
    [SerializeField]private float groundCheckY = 0.2f;
    [SerializeField]private float groundCheckX = 0.5f;
    [SerializeField]private LayerMask whatIsGround;

    [Header("Attack Settings")]
    bool attack;
    float timeBetweenAttack, timeSinceAttack;
    [SerializeField] Transform SideAttackTransform;
    [SerializeField] Transform UpAttackTransform;
    [SerializeField] Transform DownAttackTransform;
    [SerializeField] Vector2 SideAttackArea;
    [SerializeField] Vector2 UpAttackArea;
    [SerializeField] Vector2 DownAttackArea;
    [SerializeField] LayerMask attackableLayer;

    [Header("Recoil")]
    [SerializeField] int recoilXSteps = 5;
    [SerializeField] int recoilYSteps = 5;
    [SerializeField] float recoilXSpeed = 100;
    [SerializeField] float recoilYSpeed = 100;
    
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        updateStats();

    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Move();
        Jump();
        Flip();
        Attack();
        Die();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        attack = Input.GetMouseButtonDown(0);
    }

    void updateStats()
    {
        health = pStat.health;
        attackStat = pStat.attack;
        speed = pStat.speed;
    }
    void Die()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(speed * xAxis, rb.velocity.y);
    }

    private void Jump()
    {
        /*
        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        */
        if (Input.GetButtonDown("Jump") && Grounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }
    }

    public bool Grounded()
    {
        if(Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround) || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckX, whatIsGround) ||  Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckX, whatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Flip()
    {
        if(xAxis < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            
        }
        else if(xAxis > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);

        }
    }

    void Attack()
    {
        timeSinceAttack += Time.deltaTime;
        if(attack && timeSinceAttack >= timeBetweenAttack)
        {
            timeSinceAttack = 0;
            
            if(yAxis == 0 || yAxis < 0 && Grounded())
            {
                Hit(SideAttackTransform, SideAttackArea);
            }
            else if(yAxis > 0)
            {
                Hit(UpAttackTransform, UpAttackArea);
            }
            else if(yAxis < 0 && !Grounded())
            {
                Hit(DownAttackTransform, DownAttackArea);
            }
        }
    }

    void Recoil()
    {
        if(pState.recoilingX)
        {
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(SideAttackTransform.position, SideAttackArea);
        Gizmos.DrawWireCube(UpAttackTransform.position, UpAttackArea);
        Gizmos.DrawWireCube(DownAttackTransform.position, DownAttackArea);
    }
    
    private void Hit(Transform _attackTransform, Vector2 _attackArea)
    {
        Collider2D[] objectsToHit = Physics2D.OverlapBoxAll(_attackTransform.position, _attackArea, 0, attackableLayer);

        if(objectsToHit.Length > 0)
        {
            Debug.Log("hit");
        }
        for(int i = 0; i < objectsToHit.Length; i++)
        {
            if(objectsToHit[i].GetComponent<EnemyHandler>() != null)
            {
                objectsToHit[i].GetComponent<EnemyHandler>().EnemyHit(attackStat, (transform.position - objectsToHit[i].transform.position).normalized, 100);
            }
        }
    }
    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0 , pStat.health);
    }

    public void TakeDamage (float damage)
    {
        health -= Mathf.RoundToInt(damage);
        StartCoroutine(StopTakingDamage());
    }
    IEnumerator StopTakingDamage()
    {
        pState.invicible = true;
        ClampHealth();
        yield return new WaitForSeconds(1f);
        pState.invicible = false; 
    }
}
