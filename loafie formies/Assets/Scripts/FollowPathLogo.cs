using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathLogo : MonoBehaviour
{
    public float objectScale;
    public GameObject path;
    public GameObject player;
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
            if (point.transform.childCount > 0)
            {
                GameObject newObject = Instantiate(gameObject);
                FollowPath newPath = newObject.GetComponent<FollowPath>();
                newPath.path = point;
                currentPoint++;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, point.transform.position, speed);
                if (transform.position == path.transform.GetChild(currentPoint).transform.position)
                {
                    currentPoint++;
                }
            }

        }

        if (currentPoint == 2)
        {
            player.transform.localScale = new Vector3(-objectScale, objectScale, 1);
        }
    }
}