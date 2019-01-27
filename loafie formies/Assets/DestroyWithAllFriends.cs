using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithAllFriends : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Loafie");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<NumberOfFriends>().numberOfFriends == 5)
        {
            Destroy(gameObject);
        }
    }
}
