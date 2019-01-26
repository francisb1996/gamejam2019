using UnityEngine;

public class Friend : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 2;
    public Vector3 offset;
    public int followDistance;

    private void OnTriggerEnter2D(Collider2D other)
    {
        target = other.transform;
        Debug.Log("Trigger");
    }

    private void FixedUpdate()
    {
        if (target && (Vector3.Distance(target.position, transform.position) > followDistance)) {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.parent.position = smoothedPosition;
        }
    }
}
