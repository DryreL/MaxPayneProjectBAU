using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class drunkeffect : MonoBehaviour
{
    public GameObject pp;

    float distance;
    public GameObject Player;
    IEnumerator ppcount()
    {
        pp.SetActive(true);
        yield return new WaitForSeconds(5f);
        pp.SetActive(false);
    }

    private void Update()
    {
        distance = Vector3.Distance(Player.transform.position, transform.position);
        if (distance < 5f && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ppcount());
        }
    }

}
