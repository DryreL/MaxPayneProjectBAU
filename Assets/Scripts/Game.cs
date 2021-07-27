using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Time.timeScale -= Time.timeScale * 0.05f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
