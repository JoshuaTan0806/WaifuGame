using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject Transition;

    public void LoadSceneNumber(int SceneNumber)
    {
        Instantiate(Transition);
        StartCoroutine(LoadSceneCoroutine(SceneNumber));
    }

    IEnumerator LoadSceneCoroutine(int SceneNumber)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneNumber);
    }
}
