using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Range(1, 10)] public float jumpVelocity;//how high you will probably jump
    [Range(1, 10)] public float movementVelocity;//how fast you will move
    public float fallMultiplier = 2.5f;//how fast the character will fall
    public float lowJumpMultiplier = 2f;//force applied down when character mini jumps
    public float gravityMultiplier = 2f;//force applied down when character mini jumps
    Rigidbody2D rb;//this is character this is how you affect its forces and movement
    public float gravity;//the 9.81 gravity
    public bool position;//this indicates whether at top or bottom of map
    public bool playerOne;//just a bool to decide which controls you use
    public string currentRotation;//current direction you are facing
    public string wantedRotation;//the rotation you going to next
    //bool changedRotation;//dono if i use this anymore
    GameObject otherPlayer;//the other player
    bool wantsToFlip;//went you want to a flip and are waiting for partner
    bool allowedToFlip;//ensure you dont flip continously
    bool canJump;
    Animator anim;//animation controller
    // Start is called before the first frame update

    // public AudioClip realmSwitch;
    // public AudioSource realmSwitchSound;

    // public AudioClip realmSwitchPowerUp;
    // public AudioSource realmSwitchSoundPowerUp;


    void Start()
    {
        // realmSwitchSound.clip = realmSwitch;
        // realmSwitchSoundPowerUp.clip = realmSwitchPowerUp;

        // realmSwitchSoundPowerUp.Stop();
        // realmSwitchSoundPowerUp.Stop();
        anim = GetComponent<Animator>();
        rb.gravityScale = 0f;//this ensures normal game gravity doesnt influence character
        //changedRotation = true;
        wantsToFlip = false;
        allowedToFlip = true;
        canJump = true;

        if(playerOne)
             otherPlayer = GameObject.FindWithTag("playerTwo");
         else
              otherPlayer = GameObject.FindWithTag("playerOne");
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
        
        flipFunction();
        rotate();
        movement();
        rb.AddForce(transform.up * gravity * gravityMultiplier * Time.deltaTime);//this is the gravity for the character
        //rb.AddForce(transform.right * gravity);
    }

    public void flipFunction()
    {
        if ((!Input.GetKey(KeyCode.S) && playerOne) || (!Input.GetKey(KeyCode.DownArrow) && !playerOne))
        {
            // realmSwitchSoundPowerUp.Play();
            allowedToFlip = true;
        }

        if (((Input.GetKey(KeyCode.S) && playerOne) || (Input.GetKey(KeyCode.DownArrow) && !playerOne)) && otherPlayer.GetComponent<player>().getOtherFlip() && allowedToFlip)
        {
            otherPlayer.GetComponent<player>().flip();
            flip();
            // realmSwitchSound.Play();
            // realmSwitchSoundPowerUp.Stop();
            
        }
        else if((Input.GetKey(KeyCode.S) && playerOne) || (Input.GetKey(KeyCode.DownArrow) && !playerOne))
        {
            wantsToFlip = true;
        }
        else
        {
            wantsToFlip = false;
        }
    }

    public bool getOtherFlip()
    {
        return wantsToFlip;
    }

    private void movement()
    {
        if(currentRotation == "up" )
        {
            bool move = false;
            float x = 0f, y = 0f;
            anim.SetBool("walking", false);
            if ((Input.GetKeyDown(KeyCode.W) && playerOne && canJump) || (Input.GetKeyDown(KeyCode.UpArrow) && !playerOne && canJump))
            {
                    y = -jumpVelocity;
                    canJump = false;
            }
            if ((Input.GetKey(KeyCode.D) && playerOne) || (Input.GetKey(KeyCode.RightArrow) && !playerOne))
            {
                anim.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                x += movementVelocity;
                move = true;
                anim.SetBool("walking", true);
            }
            if ((Input.GetKey(KeyCode.A) && playerOne) || (Input.GetKey(KeyCode.LeftArrow) && !playerOne))
            {
                anim.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                x += movementVelocity * -1;
                anim.SetBool("walking", true);
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
            anim.SetBool("walking", false);
            if ((Input.GetKeyDown(KeyCode.W) && playerOne && canJump) || (Input.GetKeyDown(KeyCode.UpArrow) && !playerOne && canJump))
            {
                    y = jumpVelocity;
                    canJump = false;
            }
            if ((Input.GetKey(KeyCode.D) && playerOne) || (Input.GetKey(KeyCode.RightArrow) && !playerOne))
            {
                anim.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                x += movementVelocity;
                move = true;
                anim.SetBool("walking", true);
            }
            if ((Input.GetKey(KeyCode.A) && playerOne) || (Input.GetKey(KeyCode.LeftArrow) && !playerOne))
            {
                anim.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                x += movementVelocity * -1;
                move = true;
                anim.SetBool("walking", true);
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
            anim.SetBool("walking", false);
            if ((Input.GetKeyDown(KeyCode.W) && playerOne && canJump) || (Input.GetKeyDown(KeyCode.UpArrow) && !playerOne && canJump))
            {
                x = jumpVelocity;
                canJump = false;
            }
            if ((Input.GetKey(KeyCode.D) && playerOne) || (Input.GetKey(KeyCode.RightArrow) && !playerOne))
            { 
                transform.rotation = Quaternion.Euler(0, transform.rotation.y, -90);
                y += movementVelocity * -1;
                move = true;
                anim.SetBool("walking", true);
            }
            if ((Input.GetKey(KeyCode.A) && playerOne) || (Input.GetKey(KeyCode.LeftArrow) && !playerOne))
            {
                transform.rotation = Quaternion.Euler(180, transform.rotation.y, -90);
                y += movementVelocity;
                move = true;
                anim.SetBool("walking", true);
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
            anim.SetBool("walking", false);
            if ((Input.GetKeyDown(KeyCode.W) && playerOne && canJump) || (Input.GetKeyDown(KeyCode.UpArrow) && !playerOne && canJump))
            {
                x = -jumpVelocity;
                canJump = false;
            }
            if ((Input.GetKey(KeyCode.D) && playerOne) || (Input.GetKey(KeyCode.RightArrow) && !playerOne))
            {
                transform.rotation = Quaternion.Euler(0, transform.rotation.y, 90);
                y += movementVelocity;
                move = true;
                anim.SetBool("walking", true);
            }
            if ((Input.GetKey(KeyCode.A) && playerOne) || (Input.GetKey(KeyCode.LeftArrow) && !playerOne))
            {
                transform.rotation = Quaternion.Euler(180, transform.rotation.y, 90);
                y += movementVelocity * -1;
                move = true;
                anim.SetBool("walking", true);
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
            float z = rb.transform.position.z;
            Vector3 targetDir = new Vector3(x-90, y, z);
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
            float z = rb.transform.position.z;
            Vector3 targetDir = new Vector3(x+90, y, z);
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
            float z = rb.transform.position.z;
            Vector3 targetDir = new Vector3(x, y-90, z);
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
            float z = rb.transform.position.z;
            Vector3 targetDir = new Vector3(x, y+90, z);
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
        allowedToFlip = false;
        float x = rb.transform.position.x;
        float y = rb.transform.position.y;
        float z = rb.transform.position.z;
        transform.position = new Vector3(x, (float)(y * (-1.0)), z);
        //changedRotation = false;
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
            else if ((rb.velocity.y > 0 && !Input.GetKey(KeyCode.W) && playerOne) || (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow) && !playerOne))//low jump
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
            else if ((rb.velocity.y < 0 && !Input.GetKey(KeyCode.W) && playerOne) || (rb.velocity.y < 0 && !Input.GetKey(KeyCode.UpArrow) && !playerOne))
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
             else if ((rb.velocity.x > 0 && !Input.GetKey(KeyCode.W) && playerOne) || (rb.velocity.x > 0 && !Input.GetKey(KeyCode.UpArrow) && !playerOne))//low jump
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
            else if ((rb.velocity.x < 0 && !Input.GetKey(KeyCode.W) && playerOne) || (rb.velocity.x < 0 && !Input.GetKey(KeyCode.UpArrow) && !playerOne))
            {
                gravity = -9.81f * lowJumpMultiplier;
            }
            else
            {
                gravity = -9.81f;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // other.attachedRigidbody.AddForce(-0.1F * other.attachedRigidbody.velocity);

        if (collision.gameObject.tag == "bubble")
        {
            gravityBubble bubble = (gravityBubble)collision.gameObject.GetComponent(typeof(gravityBubble));
           // Debug.Log("in the bubble"+bubble.gravityDirection);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("collision");
        if (collision.gameObject.tag == "ground")
        {
            canJump = true;
        }
    }

}
