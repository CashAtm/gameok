using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    public Stats pStat;
    private float walkSpeed = 1;
    private float xAxis;
    private float jumpForce;
    
    // Start is called before the first frame update
    void Start()
    {
        pStat = (Stats)ScriptableObject.CreateInstance(typeof(Stats));
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Move();
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
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 5f);
        }
    }
}
