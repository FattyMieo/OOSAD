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

				if(prevState != State.MELEE_ATK) ChangeState(State.MELEE_ATK);
				else ChangeState((State)Random.Range((int)State.RANGED_ATK, (int)State.SPAWN_ENEMIES + 1));

				break;
		}
	}

	void ChangeState(State state)
	{
		prevState = curState;
		curState = state;
		actionStep = 0;
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
