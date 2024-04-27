using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpriteChanger: MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;


    
    public List<int> can_put_idx = new List<int>();
    public Sprite defualt_sprite;
    public Sprite cooking_sprite;
    public Sprite cooked_sprite;
    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); 
        spriteRenderer.sprite = defualt_sprite;
    }

    public void ChangeToCookingSprite()
    {
        spriteRenderer.sprite = cooking_sprite;
    }

    public void ChangeToCookedSprite()
    {
        spriteRenderer.sprite = cooked_sprite;
    }

    public void ChangeToDefualtSprite()
    {
        spriteRenderer.sprite = defualt_sprite;
    }


}
