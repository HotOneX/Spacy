﻿using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour
{
	public float dodge;
	public float smoothing;
	public float tilt;

	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneueverWait;


    public float currentSpeed;
	private Rigidbody rb;
	private float targetManeuver;
	void Start()
	{
        currentSpeed = Random.Range(3f, currentSpeed);
        rb = GetComponent<Rigidbody>();
		//currentSpeed = rb.velocity.z;
		StartCoroutine (Evade ());
	}
	IEnumerator Evade()
	{
		yield return new WaitForSeconds(Random.Range(startWait.x,startWait.y));
		while(true)
		{
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (maneueverWait.x, maneueverWait.y));
		}
	}
	void FixedUpdate()
	{
		float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManeuver, 0.0f,-currentSpeed);
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, -5.5f, 5.5f),
            rb.position.y,
            rb.position.z
		);
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
