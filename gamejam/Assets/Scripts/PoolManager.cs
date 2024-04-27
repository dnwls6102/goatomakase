using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;



public class PoolManager : MonoBehaviour
{
    //프리펩들을 보관할 변수
    public GameObject[] prefabs;

    //풀 담당을 하는 리스트들
    public List<GameObject>[] pools;
    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i=0;i<pools.Length;i++)
        {
            pools[i] =new List<GameObject>();
        }
        
    }
    public void Start()
    {
    }

    public GameObject Get(int ID)
    {
        GameObject select = null;
/*

        foreach (GameObject item in pools[ID])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if (select == null)
        {*/
            select = Instantiate(prefabs[ID], transform);
/*            pools[ID].Add(select);
        }*/

        return select;
    }

}
