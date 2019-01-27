using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject anglerLightObj;
    public Light anglerLight;

    public Text gameOverText;
    public Text restartText;
    private bool gameOver;
    private bool restart;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        restartText.text = "";

        player = GameObject.Find("Player");
        anglerLightObj = player.transform.Find("Angler Light").gameObject;
        anglerLight = anglerLightObj.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anglerLight.range <= 0 && anglerLight.intensity <= 0)
        {
            GameOver();
        }
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
        restartText.text = "Pres 'R' for Restart";
        restart = true;
        player.SetActive(false);
    }
}
