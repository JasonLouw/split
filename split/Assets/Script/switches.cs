using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switches : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject switch1 = null;
    public GameObject switch2 = null; 
    public GameObject switch3 = null; 
    public GameObject switch4 = null; 
    public GameObject switch5 = null; 
    public GameObject switch6 = null;  

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerOne" || collision.gameObject.tag == "playerTwo")
        {
            if(switch1 != null)
            switch1.gameObject.GetComponent<gravityBubble>().changeBubbleDirection();
            if(switch2 != null)
            switch2.gameObject.GetComponent<gravityBubble>().changeBubbleDirection();
            if(switch3 != null)
            switch3.gameObject.GetComponent<gravityBubble>().changeBubbleDirection();
            if(switch4 != null)
            switch4.gameObject.GetComponent<gravityBubble>().changeBubbleDirection();
            if(switch5 != null)
            switch5.gameObject.GetComponent<gravityBubble>().changeBubbleDirection();
            if(switch6 != null)
            switch6.gameObject.GetComponent<gravityBubble>().changeBubbleDirection();
        }
    }
}
