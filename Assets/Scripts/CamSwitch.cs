using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject playeRCam;
    public GameObject camCam;

    private void OnMouseDown()
    {
        Debug.Log("ss");
        playeRCam.SetActive(false);
        camCam.SetActive(true);

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playeRCam.SetActive(true);
            camCam.SetActive(false);

        }
    }
}
