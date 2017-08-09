using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	[Header("Developer")]
	public Rigidbody2D rb;
	public Collider2D coll;
	private bool isFirstInitialize = true;
	private bool isReinitialized = false;

	[Header("Debug Panel")]
	public float fireSpeed;
	public string ownerTag;

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
			transform.position.x > SpawnManagerScript.Instance.camXExtend ||
			transform.position.x < -SpawnManagerScript.Instance.camXExtend ||
			transform.position.y > SpawnManagerScript.Instance.camYExtend ||
			transform.position.y < -SpawnManagerScript.Instance.camYExtend
		)
		{
			gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other)
	{
		if(ownerTag == "Player" && other.GetComponent<EnemyManagerScript>())
		{
			EnemyManagerScript enemy = other.GetComponent<EnemyManagerScript>();

			if(!enemy.isInv)
			{
				SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_HIT);
				enemy.Damage();
				gameObject.SetActive(false);
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
			}
		}
	}
}
