using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Controller2D))]
public class enemy : MonoBehaviour
{

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timetoJumpApex = .4f;
    public float wallSlideSpeedMax = 3;
    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;
    public float wallStickTime = .25f;
    public float timeToWallUnstick;
    float gravity;
    Vector3 velocity;
    float moveSpeed = 6;
    float velocityXSmoothing;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    public float slingshotMultiplier;
    public float attackDamage = 15;
    private float timeToAttack = 4;

    float maxJumpVelocity;
    float minJumpVelocity;
    Vector2 directionalInput;
    bool wallSliding;
    int wallDirX;
    private Rigidbody2D thisRb;
    private BoxCollider2D thisCollider;

    public GameObject body;
    private Controller2D controller;

    void Start()
    {
        //you need to add this script to a gameobject that is childed to whatever enemy
        controller = transform.parent.gameObject.GetComponent<Controller2D>();
        thisRb = GetComponent<Rigidbody2D>();
        thisCollider = GetComponent<BoxCollider2D>();
        thisCollider.isTrigger = true;
        thisCollider.size = new Vector2(25, 25);
        thisRb.isKinematic = true;
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timetoJumpApex, 2);

        print("Gravity:" + gravity + "  Jump Velocity:" + maxJumpVelocity);

    }


    void Update()
    {
        CalculateVelocity();
      

        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            else
                velocity.y = 0;
        }
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector2 input = new Vector2(other.transform.position.x - transform.position.x, 0);
            input.Normalize();
            directionalInput = input;

        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (timeToAttack >= 0)
        {
            timeToAttack -= Time.deltaTime;
        }
        if (other.gameObject.tag == "Player")
        {
            Vector2 input = new Vector2(other.transform.position.x - transform.position.x, 0);

            if (Mathf.Abs(input.x) > 0.5f)
            {
                input.Normalize();
                directionalInput = input;
            }
            else
                directionalInput = Vector2.zero;

            if (other.transform.position.x - transform.position.x <= 0.5f && timeToAttack <= 0)
            {
                Debug.Log("Enemy attacks player");
                timeToAttack = 4;
                other.gameObject.GetComponent<PlayerHealth>().decrementHealth(attackDamage);
            }
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        SetDirectionalInput(Vector2.zero);
    }



    void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = targetVelocityX;
        velocity.y += gravity * Time.deltaTime;
    }


}