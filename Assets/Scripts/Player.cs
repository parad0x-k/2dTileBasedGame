using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters;
using System.Security;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 1.0f;
    [SerializeField] float jumpSpeed =  5.0f;
    [SerializeField] float climbSpeed = 1f;
    [SerializeField] Vector2 deathKick = new Vector2(0f, 5f);
    float gravityScaleAtStart;

    bool isAlive = true;

    Rigidbody2D player;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Animator myAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = player.gravityScale;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!isAlive) { return; }

        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
        Death();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, player.velocity.y);
        player.velocity = playerVelocity;

        bool isRunning = Mathf.Abs(player.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", isRunning);
    }

    private void Jump()
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))  { return; }
           
        if(Input.GetButtonDown("Jump"))
        {
            Vector2 jumpFact = new Vector2(0f, jumpSpeed);
            player.velocity += jumpFact;
        }
    }

    private void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(player.velocity.x) > Mathf.Epsilon;
        if(playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(player.velocity.x), 1.0f);
        }
    }

    private void ClimbLadder()
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myAnimator.SetBool("Climbing", false);
            player.gravityScale = gravityScaleAtStart;
            return;
        }
        float climbControl = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(player.velocity.x, climbSpeed * climbControl);
        player.velocity = climbVelocity;
        player.gravityScale = 0f;

        bool isClimbing = Mathf.Abs(player.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", isClimbing);
    }

    private void Death()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Anamolies"))) 
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
        }
    }

}
