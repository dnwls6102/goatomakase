using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;



public class PoolManager : MonoBehaviour
{
    //��������� ������ ����
    public GameObject[] prefabs;

    //Ǯ ����� �ϴ� ����Ʈ��
    public List<GameObject>[] pools;
    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
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
