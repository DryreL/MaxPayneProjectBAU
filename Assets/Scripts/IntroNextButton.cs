using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroNextButton : MonoBehaviour
{

    [Range(0, 5)] public int currentOption = 0;
    [SerializeField] private Button nextButton;

    // the image you want to fade, assign in inspector
    public Image[] img;

    private void Start()
    {
        nextButton.onClick.AddListener(() => myButtonClick(currentOption++));
    }

    public void OnButtonClick()
    {
        // fades the image out when you click
        StartCoroutine(FadeImage(true));
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                for (int a = 0; a < img.Length; a++)
                {
                    img[a].color = new Color(1, 1, 1, i);
                }
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                for (int a = 0; a < img.Length; a++)
                {
                    img[a].color = new Color(1, 1, 1, i);
                }
                yield return null;
            }
        }
    }

    public void myButtonClick(int optionValue)
    {
        switch (currentOption)
        {
            case 0:
                Debug.Log("Image1 Initialized.");
                img[0].gameObject.SetActive(true);
                optionValue++;
                break;

            case 1:
                Debug.Log("Image2 Initialized.");
                img[0].gameObject.SetActive(false);
                img[1].gameObject.SetActive(true);
                optionValue++;
                break;

            case 2:
                Debug.Log("Image3 Initialized.");
                img[1].gameObject.SetActive(false);
                img[2].gameObject.SetActive(true);
                optionValue++;
                break;

            case 3:
                Debug.Log("Image4 Initialized.");
                img[2].gameObject.SetActive(false);
                img[3].gameObject.SetActive(true);
                optionValue++;
                break;

            case 4:
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().NewScene();

                optionValue++;
                break;
        }
    }

}