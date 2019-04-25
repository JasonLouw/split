using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{
    
    private GameObject one;
    private GameObject two;

    // Start is called before the first frame update
    void Start()
    {
        one = null;
        two = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerOne"|| collision.gameObject.tag == "playerTwo")
        {
            //gravityBubble bubble = (gravityBubble)collision.gameObject.GetComponent(typeof(gravityBubble));
            if(one == null)
            {
                one = collision.gameObject;
                //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(30, 0, 0);
            }
            else
            {
                two = collision.gameObject;
            }

            if(one != null && two != null)
            {
                
                Rigidbody2D rb = one.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = two.GetComponent<Rigidbody2D>();
                Debug.Log("big jump "+ rb2.velocity.y);

                float s;
                if(rb2.velocity.x > 0)
                {
                    s = rb2.velocity.y * -1.0f;
                }
                else
                {
                    s = rb2.velocity.y;
                }

                if(rb.transform.position.y < 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x,  s);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x,  s);  
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerOne" || collision.gameObject.tag == "playerTwo")
        {
            if(collision.gameObject.tag == one.tag && two != null)
            {
                one = two;
                two = null;
            }
            else if(collision.gameObject.tag == one.tag && two == null)
            {
                one = null;
                two = null;
            }
        }
    }
}
