using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterAIScript : MonoBehaviour {

	[Header("Developer")]
	public EnemyManagerScript self;
	private bool isFirstInitialize = true;

	[Header("Debug Panel")]
	public bool isCooldown;
	public float cooldownTimer;
	public float angle;
	public Vector3 patrolPoint;
	public float currentRadius;

	[Header("Settings")]
	public float cooldownDuration;
	public Transform fireSpot;
	public float fireSpeed;
	public float rotationRadius;
	public float rotationSpeed;

	// Use this for initialization
	void Start ()
	{
		self = GetComponent<EnemyManagerScript>();

		isCooldown = false;
		cooldownTimer = 0.0f;
		isFirstInitialize = false;
	}

	void OnEnable()
	{
		if(!isFirstInitialize)
		{
			isCooldown = false;
			cooldownTimer = 0.0f;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		currentRadius = Vector3.Distance(Vector3.zero, transform.position);

		if(currentRadius > rotationRadius + 0.1f)
		{
			currentRadius -= self.speed * Time.deltaTime;
		}

		angle += rotationSpeed * Time.deltaTime;

		Vector3 offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f) * currentRadius;
		patrolPoint = Vector3.zero + offset;

		self.rb.MovePosition(patrolPoint);

		if(isCooldown)
		{
			cooldownTimer += Time.deltaTime;
			if(cooldownTimer >= cooldownDuration)
			{
				isCooldown = false;
				cooldownTimer = 0.0f;
			}
		}
		else
		{
			BulletScript newBullet = SpawnManagerScript.Instance.SpawnBullet().GetComponent<BulletScript>();
			newBullet.transform.position = fireSpot.position;
			newBullet.transform.rotation = transform.rotation;
			newBullet.fireSpeed = fireSpeed;
			newBullet.ownerTag = tag;

			isCooldown = true;
		}
	}
}
