using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Range(1, 10)] public float jumpVelocity;
    [Range(1, 10)] public float movementVelocity;
    public float fallMultiplier = 2.5f;//how fast the character will fall
    public float lowJumpMultiplier = 2f;//force applied down when character mini jumps
    public float gravityMultiplier = 2f;//force applied down when character mini jumps
    Rigidbody2D rb;//this is character this is how you affect its forces and movement
    public float gravity;
    public bool position;//this indicates whether at top or bottom of map
    public string currentRotation;
    public string wantedRotation;
    bool changedRotation;
    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 0f;//this ensures normal game gravity doesnt influence character
        changedRotation = true;
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
        rb.AddForce(transform.up * gravity * gravityMultiplier * Time.deltaTime);//this is the gravity for the character
        //rb.AddForce(transform.right * gravity);
    }

    private void movement()
    {
        if(currentRotation == "up" )
        {
            bool move = false;
            float x = 0f, y = 0f;
            if (Input.GetKeyDown(KeyCode.W))
            {
                    y = -jumpVelocity;
            }
            if (Input.GetKey(KeyCode.D))
            {
                x += movementVelocity;
                move = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                x += movementVelocity * -1;
                move = true;
            }

            if (move)
            {
                rb.velocity = new Vector2(x, rb.velocity.y + y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y + y);
            }
        }
        else if(currentRotation == "down")
        {
            bool move = false;
            float x = 0f, y = 0f;
            if (Input.GetKeyDown(KeyCode.W))
            {
                    y = jumpVelocity;
            }
            if (Input.GetKey(KeyCode.D))
            {
                x += movementVelocity;
                move = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                x += movementVelocity * -1;
                move = true;
            }

            if (move)
            {
                rb.velocity = new Vector2(x, rb.velocity.y + y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y + y);
            }
        }
        else if(currentRotation == "left")
        {
            bool move = false;
            float x = 0f, y = 0f;
            if (Input.GetKeyDown(KeyCode.W))
            {
                x = jumpVelocity;
            }
            if (Input.GetKey(KeyCode.D))
            {
                y += movementVelocity;
                move = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                y += movementVelocity * -1;
                move = true;
            }

            if (move)
            {
                rb.velocity = new Vector2(rb.velocity.x + x,  y);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x + x, 0);
            }
        }
        else //if rotation is right
        {
            bool move = false;
            float x = 0f, y = 0f;
            if (Input.GetKeyDown(KeyCode.W))
            {
                x = -jumpVelocity;
            }
            if (Input.GetKey(KeyCode.D))
            {
                y += movementVelocity;
                move = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                y += movementVelocity * -1;
                move = true;
            }

            if (move)
            {
                rb.velocity = new Vector2(rb.velocity.x + x,  y);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x + x, 0);
            }
        }


    }

    public void rotate()//rotates the character to the specific location once per change of direction
    {
        if (wantedRotation == "up" && wantedRotation != currentRotation)
        {
            float x = rb.transform.position.x;
            float y = rb.transform.position.y;
            Vector3 targetDir = new Vector3(x-90, y, 0);
            var relativePos = targetDir - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            currentRotation = wantedRotation;
        }
        else if (wantedRotation == "down"  && wantedRotation != currentRotation)
        {
            float x = rb.transform.position.x;
            float y = rb.transform.position.y;
            Vector3 targetDir = new Vector3(x+90, y, 0);
            var relativePos = targetDir - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            currentRotation = wantedRotation;
        }
        else if (wantedRotation == "left" && wantedRotation != currentRotation)
        {
            float x = rb.transform.position.x;
            float y = rb.transform.position.y;
            Vector3 targetDir = new Vector3(x, y-90, 0);
            var relativePos = targetDir - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            currentRotation = wantedRotation;
        }
        else if (wantedRotation == "right" && wantedRotation != currentRotation)
        {
            float x = rb.transform.position.x;
            float y = rb.transform.position.y;
            Vector3 targetDir = new Vector3(x, y+90, 0);
            var relativePos = targetDir - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            currentRotation = wantedRotation;
        }
        else
        {
           // Debug.Log("not rotating");
        }
    }

    public void flip()//flips over axis
    {
        float x = rb.transform.position.x;
        float y = rb.transform.position.y;
        transform.position = new Vector3(x, (float)(y * (-1.0)), 0);
        changedRotation = false;
        if (position)
        {
            wantedRotation = "up";
            position = false;
        }
        else
        {
            wantedRotation = "down";
            position = true;
            //Debug.Log(rb.transform.position);
        }
    }

    private void betterJump()//better jump controls fall speeds
    {
        if(currentRotation == "down")
        {
            if (rb.velocity.y < 0)//faster fall
            {
                gravity = -9.81f * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))//low jump
            {
               gravity = -9.81f * lowJumpMultiplier;
            }
            else
            {
                gravity = -9.81f;
            }
        }
        else if(currentRotation == "up")
        {
            if (rb.velocity.y > 0)//faster fall
            {
                gravity = -9.81f * fallMultiplier;
            }
            else if (rb.velocity.y < 0 && !Input.GetKey(KeyCode.W))//low jump
            {
                gravity = -9.81f * lowJumpMultiplier;
            }
            else
            {
                gravity = -9.81f;
            }
        }
        else if(currentRotation == "left")
        {
            if (rb.velocity.x < 0)//faster fall
            {
                gravity = -9.81f * fallMultiplier;
            }
            else if (rb.velocity.x > 0 && !Input.GetKey(KeyCode.W))//low jump
            {
                gravity = -9.81f * lowJumpMultiplier;
            }
            else
            {
                gravity = -9.81f;
            }
        }
        else if(currentRotation == "right")
        {
            if (rb.velocity.x > 0)//faster fall
            {
                gravity = -9.81f * fallMultiplier;
            }
            else if (rb.velocity.x < 0 && !Input.GetKey(KeyCode.W))//low jump
            {
                gravity = -9.81f * lowJumpMultiplier;
            }
            else
            {
                gravity = -9.81f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // other.attachedRigidbody.AddForce(-0.1F * other.attachedRigidbody.velocity);

        if (collision.gameObject.tag == "bubble")
        {
            gravityBubble bubble = (gravityBubble)collision.gameObject.GetComponent(typeof(gravityBubble));
            Debug.Log("in the bubble"+bubble.gravityDirection);
            wantedRotation = bubble.gravityDirection;
        }
    }

      void OnTriggerExit2D(Collider2D collision)
    {
        // other.attachedRigidbody.AddForce(-0.1F * other.attachedRigidbody.velocity);

        if (collision.gameObject.tag == "bubble")
        {
            gravityBubble bubble = (gravityBubble)collision.gameObject.GetComponent(typeof(gravityBubble));
            if(0 > rb.transform.position.y)
                wantedRotation = "up";
            else
                wantedRotation = "down";
        }
    }

}
