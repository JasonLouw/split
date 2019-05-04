using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowSwap : MonoBehaviour
{
    // Start is called before the first frame update

    public string wantedRotation;
    public string currentRotation;


    public Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.Log("big error");
    }



    public void changeDirection(string dir)
    {
        wantedRotation = dir;
    }

    // Update is called once per frame
    void Update()
    {
        if (wantedRotation == "down" && wantedRotation != currentRotation)
        {
            Debug.Log("go up");
            float x = rb.transform.position.x;
            float y = rb.transform.position.y;
            Vector3 targetDir = new Vector3(x - 90, y, 0);
            var relativePos = targetDir - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            currentRotation = wantedRotation;
        }
        else if (wantedRotation == "up" && wantedRotation != currentRotation)
        {
            if(rb == null)
              Debug.Log("go down");
            float x = rb.transform.position.x;
            float y = rb.transform.position.y;
            Vector3 targetDir = new Vector3(x + 90, y, 0);
            var relativePos = targetDir - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            currentRotation = wantedRotation;
        }
        else if (wantedRotation == "right" && wantedRotation != currentRotation)
        {
            Debug.Log("go left");
            float x = rb.transform.position.x;
            float y = rb.transform.position.y;
            Vector3 targetDir = new Vector3(x, y - 90, 0);
            var relativePos = targetDir - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            currentRotation = wantedRotation;
        }
        else if (wantedRotation == "left" && wantedRotation != currentRotation)
        {
            Debug.Log("go right");
            float x = rb.transform.position.x;
            float y = rb.transform.position.y;
            Vector3 targetDir = new Vector3(x, y + 90, 0);
            var relativePos = targetDir - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            currentRotation = wantedRotation;
        }
        else
        {
             Debug.Log("not rotating");
        }
    }
}
