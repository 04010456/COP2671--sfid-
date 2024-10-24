using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{ 
    // transform objects of the camera and both the backgrounds
    public Transform mainCam;
    public Transform middleBG;
    public Transform sideBG;

    public float length = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if the camera x position is greater than the middle background x position, spawn a new side background to the right of the middle background
        if (mainCam.position.x > middleBG.position.x)
        {
            sideBG.position = middleBG.position + Vector3.right * length;
        }

        // if the camera x position is less than the middle background x position, spawn a new side background to the left of the middle background
        if (mainCam.position.x < middleBG.position.x)
        {
            sideBG.position = middleBG.position + Vector3.left * length;
        }

        // if the camera x position is greater than or less than the side background x position, swap the middle and side background to create a continous background
        if (mainCam.position.x > sideBG.position.x || mainCam.position.x < sideBG.position.x)
        {
            Transform z = middleBG;
            middleBG = sideBG;
            sideBG = z;
        }
    }
}
