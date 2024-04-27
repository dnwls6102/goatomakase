using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cooker_state
{
    IDLE,
    WORKING,
    COOKED
}
public class Cooker : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteChanger cooker_SpriteChanger;
    Ingredient current_ingredient;

    cooker_state state;
    public int time;

    public bool busy;
    void Awake()
    {
        cooker_SpriteChanger = GetComponent<SpriteChanger>();
        busy = false;
    }

    // Update is called once per frame
    private void Start()
    {
        cooker_SpriteChanger.ChangeToDefualtSprite();

    }
    void Update()
    {
        
    }

    public void Cook(Ingredient ing)
    {

        current_ingredient = ing;
        StartCoroutine(CookCorutine());

    }
    IEnumerator CookCorutine()
    {
        busy = true;
        current_ingredient.spriteChanger.ChangeToCookingSprite();
        cooker_SpriteChanger.ChangeToCookingSprite();
        yield return new WaitForSeconds(time);
        current_ingredient.dragable = true;
        current_ingredient.spriteChanger.ChangeToCookedSprite();
        current_ingredient.cooked = true;
    }


}
