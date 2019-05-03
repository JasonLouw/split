using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityBubble : MonoBehaviour
{
    // Start is called before the first frame update
    public string gravityDirection;
    public string gravityPosition1;
    public string gravityPosition2;
    public string gravityPosition3;
    public string gravityPosition4;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeBubbleDirection()
    {
        if(gravityDirection == gravityPosition1)
        {
            gravityDirection = gravityPosition2;
        }
        else if(gravityDirection == gravityPosition2)
        {
            gravityDirection = gravityPosition3;
        }
        else if(gravityDirection == gravityPosition3)
        {
            gravityDirection = gravityPosition4;
        }
        else if(gravityDirection == gravityPosition4)
        {
            gravityDirection = gravityPosition1;
        }
        else
        {
             Debug.Log("gravity no set");
        }

        if(gravityDirection == "null")
        {
            gravityDirection = gravityPosition1;
        }
    }
}
