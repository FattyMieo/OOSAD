using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossAIScript : MonoBehaviour
{
	public enum State
	{
		PATROL = 0,
		RANGED_ATK,
		SPAWN_ENEMIES,
		MELEE_ATK
	}

	[Header("Developer")]
	public EnemyManagerScript self;
//	private bool isFirstInitialize = true;

	[Header("State")]
	public State curState;
	State prevState;
	public int actionStep = 0;

	[Header("Debug Panel")]
	public float cooldownTimer;
	public float angle;
	public Vector3 patrolPoint;
	public float currentRadius;

	[Header("Settings")]
	public float shootDuration;
	public float spawnDuration;
	public Transform fireSpot;
	public float fireSpeed;
	public float rotationRadius;
	public float rotationSpeed;

	public float moveRange;

	public float shootRange;
	int shootAmount;

	public float meleeRange;
	public float meleeDashSpeed;

	// Use this for initialization
	void Start ()
	{
		self = GetComponent<EnemyManagerScript>();
		
		cooldownTimer = 0.0f;
//		isFirstInitialize = false;

		curState = State.PATROL;
		prevState = State.MELEE_ATK;
		shootAmount = 0;
	}

	// Update is called once per frame
	void Update ()
	{
		switch(curState)
		{
			case State.RANGED_ATK:
				Move();

				if(cooldownTimer > 0.0f)
				{
					cooldownTimer -= Time.deltaTime;
				}
				else
				{
					for(int i = 0; i < 2; i++)
					{
						BulletScript newBullet = SpawnManagerScript.Instance.SpawnBullet().GetComponent<BulletScript>();
						newBullet.transform.position = fireSpot.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0.0f);
						newBullet.transform.rotation = transform.rotation;
						newBullet.fireSpeed = fireSpeed;
						newBullet.ownerTag = tag;
						newBullet.ChangeColor(self.bodySprite.color);
					}

					cooldownTimer = shootDuration;
					shootAmount++;
				}

				if(shootAmount >= 10)
				{
					cooldownTimer = 0.0f;
					shootAmount = 0;
					ChangeState(State.PATROL);
				}
				break;
			case State.SPAWN_ENEMIES:
				if(SpawnManagerScript.Instance.enemyList.Count > 10)
				{
					ChangeState(State.PATROL);
					cooldownTimer = 0.0f;
					shootAmount = 0;
				}

				if(cooldownTimer > 0.0f)
				{
					cooldownTimer -= Time.deltaTime;
				}
				else
				{
					GameObject newEnemy = SpawnManagerScript.Instance.Spawn(EnemyManagerScript.Type.ENEMY_03);
					newEnemy.transform.position = transform.position + transform.up;

					cooldownTimer = spawnDuration;
					shootAmount++;
				}

				if(shootAmount >= 2)
				{
					cooldownTimer = 0.0f;
					shootAmount = 0;
					ChangeState(State.PATROL);
				}
				break;
			case State.MELEE_ATK:
				Vector3 dir = transform.position - self.player.transform.position;
				dir.Normalize();
				transform.Translate(dir * meleeDashSpeed * Time.deltaTime);
				if(Vector3.Distance(transform.position, self.player.transform.position) <= meleeRange)
					ChangeState(State.PATROL);
				break;
			case State.PATROL:
			default:
				Move();
				State newState;
				do
				{
					newState = (State)Random.Range((int)State.RANGED_ATK, (int)State.MELEE_ATK + 1);
				}
				while(newState == prevState);

				ChangeState(newState);
				break;
		}
	}

	void ChangeState(State state)
	{
		prevState = curState;
		curState = state;
		actionStep = 0;

		angle = (Mathf.Atan2(transform.position.y, transform.position.x) - Mathf.Atan2(1, 0)) * Mathf.Rad2Deg + 90.0f;
	}

	void Move()
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
	}
}
