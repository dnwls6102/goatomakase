using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Text txt;


    public void StartTimer(int i)
    {
        StartCoroutine(Timer(i));
    }
    // Update is called once per frame
    IEnumerator Timer(int time)
    {
        txt.text = time.ToString();
        while (time >=0)
        {
            yield return new WaitForSeconds(1);
            time--;
            txt.text = time.ToString();
        }
        txt.text = "³¡!!!!!";
    }

    public void textDefualt()
    {
        txt.text = "";
    }
}
