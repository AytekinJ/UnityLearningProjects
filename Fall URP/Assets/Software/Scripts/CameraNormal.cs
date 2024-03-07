using UnityEngine;

public class CameraNormal : MonoBehaviour
{
    public Transform target;
	public Vector3 offset;
	public float smooth;  

	Vector3 velocity = Vector3.zero;
	void FixedUpdate()
	{
        if(target != null)
        {
		    Vector3 movePosition = new Vector3(transform.position.x, target.position.y) + offset;
		    transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, smooth);
        }
	}
}
