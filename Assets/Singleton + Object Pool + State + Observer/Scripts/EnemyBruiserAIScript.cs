using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBruiserAIScript : MonoBehaviour {

	[Header("Developer")]
	public EnemyManagerScript self;

	// Use this for initialization
	void Start ()
	{
		self = GetComponent<EnemyManagerScript>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 dir = transform.up;
		dir *= Time.deltaTime * self.speed;
		self.rb.AddForce(dir, ForceMode2D.Impulse);
	}
}
