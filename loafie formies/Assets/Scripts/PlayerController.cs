using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public int speed;

    public int maxSpeed;

    public float objectScale;

    public float speedBuffTime;
    public float speedDebuffTime;
    public bool isSpeedBuffed;
    public bool isSpeedDebuffed;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    public GameObject player;
    public GameObject anglerLightObj;
    public Light anglerLight;

    void Start()
    {
        isSpeedBuffed = false;
        
        this.transform.localScale = new Vector3(objectScale, objectScale, 1);
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        anglerLightObj = player.transform.Find("Angler Light").gameObject;
        anglerLight = anglerLightObj.GetComponent<Light>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        if(moveHorizontal > 0)
        {
            this.transform.localScale = new Vector3(-objectScale, objectScale, 1);
        }
        else if(moveHorizontal < 0)
        {
            this.transform.localScale = new Vector3(objectScale, objectScale, 1);
        }

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical/2);


        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);


    }

    void Update()
    {
        if (isSpeedBuffed)
        {
            speedBuffTime -= Time.deltaTime;
            if (speedBuffTime <= 0.0f)
            {
                speed = 5;
                isSpeedBuffed = false;
            }
        }
        else if (isSpeedDebuffed)
        {
            speedDebuffTime -= Time.deltaTime;
            if (speedDebuffTime <= 0.0f)
            {
                speed = 5;
                isSpeedDebuffed = false;
            }
        }
    }

    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("SpeedBuff"))
        {
            other.gameObject.SetActive(false);
            isSpeedBuffed = true;
            speedBuffTime = 3.0f;
            speed = speed * 5;
        }
        else if (other.gameObject.CompareTag("SpeedDebuff"))
        {
            other.gameObject.SetActive(false);
            isSpeedDebuffed = true;
            speedDebuffTime = 3.0f;
            speed = speed / 2;
        }
    }
}