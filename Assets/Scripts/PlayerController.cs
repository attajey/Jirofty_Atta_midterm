using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 500.0f;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody2D rBody;
    private Animator anim;
    public Animator chestAnim;
    private bool isGrounded = false;
    private bool isFacingRight = true;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        isGrounded = GroundCheck();

        // Jump
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            rBody.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
        }

        // Attack
        if (Input.GetKey(KeyCode.E))
        {

            chestAnim.SetBool("isOpen", true);
        }
       /* else
        {
            chestAnim.SetBool("isOpen", false);
        }*/
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isDucking", true);

        }
        else
        {
            anim.SetBool("isDucking", false);
        }

        // Move
        rBody.velocity = new Vector2(horiz * speed, rBody.velocity.y);

        // Flip check
        if ((isFacingRight && rBody.velocity.x < 0) || (!isFacingRight && rBody.velocity.x > 0))
        {
            Flip();
        }

        // Animator Comunication 
        anim.SetFloat("xSpeed", Mathf.Abs(rBody.velocity.x));
        anim.SetFloat("ySpeed", rBody.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        isFacingRight = !isFacingRight;
    }
}
