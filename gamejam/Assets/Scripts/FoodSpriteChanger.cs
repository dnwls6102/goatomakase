using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodSpriteChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] defualt_sprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ChangeSprite(int idx)
    {
        spriteRenderer.sprite = defualt_sprite[idx];
    }
}
