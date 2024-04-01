using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCamera : MonoBehaviour
{
	Transform target;
	public Vector3 offset;
	public float smooth;  
	Vector3 velocity = Vector3.zero;

	private void Awake() {
		target = GameObject.FindGameObjectWithTag("Player") != null ? GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>() : null;
	}
	private void LateUpdate() {
        if(target != null)
        {
            Vector3 movePosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, smooth);
        }
    }
}
