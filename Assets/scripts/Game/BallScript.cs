﻿using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour 
{
  public int lastTeamTouch;

  private Rigidbody rbody;
  private Vector3 startPosition;
	

  void Awake()
  {
    rbody = GetComponent<Rigidbody> ();

    startPosition = this.transform.position;
  }

  public void Reset()
  {
    this.transform.position = startPosition;
		lastTeamTouch = 0;

    rbody.velocity = Vector3.zero;
  }


  void OnCollisionEnter(Collision other){
    var col = other.collider;
    if (col.tag != "Player")
      return;
    var p = col.GetComponent<PlayerScript> ();
    lastTeamTouch = p.team;
    Debug.Log ("poc by ");

  }
}