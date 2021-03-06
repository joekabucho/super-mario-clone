﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move_prototype : MonoBehaviour {
	 public int playerSpeed = 10;
	 
	 public int playerJumpPower = 1250;
     private float moveX;
	// Use this for initialization
	public bool isGround ;
	public float distanceToBottomOfPlayer=1.5f;
	
	// Update is called once per frame
	void Update () {
		PlayerMove();
		playerRaycast();
	}
	void PlayerMove()
	{
		//CONTROL
      moveX=Input.GetAxis("Horizontal");
	  if(Input.GetButtonDown ("Jump")&& isGround==true ){
		  Jump();
	  }
		//ANIMATION
		if(moveX!=0){
			GetComponent<Animator>().SetBool("isRunning",true);
		}
		else{
			GetComponent<Animator>().SetBool("isRunning",false);
		}
		//P
		if (moveX<0.0f ){
		GetComponent<SpriteRenderer>().flipX=true;
		}
		else if  (moveX>0.0f ){
		GetComponent<SpriteRenderer>().flipX=false;
		}
		//PHYSICS
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * playerSpeed,gameObject.GetComponent<Rigidbody2D>().velocity.y);
	}
	void Jump()
	{
        //jump code
		GetComponent<Rigidbody2D>().AddForce (Vector2.up * playerJumpPower);
		isGround=false;
	}
	
void OnCollisionEnter2D (Collision2D col)
	{
		
	
	}

	void playerRaycast(){
		RaycastHit2D rayUp = Physics2D.Raycast (transform.position,Vector2.up);
         if ( rayUp  != null && rayUp .collider!=null  && rayUp .distance<distanceToBottomOfPlayer && rayUp .collider.tag=="box_2" ){
          Destroy(rayUp.collider.gameObject);
		 }


		RaycastHit2D rayDown = Physics2D.Raycast (transform.position,Vector2.down);
		if ( rayDown  != null && rayDown .collider!=null  && rayDown .distance<distanceToBottomOfPlayer && rayDown .collider.tag=="enemy" )
		{
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
		      Destroy(rayDown.collider.gameObject);
			
		}
		if (rayDown .collider!=null && rayDown  != null && rayDown .distance<distanceToBottomOfPlayer && rayDown .collider.tag!="enemy" )
		{
			isGround=true;
		}
		else
       {
            isGround = false;
        }﻿
	}
}
