using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessmanState : MonoBehaviour {

    public enum chessmanType
    {
        non, circle, cross, non_active, circle_active, cross_active
    }

    public Vector2 coordinateChessman;
    public Sprite[] chessmanImages;
    public bool choicePossible = true;
    public chessmanType chessType;
    
    public void SetType(int type)
    {
        if (choicePossible)
        {
            type += 3;
        }

        switch (type)
        {
            case 0:
                chessType = chessmanType.non;
                break;
            case 1:
                chessType = chessmanType.circle;
                break;
            case 2:
                chessType = chessmanType.cross;
                break;
            case 3:
                chessType = chessmanType.non_active;
                break;
            case 4:
                chessType = chessmanType.circle_active;
                break;
            case 5:
                chessType = chessmanType.cross_active;
                break;
            default:
                chessType = chessmanType.non;
                break;
        }
        this.GetComponent<SpriteRenderer>().sprite = chessmanImages[type];
    }
    
}
