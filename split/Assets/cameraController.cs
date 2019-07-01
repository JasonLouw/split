using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform target;
    public Transform target1;
    public float cameraFollowSpeed = 0.125f;


    // Start is called before the first frame update
    void Start()
    {
        setRatio();
    }

     // Update is called once per frame
    void FixedUpdate()//always run right after update, which is cool i guess
    {
        Vector3 p1 = target.position;
        Vector3 p2 = target1.position;

        Vector3 middle = (p1+p2)/2;
        middle.z = -39.52f;
        middle.y = 0.0f;
        Vector3 smoothMiddle = Vector3.Lerp(transform.position, middle, cameraFollowSpeed*Time.deltaTime);
        transform.position = smoothMiddle;
    }



    private void setRatio()
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 2.0f / 1.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {  
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;
            
            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

   
}
