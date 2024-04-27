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

    private string[] sampleStringArray;
    private bool firstDish;
    private bool secondDish;
    private bool thirdDish;

    void Awake()
    {
        MaxTime = GameTime;
        sampleStringArray = new string[] { "쌀밥주세요", "짜장면주세요", "피자주세요" };
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

    public void CorrectDebug()
    {
        GameTime += 1.0f;
        if (GameTime > MaxTime)
        {
            MaxTime = GameTime;
        }
    }

    public void WrongDebug()
    {
        GameTime -= 1.0f;
    }
}
