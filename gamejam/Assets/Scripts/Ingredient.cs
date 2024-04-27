using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    Camera cam;
    Vector2 pre_position;
    GameObject board;
    private void Awake()
    {
        cam = Camera.main;
        board = GameObject.Find("board");
    }
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        pre_position = transform.position;

    }
    private void OnMouseDrag()
    {
        Vector2 mouse_position = Input.mousePosition;
        mouse_position = cam.ScreenToWorldPoint(mouse_position);
        transform.position = mouse_position;
        
    }
    private void OnMouseUp()
    {
        Vector2 mouse_position = Input.mousePosition;
        mouse_position = cam.ScreenToWorldPoint(mouse_position);
        RaycastHit2D hit = Physics2D.Raycast(mouse_position, Vector3.down, 1, LayerMask.GetMask("table"));
        if(hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<Board>().AddIngredient(this);
        }

        transform.position = pre_position;
    }

}
