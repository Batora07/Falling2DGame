using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 7.0f;
	public event System.Action OnPlayerDeath;

	float screenHalfWidthInWorldUnits;

	// Use this for initialization
	void Start () {

		// Allow the player to smoothly spawn from the other side of the screen
		// when going in a border
		float halfPlayerWidth = transform.localScale.x / 2f;
		screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
	}
	
	// Update is called once per frame
	void Update () {

		float inputX = Input.GetAxisRaw("Horizontal");
		float velocity = inputX * speed;
		transform.Translate(Vector2.right * velocity * Time.deltaTime);

		// if going to left border => spawn the player from the right side
		if (transform.position.x < -screenHalfWidthInWorldUnits)
		{
			transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
		}

		// the opposite from above
		if (transform.position.x > screenHalfWidthInWorldUnits)
		{
			transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
		}
	}

	void OnTriggerEnter2D(Collider2D triggerCollider)
	{
		if (triggerCollider.tag == "FallingBlock")
		{
			if(OnPlayerDeath != null)
			{
				OnPlayerDeath();
			}
			Destroy(gameObject);
		}
	}
}
