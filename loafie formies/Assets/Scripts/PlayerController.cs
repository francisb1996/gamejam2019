using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public int speed;

    public int maxSpeed;

    public int friction;

    public float objectScale;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        this.transform.localScale = new Vector3(objectScale, objectScale, 1);
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d.drag = friction;
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
}