using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    // Start is called before the first frame update
    public Board board;
    bool prev = false;
    void Start()
    {
        board = GameManager.gm.Doma;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm._isOrdering == false && prev ==true)
        {
            Trashing();
        }

        prev = GameManager.gm._isOrdering;
    }
    public void Trashing()
    {
        print("trasing");
        board.clear_board();
    }
}
