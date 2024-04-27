using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    Camera cam;
    Vector2 pre_position;
    GameObject board;
    SpriteRenderer spriteRenderer;
    public SpriteChanger spriteChanger;
    Cooker cooker;
    public int idx;
    public bool dragable; 
    public bool cooked;
    public bool in_board;
    public bool is_generator;

    public PoolManager pool;

    
    private void Awake()
    {
        cam = Camera.main;
        board = GameObject.Find("board");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        pool = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        spriteChanger = gameObject.GetComponent<SpriteChanger>();
        dragable = true;
        in_board = false;
    }

    private void OnMouseDown()
    {
        if(is_generator)
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
        if(is_generator)
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
        if (is_generator)
        {
            if (hit_board.collider != null)
            {
                print(hit_board.collider.name);

                Ingredient ingredient = pool.Get(idx).GetComponent<Ingredient>();
                ingredient.transform.position = mouse_position;
                ingredient.spriteRenderer.enabled = true;
                hit_board.collider.gameObject.GetComponent<Board>().AddIngredient(ingredient);
                ingredient.cooked = true;
                ingredient.in_board = true;
                
            }
            else if (hit_cooker.collider != null && hit_cooker.collider.gameObject.GetComponent<Cooker>().busy == false)
            {

                Ingredient ingredient = pool.Get(idx).GetComponent<Ingredient>();
                ingredient.transform.position = mouse_position;
                ingredient.spriteRenderer.enabled = true;
                print(hit_cooker.collider.name);
                hit_cooker.collider.gameObject.GetComponent<Cooker>().Cook(ingredient);
                ingredient.dragable = false;
                ingredient.cooker = hit_cooker.collider.gameObject.GetComponent<Cooker>();
            }

                transform.position = pre_position;
                spriteRenderer.enabled = false;

        }
        else
        {
            if (dragable)
            {

                if (hit_board.collider != null && !in_board)
                {
                    in_board = true;
                    cooked = true;
                    hit_board.collider.gameObject.GetComponent<Board>().AddIngredient(this);
                    cooker.cooker_SpriteChanger.ChangeToDefualtSprite();
                    cooker.busy = false;
                    
                }
                else if (hit_cooker.collider != null && !cooked)
                {

                    Ingredient ingredient = pool.Get(idx).GetComponent<Ingredient>();
                    ingredient.transform.position = mouse_position;
                    ingredient.spriteRenderer.enabled = true;
                    print(hit_cooker.collider.name);
                    hit_cooker.collider.gameObject.GetComponent<Cooker>().Cook(ingredient);
                    ingredient.dragable = false;
                }
                else
                {
                    transform.position = pre_position;
                }
            }
       


        }
        
    }

}
