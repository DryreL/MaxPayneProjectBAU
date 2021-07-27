using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public Text[] texts;
    public GameObject[] mainMenuObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.JoystickButton10))
        {
            mainMenuObjects[0].SetActive(false);
            mainMenuObjects[1].SetActive(true);
        }
    }

    public void WhiteTextAll()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = Color.white;
        }
    }

    public void BlackTextAll()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = Color.black;
        }
    }

    public void StoryTextBlack() { texts[0].color = Color.black; }
    public void StoryTextWhite() { texts[0].color = Color.white; }
    public void SettingsTextBlack() { texts[1].color = Color.black; }
    public void SettingsTextWhite() { texts[1].color = Color.white; }
    public void CreditsTextBlack() { texts[2].color = Color.black; }
    public void CreditsTextWhite() { texts[2].color = Color.white; }
    public void QuitTextBlack() { texts[3].color = Color.black; }
    public void QuitTextWhite() { texts[3].color = Color.white; }
    public void BackTextBlack() { texts[4].color = Color.black; }
    public void BackTextWhite() { texts[4].color = Color.white; }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }
}
