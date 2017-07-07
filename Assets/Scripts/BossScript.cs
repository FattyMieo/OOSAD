using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
	public enum State
	{
		PATROL = 0,
		RANGED_ATK1,
		RANGED_ATK2,
		MELEE_ATK
	}

	[Header("Attributes")]
	public State curState;
	State prevState;
	public int actionStep = 0;

	[Header("Target")]
	public GameObject target;

	[Header("Settings")]
	public float moveSpeed;
	public float moveRange;
	public float shootRange;

	public float meleeRange;
	public float meleeDashSpeed;

	public float shootTimer;
	public float shootDuration;
	int shootAmount;

	// Use this for initialization
	void Start ()
	{
		curState = State.PATROL;
		prevState = State.MELEE_ATK;
		shootAmount = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch(curState)
		{
			case State.RANGED_ATK1:
				Move();

				if(shootTimer > 0.0f)
					shootTimer -= Time.deltaTime;
				else if(target.transform.position.x > transform.position.x - shootRange && target.transform.position.x < transform.position.x + shootRange)
				{
					Shoot((EnemyBaseScript.Type)Random.Range(0, (int)EnemyBaseScript.Type.TOTAL));
					shootTimer = shootDuration;
					shootAmount++;
				}

				if(shootAmount >= 10)
				{
					shootTimer = 0.0f;
					shootAmount = 0;
					ChangeState(State.PATROL);
				}
				break;
			case State.RANGED_ATK2:
				Move();
				
				if(shootTimer > 0.0f)
					shootTimer -= Time.deltaTime;
				else if(target.transform.position.x > transform.position.x - shootRange && target.transform.position.x < transform.position.x + shootRange)
				{
					for(int i = 0; i < 2; i++)
					{
						Shoot((EnemyBaseScript.Type)Random.Range(0, (int)EnemyBaseScript.Type.TOTAL));
					}
					shootTimer = shootDuration;
					shootAmount++;
				}

				if(shootAmount >= 5)
				{
					shootTimer = 0.0f;
					shootAmount = 0;
					ChangeState(State.PATROL);
				}
				break;
			case State.MELEE_ATK:
				switch(actionStep)
				{
					case 0:
						transform.Translate(Vector3.down * meleeDashSpeed * Time.deltaTime);
						if(transform.position.y < -3.5f && transform.position.y < target.transform.position.y - 1.5f) actionStep++;
						break;
					case 1:
						transform.Translate(Vector3.up * meleeDashSpeed * 0.5f * Time.deltaTime);
						if(transform.position.y > 3.0f) ChangeState(State.PATROL);
						break;
				}
				break;
			case State.PATROL:
			default:
				Move();

				if(target.transform.position.x > transform.position.x - meleeRange && target.transform.position.x < transform.position.x + meleeRange)
				{
					if(prevState != State.MELEE_ATK) ChangeState(State.MELEE_ATK);
					else ChangeState((State)Random.Range((int)State.RANGED_ATK1, (int)State.RANGED_ATK2 + 1));
				}
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
		if(target.transform.position.x < transform.position.x - moveRange)
			transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
		else if(target.transform.position.x > transform.position.x + moveRange)
			transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
	}

	void Shoot(EnemyBaseScript.Type type)
	{
		GameObject tmpGO = SpawnManagerScript.Instance.Spawn(type);
		tmpGO.transform.position = transform.position;
		tmpGO.GetComponent<EnemyBaseScript>().speed = Random.Range(1.0f, 5.0f);
	}
}
