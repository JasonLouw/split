using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Range(1, 10)] public float jumpVelocity;
    [Range(1, 10)] public float movementVelocity;
    public float fallMultiplier = 2.5f;//how fast the character will fall
    public float lowJumpMultiplier = 2f;//force applied down when character mini jumps
    Rigidbody2D rb;//this is character this is how you affect its forces and movement
    float gravity;
    public bool position;//this indicates whether at top or bottom of map
    public string rotation;
    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 0f;//this ensures normal game gravity doesnt influence character
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = -9.81f;
    }

    // Update is called once per frame
    void Update()
    {
        betterJump();
        movement();
        if (Input.GetKeyDown(KeyCode.S))
        {
            flip();
        }
        rotate();
        rb.AddForce(transform.up * gravity);//this is the gravity for the character
    }

    private void movement()
    {
        bool move = false;
        float x = 0f, y = 0f;
        if (Input.GetKeyDown(KeyCode.W))
        {
            //rb.velocity = Vector2.up * jumpVelocity;
            y = jumpVelocity;
        }
        if (Input.GetKey(KeyCode.D))
        {
            // rb.velocity = Vector2.up * jumpVelocity;
            x += movementVelocity;
            move = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // rb.velocity = Vector2.up * jumpVelocity;
            x += movementVelocity*-1;
            move = true;
        }

        if(move)
        {
            rb.velocity = new Vector2(x, rb.velocity.y + y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y + y);
        }
       
    }

    public void rotate()
    {
        if(rotation == "up")
        {
            float x = rb.transform.position.x;
            float y = rb.transform.position.y;
            float z = rb.transform.position.z;

            Vector3 targetDir = new Vector3(0, 0, 90);// - rb.transform.position;

            // The step size is equal to speed times frame time.
            float step = 1f * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        else if (rotation == "down")
        {
            
        }
        else if (rotation == "left")
        {

        }
        else if (rotation == "right")
        {

        }
        else
        {
            Debug.Log("ERROR! bad rotation value");
        }
    }

    public void flip()
    {
        float x = rb.transform.position.x;
        float y = rb.transform.position.y;
        transform.position = new Vector3(x, (float)(y * (-1.0)), 0);
        if (position)
        {
            position = false;
        }
        else
        {
            position = true;
            //Debug.Log(rb.transform.position);
        }
    }

    private void betterJump()
    {
        if (rb.velocity.y < 0)//faster fall
        {
            //rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            gravity = -18.0f;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))//low jump
        {
            //rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            gravity = -25.0f;
        }
        else
        {
            gravity = -9.81f;
        }
    }

}
