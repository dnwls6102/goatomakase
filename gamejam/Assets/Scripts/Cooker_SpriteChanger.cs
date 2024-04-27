using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cooker_SpriteChanger: MonoBehaviour
{
    [SerializeField]
    int ingredient_index;
    [SerializeField]
    SpriteRenderer spriteRenderer;


    
    public List<int> can_put_idx = new List<int>();
    public Sprite defualt_sprite;
    public Sprite[] cooking_sprites = new Sprite[5];
    public Sprite[] cooked_sprites = new Sprite[5];
    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); 
        spriteRenderer.sprite = defualt_sprite;
        ingredient_index = -1;
    }

    public bool ChangeToCookingSprite(int idx)
    {
        if( can_put_idx.Contains(0))
        {
            spriteRenderer.sprite = cooking_sprites[0];
            ingredient_index = 0;
            return true;
        }
        return false;
    }

    public void ChangeToCookedSprite()
    {
        spriteRenderer.sprite = cooked_sprites[ingredient_index];
    }

    public void ChangeToDefualtSprite()
    {
        ingredient_index = -1;
        spriteRenderer.sprite = defualt_sprite;
    }


}
