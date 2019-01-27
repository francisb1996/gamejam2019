using UnityEngine;

public class Friend : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 2;
    public Vector3 offset;
    public int followDistance;

    private float facing;
    private Transform parent;

    private bool attached = false;

    private void Start()
    {
        parent = transform.parent;
        facing = parent.localScale.x;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!attached)
        {
            target = other.transform;
            other.transform.GetComponent<AudioSource>().Play();
            attached = true;
        }
    }

    private void FixedUpdate()
    {
        if (target && (Vector3.Distance(target.position, parent.position) > followDistance)) {
            if (target.position.x > parent.position.x)
            {
                parent.localScale = new Vector3(-facing, Mathf.Abs(facing), 1);
            }
            else if (target.position.x < parent.position.x)
            {
                parent.localScale = new Vector3(facing, Mathf.Abs(facing), 1);
            }
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(parent.position, desiredPosition, smoothSpeed * Time.deltaTime);
            parent.position = smoothedPosition;
        }
    }
}
