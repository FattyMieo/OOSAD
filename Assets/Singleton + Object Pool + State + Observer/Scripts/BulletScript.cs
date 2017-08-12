using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public enum Type
	{
		BULLET = 0,
		MEDKIT,

		TOTAL
	}

	[Header("Developer")]
	public Rigidbody2D rb;
	public Collider2D coll;
	public SpriteRenderer bodySprite;
	private bool isFirstInitialize = true;
	private bool isReinitialized = false;
	private bool hasHit = false;

	[Header("Debug Panel")]
	public float fireSpeed;
	public string ownerTag;

	[Header("Settings")]
	public Type type;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();

		rb.AddForce((Vector2)transform.up * fireSpeed, ForceMode2D.Impulse);
		SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_ATTACK);

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
			SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_ATTACK);
			hasHit = false;
			isReinitialized = true;
		}
	}

	void Update()
	{
		if(isReinitialized)
		{
			rb.AddForce((Vector2)transform.up * fireSpeed, ForceMode2D.Impulse);
			isReinitialized = false;
		}

		if
		(
			transform.position.x > SpawnManagerScript.Instance.camXExtend + 1.0f||
			transform.position.x < -SpawnManagerScript.Instance.camXExtend - 1.0f ||
			transform.position.y > SpawnManagerScript.Instance.camYExtend + 1.0f ||
			transform.position.y < -SpawnManagerScript.Instance.camYExtend - 1.0f
		)
		{
			gameObject.SetActive(false);
			hasHit = true;
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other)
	{
		if(!hasHit)
		{
			if(type == Type.BULLET)
			{
				if(ownerTag == "Player" && other.GetComponent<EnemyManagerScript>())
				{
					EnemyManagerScript enemy = other.GetComponent<EnemyManagerScript>();

					if(!enemy.isInv)
					{
						SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_HIT);
						enemy.Damage();
						gameObject.SetActive(false);
						hasHit = true;
					}
				}
				else if(ownerTag == "Enemy" && other.GetComponent<SpaceshipManagerScript>())
				{
					SpaceshipManagerScript player = other.GetComponent<SpaceshipManagerScript>();

					if(!player.isInv)
					{
						SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_HIT);
						player.Damage();
						gameObject.SetActive(false);
						hasHit = true;
					}
				}
			}
			else if(type == Type.MEDKIT)
			{
				if(other.GetComponent<SpaceshipManagerScript>())
				{
					SpaceshipManagerScript player = other.GetComponent<SpaceshipManagerScript>();
					player.Heal();
					gameObject.SetActive(false);
					hasHit = true;
				}
//				else if(other.GetComponent<EnemyManagerScript>())
//				{
//					EnemyManagerScript enemy = other.GetComponent<EnemyManagerScript>();
//					enemy.currentHealth += 5;
//				}
			}
		}
	}

	public void ChangeColor(Color newColor)
	{
		bodySprite.color = newColor;
	}
}
