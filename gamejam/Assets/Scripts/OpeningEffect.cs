using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningEffect : MonoBehaviour
{
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    private float timer;
    private GameObject[] list;
    private int temp;
    private int last;

    // Start is called before the first frame update
    void Awake()
    {
        timer = 0.0f;
        list = new GameObject[] { one, two, three, four, five };
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        temp = (int)timer % 5;
        last = temp - 1;
        if (last < 0)
            last = 4;
        list[temp].SetActive(true);
        list[last].SetActive(false);
    }
}
