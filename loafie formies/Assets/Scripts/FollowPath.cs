using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public GameObject path;
    public float speed;
    private int currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPoint < path.transform.childCount)
        {
            GameObject point = path.transform.GetChild(currentPoint).gameObject;
            transform.position = Vector3.MoveTowards(transform.position, point.transform.position, speed);
            if (point.transform.childCount > 0)
            {
                GameObject newObject = Instantiate(gameObject);
                FollowPath newPath = newObject.GetComponent<FollowPath>();
                newPath.path = point;
                currentPoint++;
            }
            else if (transform.position == path.transform.GetChild(currentPoint).transform.position)
            {
                currentPoint++;
            }
        }
    }
}
