using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extension
{
	public static Quaternion RotateTowards(Vector3 selfPos, Vector3 targetPos, float angleOffset = 0.0f)
	{
		Vector3 dir = targetPos - selfPos;
		dir.Normalize();

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		return Quaternion.Euler(0.0f, 0.0f, angle + angleOffset);
	}
}
