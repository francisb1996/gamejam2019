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
    private ParticleSystem swimEffect;
    private ParticleSystemForceField swimForce;

    public ParticleSystemForceFieldShape m_Shape = ParticleSystemForceFieldShape.Sphere;
    public float m_StartRange = 5.0f;
    public float m_EndRange = 20.0f;
    public Vector3 m_Direction = Vector3.zero;
    public float m_Gravity = 0.0f;
    public float m_GravityFocus = 1.0f;
    public float m_RotationSpeed = 0.0f;
    public float m_RotationAttraction = 0.0f;
    public Vector2 m_RotationRandomness = Vector2.zero;
    public float m_Drag = 0.0f;
    public bool m_MultiplyDragByParticleSize = true;
    public bool m_MultiplyDragByParticleVelocity = true;

    public GameObject player;
    public GameObject anglerLightObj;
    public Light anglerLight;

    void Start()
    {
        isSpeedBuffed = false;
        
        this.transform.localScale = new Vector3(objectScale, objectScale, 1);
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swimForce = GetComponent<ParticleSystemForceField>();
        swimEffect = GetComponent<ParticleSystem>();
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

            m_Gravity = 0.7f;

            ChangeSwimForceFieldGravity();
        }
        else if(moveHorizontal < 0)
        {
            this.transform.localScale = new Vector3(objectScale, objectScale, 1);

            m_Gravity = 0.7f;

            ChangeSwimForceFieldGravity();
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

        if (!Input.anyKey)
        {
            m_Gravity = 0f;
            ChangeSwimForceFieldGravity();
        }
    }

    private void ChangeSwimForceFieldGravity()
    {
        swimForce = GetComponent<ParticleSystemForceField>();
        swimForce.gravity = m_Gravity;
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