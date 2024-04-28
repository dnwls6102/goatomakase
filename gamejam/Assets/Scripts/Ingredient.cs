using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Ingredient_state
{
    FRESH,
    MIXED,
    BOILED,
    FRYED,
    FAILD,
}
public class Ingredient : MonoBehaviour
{
    Camera cam;
    Vector2 pre_position;
    GameObject board;
    public SpriteRenderer spriteRenderer;
    public SpriteChanger spriteChanger;
    Cooker cooker;
    public int idx;
    public bool dragable;
    public bool cooked;
    public bool is_plate;
    public bool in_board;
    public bool is_generator;
    public bool can_fry;

    public Ingredient_state state;

    public PoolManager pool;

    public Sprite pot_to_board;
    public Sprite mixer_to_board;

    private void Awake()
    {
        cam = Camera.main;
        board = GameObject.Find("board");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        pool = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        spriteChanger = gameObject.GetComponent<SpriteChanger>();
        dragable = true;
        in_board = false;
        if (!is_plate)
        {
            state = Ingredient_state.FRESH;
        }
    }

    private void OnMouseDown()
    {
        if (is_generator)
        {
            pre_position = transform.position;
            spriteRenderer.enabled = true;
        }
        else
        {
            if (dragable)
            {
                pre_position = transform.position;
            }
        }


    }
    private void OnMouseDrag()
    {
        if (is_generator)
        {
            Vector2 mouse_position = Input.mousePosition;
            mouse_position = cam.ScreenToWorldPoint(mouse_position);
            transform.position = mouse_position;
        }
        else
        {
            if (dragable)
            {
                Vector2 mouse_position = Input.mousePosition;
                mouse_position = cam.ScreenToWorldPoint(mouse_position);
                transform.position = mouse_position;
            }
        }



    }
    private void OnMouseUp()
    {
        Vector2 mouse_position = Input.mousePosition;
        mouse_position = cam.ScreenToWorldPoint(mouse_position);
        RaycastHit2D hit_board = Physics2D.Raycast(mouse_position, Vector3.down, 0.1f, LayerMask.GetMask("table"));
        RaycastHit2D hit_cooker = Physics2D.Raycast(mouse_position, Vector3.down, 0.1f, LayerMask.GetMask("Cooker"));
        RaycastHit2D hit_tray = Physics2D.Raycast(mouse_position, Vector3.down, 0.1f, LayerMask.GetMask("tray"));
        RaycastHit2D hit_trash_can = Physics2D.Raycast(mouse_position, Vector3.down, 0.1f, LayerMask.GetMask("trash"));
        //�������
        if (is_generator)
        {
            if (hit_board.collider != null)
            {
                print(hit_board.collider.name);

                Ingredient ingredient = pool.Get(idx).GetComponent<Ingredient>();
                ingredient.transform.position = mouse_position;
                ingredient.spriteRenderer.enabled = true;
                if (hit_board.collider.gameObject.GetComponent<Board>().AddIngredient(ingredient) == false)
                {
                    Destroy(ingredient.gameObject);
                    transform.position = pre_position;
                    spriteRenderer.enabled = false;
                    return;
                }
                ingredient.cooked = true;
                ingredient.in_board = true;

            }
            else if (hit_cooker.collider != null && hit_cooker.collider.gameObject.GetComponent<Cooker>().busy == false && can_fry)
            {

                Ingredient ingredient = pool.Get(idx).GetComponent<Ingredient>();
                ingredient.transform.position = mouse_position;

                print(hit_cooker.collider.name);
                hit_cooker.collider.gameObject.GetComponent<Cooker>().Cook(ingredient);
                ingredient.dragable = false;
                ingredient.cooker = hit_cooker.collider.gameObject.GetComponent<Cooker>();

                if (hit_cooker.collider.name == "Fryer")
                {
                    ingredient.spriteRenderer.enabled = true;
                    ingredient.state = Ingredient_state.FRYED;

                }
                else if (hit_cooker.collider.name == "Pot")
                {
                    ingredient.state = Ingredient_state.BOILED;
                }
                else if (hit_cooker.collider.name == "Mixer")
                {
                    ingredient.state = Ingredient_state.MIXED;
                }
            }

            transform.position = pre_position;
            spriteRenderer.enabled = false;

        }
        else
        {
            if (dragable)
            {
                //보드에 추가
                if (hit_board.collider != null && !in_board)
                {
                    in_board = true;
                    cooked = true;

                    if
                    (hit_board.collider.gameObject.GetComponent<Board>().AddIngredient(this) == false)
                    {
                        transform.position = pre_position;
                        return;
                    }
                    cooker.cooker_SpriteChanger.ChangeToDefualtSprite();
                    cooker.busy = false;

                }
                /*                else if (hit_cooker.collider != null && !cooked && can_fry)
                                {

                                    Ingredient ingredient = pool.Get(idx).GetComponent<Ingredient>();
                                    ingredient.transform.position = mouse_position;
                                    ingredient.spriteRenderer.enabled = true;
                                    print(hit_cooker.collider.name);
                                    hit_cooker.collider.gameObject.GetComponent<Cooker>().Cook(ingredient);
                                    ingredient.dragable = false;
                                }*/
                else if (hit_tray.collider != null) // 트레이에 추가안함. fail임
                {
                    if(state != Ingredient_state.FAILD)
                    {
                        transform.position = hit_tray.transform.position;
                        GameManager.gm._isFinished = true;
                        GameManager.gm.PrintFlags();
                    }
                    else
                    {
                        transform.position = pre_position;
                    }
                    print(GameManager.gm._isCorrect);
                    print(GameManager.gm._isFinished);

                }
                else if (hit_trash_can.collider != null)
                {
                    hit_trash_can.collider.GetComponent<TrashCan>().Trashing();

                }
            }

        }

    }

    /*    private void OnDisable()
        {
            spriteChanger.ChangeToDefualtSprite();
        }*/

}
