using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] private GameObject tank;
	[SerializeField] private GameObject healer;
	[SerializeField] private GameObject flagBearer;
	[SerializeField] private GameObject sniper;
	[SerializeField] private GameObject barbarian;

	[SerializeField] private int enemyResources;

	private float timer;
	private float resourcesPerSecond;

	private float spawnTimer;
	private float timeToSpawn;


	// Use this for initialization
	void Start() 
	{
		enemyResources = 100;
		resourcesPerSecond = 4;

		timer = 0.0f;
		spawnTimer = 0.0f;
		timeToSpawn = 2.0f;
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (timer > (1.0 / resourcesPerSecond))
		{
			enemyResources++;

			timer = 0.0f;
		}

		if (spawnTimer >= timeToSpawn)
		{
			GenerateUnits();
			spawnTimer = 0.0f;
		}

		timer += Time.deltaTime;
		spawnTimer += Time.deltaTime;
	}

	public void GenerateUnits()
	{
		int rand = (int)Mathf.Floor(Random.value * 5.0f) + 1;
		GameObject createType = null;
		if (rand == 1)
		{
			createType = tank;
		}
		else if (rand == 2)
		{
			createType = healer;
		}
		else if (rand == 3)
		{
			createType = flagBearer;
		}
		else if (rand == 4)
		{
			createType = sniper;
		}
		else if (rand == 5)
		{
			createType = barbarian;
		}
		if (createType != null && enemyResources >= createType.GetComponent<UnitScript>().unitCost)
		{
			createType.GetComponent<UnitGeneralBehavior>().goesRight = false;
			int randBase = (int)Mathf.Floor(Random.value * transform.childCount);

			GameObject selectedBase = gameObject.transform.GetChild(randBase).gameObject;
			Instantiate(createType, selectedBase.transform.position, Quaternion.identity);

			enemyResources -= createType.GetComponent<UnitScript>().unitCost;
		}
	}
}
