using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    public IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
    }

    public IEnumerator LoadAsyncronously(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        /*
        while (!operation.isDone)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
        */
    }

    public void GoToMainMenu() { StartCoroutine(LoadAsyncronously("MainMenu")); }
    public void Intro() { StartCoroutine(LoadAsyncronously("Intro")); }
    public void NewScene() { StartCoroutine(LoadAsyncronously("NewScene")); }
    public void ThanksForPlayingScreen() { StartCoroutine(LoadAsyncronously("ThanksForPlaying")); }
    public void Scene1() { StartCoroutine(LoadAsyncronously("Scene1"));}
    public void Scene2() { StartCoroutine(LoadAsyncronously("Scene2"));}
    public void Scene3() { StartCoroutine(LoadAsyncronously("Scene3"));}
}
