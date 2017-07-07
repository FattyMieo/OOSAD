using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public float speed;
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f) * -speed * Time.deltaTime);
	}
}
