﻿using UnityEngine;
using System.Collections;

public class BallCamera : MonoBehaviour 
{
  public static bool FollowBall = true;

  private BallScript ball;

  private float speed = 0.07f;
  private float speedBoost = 1f;
  private float directionX, directionSign;

  void Start()
  {
    ball = FindObjectOfType<BallScript> ();
    ball.OnShoot += () => 
    {
      speedBoost = 10f;
    };
    ball.BallReset += () =>  
    {
      speedBoost = 1f;
      this.transform.position = new Vector3(ball.transform.position.x, this.transform.position.y, this.transform.position.z);
    };
  }

	void Update () 
  {
    if (ball == null || FollowBall == false)
      return;

    directionX -= 0.1f * directionSign;
    if (directionSign > 0f && directionX < 0f)
      directionX = 0f;
    else if (directionSign < 0f && directionX > 0f)
      directionX = 0f;

    speedBoost -= 0.1f;
    if (speedBoost < 1f)
      speedBoost = 1f;

    // Rect on screen. If ball is outside, follow it.
    Vector3 viewportCoords = Camera.main.WorldToViewportPoint (ball.transform.position);

    const float zoneSize = 0.1f;
   
    if (viewportCoords.x < (0.5f - zoneSize)) 
    {
      directionX = -1f;
      directionSign = -1f;
    } 
    else if (viewportCoords.x > (0.5f + zoneSize)) 
    {
      directionX = 1f;
      directionSign = 1f;
    }

    float currentX = this.transform.position.x + (directionX * speed * speedBoost);

    this.transform.position = new Vector3 (currentX, this.transform.position.y, this.transform.position.z);
	}
}