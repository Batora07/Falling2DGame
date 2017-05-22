﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject fallingBlockPrefab;
	public Vector2 secondsBetweenSpawnsMinMax;
	float nextSpawnTime;

	public Vector2 spawnSizeMinMax;
	public float spawnAngleMax;

	Vector2 screenHalfSizeWorldUnits;

	// Use this for initialization
	void Start () {
		screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawnTime)
		{
			float secondsBetweenSpawns = Mathf.Lerp(secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x, Difficulty.GetDifficultyPercent());
			nextSpawnTime = Time.time + secondsBetweenSpawns;

			float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
			float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
			// spawn the block in the upper border of the screen
			Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), screenHalfSizeWorldUnits.y + spawnSize);
			// instantiate a new block and apply a random rotation, and random scale on it
			GameObject newBlock = (GameObject)Instantiate(fallingBlockPrefab, spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
			// moves the block
			newBlock.transform.localScale = Vector2.one * spawnSize;
		}
	}
}
