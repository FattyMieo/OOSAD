using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour, IBaseNPC
{
	public GameObject selectionRing;
	Vector2 moveDirection;
	Vector2 moveTarget;
	public float moveSpeed = 1.0f;
	bool isMove = false;

	public void Move(Vector2 moveDestination)
	{
		moveTarget = moveDestination;
		moveDirection = moveDestination - (Vector2)transform.position;
		moveDirection.Normalize();
		isMove = true;
	}

	public void Selected(bool t)
	{
		selectionRing.SetActive(t);
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isMove)
		{
			if(Vector2.Distance((Vector2)transform.position, moveTarget) <= 0.1f)
			{
				isMove = false;
			}
			else
			{
				transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
			}
		}
	}
}
