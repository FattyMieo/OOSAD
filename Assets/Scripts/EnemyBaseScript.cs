using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseScript : MonoBehaviour
{
	public enum Type
	{
		ENEMY_01 = 0,
		ENEMY_02,
		ENEMY_03,
		TOTAL,
		NONE
	}

	public Type type = Type.ENEMY_01;
	public bool isToBeDestroyed = false;

	public float speed;

	// Use this for initialization
	// ! This doesn't get called, as it is 
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	public void Update ()
    {
		if(transform.position.y < -5.6f)
		{
			isToBeDestroyed = true;
		}

		if(isToBeDestroyed)
		{
			GameManagerScript.Instance.score += 100;
			SoundManagerScript.Instance.PlaySFX(AudioClipID.SFX_ATTACK);
			gameObject.SetActive(false);
			isToBeDestroyed = false;
			//Destroy(gameObject);
		}
	}
}
