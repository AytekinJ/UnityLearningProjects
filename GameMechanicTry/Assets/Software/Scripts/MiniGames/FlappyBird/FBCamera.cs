using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FBCamera : MonoBehaviour
{
    [SerializeField] private GameObject GroundParent;
    [SerializeField] private GameObject GroundPrefab;
    [SerializeField] private GameObject PipeParent;
    [SerializeField] private GameObject PipePrefab;
    Transform target;
	public Vector3 offset;
	public float smooth;  

	Vector3 velocity = Vector3.zero;

    private void LateUpdate() {

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        
        if(target != null)
        {
            Vector3 movePosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(movePosition.x, transform.position.y, transform.position.z), ref velocity, smooth);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Ground"))
        {
            Vector2 lastGroundTransform = GroundParent.transform.GetChild(4).transform.position;
            Instantiate(GroundPrefab, new Vector3(lastGroundTransform.x + 6f, -5, 0), Quaternion.identity, GroundParent.transform);
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Wall"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Wall"))
        {
            Instantiate(PipePrefab, new Vector3(other.gameObject.transform.position.x + 5, Random.Range(3, -3), 0), Quaternion.identity, PipeParent.transform);
        }
    }
}
