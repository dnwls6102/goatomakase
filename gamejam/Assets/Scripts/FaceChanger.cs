using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChanger : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite default_sprite;
    public Sprite angry_sprite;
    public Sprite veryangry_sprite;
    // void Awake()
    // {
    //     spriteRenderer = GetComponent<spriteRenderer>();
    //     spriteRenderer.sprite = default_sprite;
    // }

    public void ChangeToAngrySprite()
    {
        spriteRenderer.sprite = angry_sprite;
    }
}
