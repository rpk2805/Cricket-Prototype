using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchMarkerController : MonoBehaviour
{
    [Tooltip("Marker Reference")][SerializeField] private GameObject marker;
    [Tooltip("JoyStick Reference")] [SerializeField] private Joystick joystick;
    float xAxis;// xAxis Movement
    float zAxis;//zAxis Movement;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region controll The Marker
        xAxis = joystick.Horizontal;
        zAxis = joystick.Vertical;

        //Clamp The position in boundaries
        if (marker.transform.position.x > 3.8f)
            marker.transform.position = new Vector3(3.8f, marker.transform.position.y, marker.transform.position.z);
        else if(marker.transform.position.x < -3.8f)
            marker.transform.position = new Vector3(-3.8f, marker.transform.position.y, marker.transform.position.z);
        if (marker.transform.position.z > 9f)
            marker.transform.position = new Vector3(marker.transform.position.x, marker.transform.position.y, 9);
        else if (marker.transform.position.z < 0f)
            marker.transform.position = new Vector3(marker.transform.position.x, marker.transform.position.y,0);
        //Move The Marker
        marker.transform.Translate(xAxis * 5f * Time.deltaTime, zAxis * 5f * Time.deltaTime, 0);
        #endregion



    }
}
