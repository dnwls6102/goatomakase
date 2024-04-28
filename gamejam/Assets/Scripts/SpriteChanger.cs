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
    public Sprite boil_fail;
    public Sprite mixed_fail;
    public Sprite in_boil;
    public Sprite in_mixer;

    public Sprite[] spritelist;
    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); 
        spriteRenderer.sprite = defualt_sprite;
    }

    public void TargetSprite(int idx)
    {
        cooking_sprite = spritelist[idx];
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

    public void ChangeToBoilFail()
    {
        spriteRenderer.sprite = boil_fail;
    }
    public void ChangeToMixedFail()
    {
        spriteRenderer.sprite = mixed_fail;
    }

    public void ChangeToInBoil()
    {
        spriteRenderer.sprite = in_boil;
    }
    public void ChangeToInMixed()
    {
        spriteRenderer.sprite = in_mixer;
    }


}
