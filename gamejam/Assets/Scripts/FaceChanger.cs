using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChanger : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    //각 리스트별 0번 : 흰염소 1번 :흑염소 2번 : 악마
    public Sprite default_sprite_white;
    public Sprite angry_sprite_white;
    public Sprite veryangry_sprite_white;
    public Sprite default_sprite_black;
    public Sprite angry_sprite_black;
    public Sprite veryangry_sprite_black;
    public Sprite default_sprite_devil;
    public Sprite angry_sprite_devil;
    public Sprite verangry_sprite_devil;
    private List<Sprite> defaultFaces;
    private List<Sprite> angryFaces;
    private List<Sprite> veryangryFaces;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = default_sprite_white;
        defaultFaces = new List<Sprite> { default_sprite_white, default_sprite_black, default_sprite_devil };
        angryFaces = new List<Sprite> { angry_sprite_white, angry_sprite_black, angry_sprite_devil };
        veryangryFaces = new List<Sprite> { veryangry_sprite_white, veryangry_sprite_black, verangry_sprite_devil };
    }

    public void SetDefaultSprite(int index)
    {
        spriteRenderer.sprite = defaultFaces[index];
    }

    public void ChangeToAngrySprite(int index)
    {
        spriteRenderer.sprite = angryFaces[index];
    }
    public void ChangeToVeryangrySprite(int index)
    {
        spriteRenderer.sprite = veryangryFaces[index];
    }
}
