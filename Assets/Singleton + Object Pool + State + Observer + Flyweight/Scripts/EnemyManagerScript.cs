using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerScript : MonoBehaviour
{
	public enum Type
	{
		ENEMY_01,
		ENEMY_02,
		ENEMY_03,

		BOSS,

		TOTAL
	}

	public Type type;

	[Header("Developer")]
	public Rigidbody2D rb;
	public Collider2D coll;
	public Animator anim;
	public GameObject player;
	private bool isFirstInitialize = true;

	[Header("Debug Panel")]
	public bool isInv;
	public float invTimer;
	public float currentHealth;

	[Header("Settings")]
	public float speed;
	public float maxHealth;
	public float invDuration;

	// Use this for initialization
	void Awake ()
	{
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
		anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");

		isInv = false;
		invTimer = 0.0f;
		currentHealth = maxHealth;
		isFirstInitialize = false;
	}

	void OnDisable()
	{
		if(!isFirstInitialize)
		{
			rb.velocity = Vector2.zero;
		}
	}

	void OnEnable()
	{
		if(!isFirstInitialize)
		{
			isInv = false;
			invTimer = 0.0f;
			currentHealth = maxHealth;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.rotation = Extension.RotateTowards(transform.position, player.transform.position, -90.0f);

		anim.SetBool("IsInv", isInv);

		if(isInv)
		{
			invTimer += Time.deltaTime;
			if(invTimer >= invDuration)
			{
				isInv = false;
				invTimer = 0.0f;
			}
		}
	}

	void OnCollisionStay2D (Collision2D other)
	{
		if(other.collider.GetComponent<SpaceshipManagerScript>())
		{
			SpaceshipManagerScript player = other.collider.GetComponent<SpaceshipManagerScript>();

			if(!player.isInv && !isInv)
			{
				player.Damage();
				Damage();
				SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_HIT);
			}
		}
	}

	public void Damage()
	{
		if(!isInv)
		{
			currentHealth--;

			if(currentHealth <= 0)
			{
				AchievementManagerScript.Instance.AddMonsterSlay(type);
				switch(type)
				{
					case Type.ENEMY_01:
						AchievementManagerScript.Instance.AddScore(10);
						break;
					case Type.ENEMY_02:
						AchievementManagerScript.Instance.AddScore(50);
						break;
					case Type.ENEMY_03:
						AchievementManagerScript.Instance.AddScore(30);
						break;
				}
				SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_EXPLOSION);
				gameObject.SetActive(false);
				return;
			}

			isInv = true;
		}
	}
}
