using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSample : MonoBehaviour
{
    public Transform target;
	public Vector3 offset; //The Z value should be -5, and it should be changed from within the game engine.
	public float smooth;  

	Vector3 velocity = Vector3.zero;
	void FixedUpdate()
	{
		Vector3 movePosition = target.position + offset;
		transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, smooth);
	}
}
