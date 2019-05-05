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

            Debug.Log(collision.gameObject.tag);
            //gravityBubble bubble = (gravityBubble)collision.gameObject.GetComponent(typeof(gravityBubble));
            if(one == null)
            {
                Debug.Log("first");
                one = collision.gameObject;
            }
            else
            {
                Debug.Log("second");
                two = collision.gameObject;
            }

            if(one != null && two != null)
            {
                
                Rigidbody2D rb = one.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = two.GetComponent<Rigidbody2D>();
                Debug.Log("boom from: "+collision.gameObject.tag+" speed "+ rb2.velocity.y);
                

                float s;
                // if(rb2.velocity.x < 0)
                // {
                    // s = rb2.velocity.y * -1.0f;
                // }
                // else
                // {
                    s = rb2.velocity.y;
                // }
                Debug.Log("after"+ s);
                rb.velocity = new Vector2(rb.velocity.x,  s);  
                
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
