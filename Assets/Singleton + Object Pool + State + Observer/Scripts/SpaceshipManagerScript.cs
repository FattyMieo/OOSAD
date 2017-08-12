using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipManagerScript : MonoBehaviour
{
	[Header("Developer")]
	public Rigidbody2D rb;
	public Collider2D coll;
	public Animator anim;
	public SpriteRenderer bodySprite;

	[Header("Debug Panel")]
	public bool isCooldown;
	public float cooldownTimer;
	public bool isInv;
	public float invTimer;
	public int currentHealth;

	[Header("Settings")]
	public float speed;
	public int maxHealth;
	public float invDuration;
	public float cooldownDuration;
	public Transform fireSpot;
	public float fireSpeed;

	[Header("HUD")]
	public HealthHUDScript healthHUD;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
		anim = GetComponent<Animator>();

		isCooldown = false;
		cooldownTimer = 0.0f;
		isInv = false;
		invTimer = 0.0f;
		currentHealth = maxHealth;
		healthHUD.valueCount = currentHealth;
		healthHUD.maxCount = maxHealth;
		healthHUD.Notify(currentHealth, true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.rotation = Extension.RotateTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), -90.0f);

		Vector3 dir = Vector3.zero;
		dir.x = Input.GetAxis("Horizontal"); 
		dir.y = Input.GetAxis("Vertical");
		dir *= Time.deltaTime * speed;
		rb.AddForce(dir, ForceMode2D.Impulse);

		if(isCooldown)
		{
			cooldownTimer += Time.deltaTime;
			if(cooldownTimer >= cooldownDuration)
			{
				isCooldown = false;
				cooldownTimer = 0.0f;
			}
		}
		else if(Input.GetMouseButton(0))
		{
			isCooldown = true;

			BulletScript newBullet = SpawnManagerScript.Instance.SpawnBullet().GetComponent<BulletScript>();
			newBullet.transform.position = fireSpot.position;
			newBullet.transform.rotation = transform.rotation;
			newBullet.fireSpeed = fireSpeed;
			newBullet.ownerTag = tag;
			newBullet.ChangeColor(bodySprite.color);
		}

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

	public void Damage()
	{
		if(!isInv)
		{
			currentHealth--;
			healthHUD.Notify(currentHealth);

			if(currentHealth <= 0)
			{
				SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_EXPLOSION);
				gameObject.SetActive(false);
				return;
			}

			isInv = true;
		}
	}

	public void Heal()
	{
		currentHealth += 3;
		if(currentHealth > maxHealth)
			currentHealth = maxHealth;
		healthHUD.Notify(currentHealth);
	}
}
