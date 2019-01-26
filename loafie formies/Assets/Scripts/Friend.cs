using UnityEngine;

public class Friend : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 2;
    public Vector3 offset;
    public int followDistance;

    private float facing;

    private void Start()
    {
        facing = transform.parent.localScale.x;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        target = other.transform;
    }

    private void FixedUpdate()
    {
        if (target && (Vector3.Distance(target.position, transform.position) > followDistance)) {
            if (target.position.x > transform.position.x)
            {
                transform.parent.localScale = new Vector3(-facing, Mathf.Abs(facing), 1);
            }
            else if (target.position.x < transform.position.x)
            {
                transform.parent.localScale = new Vector3(facing, Mathf.Abs(facing), 1);
            }
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.parent.position = smoothedPosition;
        }
    }
}
