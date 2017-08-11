using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    float maxJumpVelocity;
    float minJumpVelocity;
    Vector2 directionalInput;
    bool wallSliding;
    int wallDirX;

    internal Controller2D controller;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timetoJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timetoJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
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
		if(other.gameObject.tag == "Player"){
        Vector2 input = new Vector2(other.transform.position.x - transform.position.x, 0);
        input.Normalize();
        directionalInput = input;
		}
	}
    void OnTriggerStay2D(Collider2D other)
    {
		if(other.gameObject.tag == "Player"){
        Vector2 input = new Vector2(other.transform.position.x - transform.position.x, 0);
        
		if(Mathf.Abs(input.x) > 4){
			input.Normalize();
        	directionalInput = input;
		}
		else
			directionalInput = Vector2.zero;
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
