using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public float MaxTime;
    public float GameTime = 5.0f;
    public Slider TimeBar;

    void Awake()
    {
        MaxTime = GameTime;
    }

    // Update is called once per frame
    void Update()
    {
        GameTime -= Time.deltaTime;
        CheckTime();
        if (GameTime < 0)
        {
            Debug.Log("Time Over");
            Time.timeScale = 0f;
        }
    }

    void CheckTime()
    {
        if (TimeBar != null)
        {
            TimeBar.value = GameTime / MaxTime;
        }
    }
}
