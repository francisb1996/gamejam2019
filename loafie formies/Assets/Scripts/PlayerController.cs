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
    private float maxRotation = 15;
    private float rotationSpeed = 8;
    private float returnSpeed = 12;

    void Start()
    {
        isSpeedBuffed = false;
        transform.localScale = new Vector3(objectScale, objectScale, 1);
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Turn based on moving direction
        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(-objectScale, objectScale, 1);
        }
        else if(moveHorizontal < 0)
        {
            transform.localScale = new Vector3(objectScale, objectScale, 1);
        }

        bool facingLeft = transform.localScale.x > 0;
        bool facingRight = transform.localScale.x < 0;

        bool movingUp = moveVertical > 0;
        bool movingDown = moveVertical < 0;
        bool notMoving = moveVertical == 0;


        Vector3 clockwise = Vector3.back;
        Vector3 counterClockwise = Vector3.forward;

        float currentAngle = getCurrentAngle();

        //Rotate based on moving direction
        if (facingRight)
        {
            if(movingUp &&
               currentAngle < maxRotation)
            {
                transform.Rotate(counterClockwise * Time.deltaTime * rotationSpeed);
            }
            else if(movingDown &&
                    currentAngle > -maxRotation)
            {
                transform.Rotate(clockwise * Time.deltaTime * rotationSpeed);
            }
        }
        else if (facingLeft)
        {
            if (movingUp &&
               currentAngle > -maxRotation)
            {
                transform.Rotate(clockwise * Time.deltaTime * rotationSpeed);
            }
            else if (movingDown &&
                    currentAngle < maxRotation)
            {
                transform.Rotate(counterClockwise * Time.deltaTime * rotationSpeed);
            }
        }

        //Return to 0 rotation when not moving
        if(notMoving)
        {
            if (currentAngle > 0.14)
            {
                transform.Rotate(clockwise * Time.deltaTime * returnSpeed);
            }
            else if (currentAngle < -0.14)
            {
                transform.Rotate(counterClockwise * Time.deltaTime * returnSpeed);
            }
            else
            {
                transform.rotation = new Quaternion(0,0,0,0);
            }
        }

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical/2);


        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }

    float getCurrentAngle()
    {
        float angle = transform.eulerAngles.z;
        if(angle > 150)
        {
            return angle - 360;
        }
        else
        {
            return angle;
        }
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
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("SpeedBuff"))
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