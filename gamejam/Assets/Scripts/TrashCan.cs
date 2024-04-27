using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    // Start is called before the first frame update
    public Board board;
    void Start()
    {
        board = GameManager.gm.Doma;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm._isOrdering == false)
        {
            Trashing();
        }
    }
    public void Trashing()
    {
        board.clear_board();
    }
}
