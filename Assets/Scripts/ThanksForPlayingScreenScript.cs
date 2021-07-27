using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanksForPlayingScreenScript : MonoBehaviour
{
    public GameObject myObject;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            myObject.GetComponent<LevelLoader>().ThanksForPlayingScreen();
        }

    }
}