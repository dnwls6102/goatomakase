using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGameScene : MonoBehaviour
{
    public void RealLoadGameScene()
    {
        StartCoroutine("Delay");
    }
    private void LoadGameScene()
    {
        SceneManager.LoadScene("sample1");
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        LoadGameScene();
    }
}
