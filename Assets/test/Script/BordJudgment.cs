using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BordJudgment : MonoBehaviour {

    public GameObject winerObj; // 決着時に表示
    public GameObject[] winerOFFImege; // 決着時に消すもの
    public Sprite[] winerImage;
    public bool Judg = true;

    private int[,] bord;
    private int[] victoryPlayer = new int[3]; // ナナメ縦横の順
    private Bord bordJudg;
    private ChessmanState[,] chessmanSelectOffObj;

    private void Start()
    {
        bordJudg = this.GetComponent<Bord>();
        bord = new int[bordJudg.squares + 2, bordJudg.squares + 2];
        
    }

    // コマのオブジェクトを取得
    public void ChessmanTransfer(ChessmanState[,] chessmans)
    {
        chessmanSelectOffObj = chessmans;
    }

    // 盤面の様子を取得して勝敗判定
    public void BordTransfer(int[,] bordOrigin)
    {
        bord = bordOrigin;

        victoryPlayer[0] = AslopeJadg();
        victoryPlayer[2] = HeightJadg();
        victoryPlayer[1] = SideJadg();

        foreach (int victoryPoint in victoryPlayer)
        {
            switch(victoryPoint)
            {
                case 1:
                    Debug.Log("1p Win");
                    ChessmanNonActiv();
                    bordJudg.ChessDrow();
                    
                    StartCoroutine(ResultUIOpen(0));
                    Judg = false;
                    break;
                case 2:
                    Debug.Log("2p Win");
                    ChessmanNonActiv();
                    bordJudg.ChessDrow();

                    StartCoroutine(ResultUIOpen(1));
                    Judg = false;
                    break;
            }
        }
    }

    // コマを選択できないようにする
    private void ChessmanNonActiv()
    {
        foreach(ChessmanState chess in chessmanSelectOffObj)
        {
            chess.choicePossible = false;
        }
    }

    //横がそろっているかの判定 [0,1,2]=[無, 1pWin, 2pWin]
    private int SideJadg()
    {
        int victorySum = 1;

        for (int y = 1; y <= bordJudg.squares; y++)
        {
            for (int x = 1; x <= bordJudg.squares; x++)
            {
                victorySum *= bord[y, x];
            }

            if (victorySum == 1)
            {
                return 1;
            }
            else if (victorySum == Mathf.Pow(2, bordJudg.squares))
            {
                return 2;
            }
            victorySum = 1;
        }
        return 0;
    }

    //縦がそろっているかの判定 [0,1,2]=[無, 1pWin, 2pWin]
    private int HeightJadg()
    {
        int victorySum = 1;

        for (int x = 1; x <= bordJudg.squares; x++)
        {
            for (int y = 1; y <= bordJudg.squares; y++)
            {
                victorySum *= bord[y, x];
            }

            if (victorySum == 1)
            {
                return 1;
            }
            else if (victorySum == Mathf.Pow(2, bordJudg.squares))
            {
                return 2;
            }
            victorySum = 1;
        }
        return 0;
    }

    //斜めがそろっているかの判定 [0,1,2]=[無, 1pWin, 2pWin]
    private int AslopeJadg()
    {
        int victorySum = 1;
        //左上から右下
        for (int i = 1; i <= bordJudg.squares; i++)
        {
            victorySum *= bord[i, i];
        }
        if (victorySum == 1)
        {
            return 1;
        }
        else if (victorySum == Mathf.Pow(2, bordJudg.squares))
        {
            return 2;
        }
        victorySum = 1;

        //右上から左下
        for (int i = 1; i <= bordJudg.squares; i++)
        {
            victorySum *= bord[bordJudg.squares + 1 - i, i];
        }
        if (victorySum == 1)
        {
            return 1;
        }
        else if (victorySum == Mathf.Pow(2, bordJudg.squares))
        {
            return 2;
        }
        return 0;
    }

    // リザルト画面の表示
    private IEnumerator ResultUIOpen(int pl)
    {
        yield return new WaitForSeconds(1);

        winerObj.SetActive(true);
        winerObj.GetComponent<Image>().sprite = winerImage[pl];

        foreach (GameObject offImege in winerOFFImege)
        {
            offImege.SetActive(false);
        }
    }
}
