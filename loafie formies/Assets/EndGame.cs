using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            GameObject.Find("MainCamera").GetComponent<CameraFollow>().target = null;
            other.gameObject.GetComponent<PlayerController>().m_Direction = Vector3.down;
            other.gameObject.GetComponent<PlayerController>().speed = 5;
            new WaitForSeconds(3);
            SceneManager.LoadScene(0);
        }
    }
}
