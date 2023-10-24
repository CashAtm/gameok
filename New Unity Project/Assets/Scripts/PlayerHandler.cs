using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    public Stats pStat;
    
    [Header("Horizontal Movement Settings")]
    [SerializeField]private float walkSpeed = 1;
    private float xAxis;
    
    [Header("Ground Check Settings")]
    [SerializeField]private float jumpForce;
    [SerializeField]private Transform groundCheckPoint;
    [SerializeField]private float groundCheckY = 0.2f;
    [SerializeField]private float groundCheckX = 0.5f;
    [SerializeField]private LayerMask whatIsGround;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pStat = (Stats)ScriptableObject.CreateInstance(typeof(Stats));

    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Move();
        Jump();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        
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

    private void CheckStats()
    {
        walkSpeed = pStat.GetSpeed();
    }
}
