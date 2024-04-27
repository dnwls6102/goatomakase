using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    List<Ingredient> ingredient_list;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddIngredient(Ingredient ingredient)
    {
        ingredient_list.Add(ingredient);
        
        return true;
    }


}
