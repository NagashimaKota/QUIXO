using System.Collections;
using UnityEngine;

public class Bord : MonoBehaviour {

    public int squares = 3;
    public GameObject chessmanPrefab;

    private int[,] bord;
    private ChessmanState[,] chessman;
    private BordJudgment bordJudg;
    private int playerTurn = 0;
    private float postionClearance = 1.5f; // コマ置く場所を変える量（隙間）
    private float waku_width = 0.1f;

    private void Start()
    {
        bordJudg = this.GetComponent<BordJudgment>();

        // マス数によるコマの大きさ変化
        switch (squares)
        {
            case 3:
                postionClearance = 2.3f / 1.5f;
                waku_width = 0.1f;
                chessmanPrefab.transform.localScale = Vector2.one * postionClearance;
                break;
            case 5:
                postionClearance = 1.5f;
                waku_width = 0.2f;
                chessmanPrefab.transform.localScale = Vector2.one;
                break;
            case 7:
                postionClearance = 1.5f;
                waku_width = 0.2f;
                chessmanPrefab.transform.localScale = Vector2.one / postionClearance;
                break;
        }

        //bord(y,x) = で以下の並び
        bord = new int[squares + 2, squares + 2];
        chessman = new ChessmanState[squares, squares];
        

        for (int y = squares / 2; y >= -squares / 2; y--)
        {
            for (int x = -squares /2 ; x <= squares / 2; x++)
            {
                chessman[y + (squares / 2), x + (squares / 2)] = Instantiate(chessmanPrefab).GetComponent<ChessmanState>();
                chessman[y + (squares / 2), x + (squares / 2)].choicePossible = false;
                chessman[y + (squares / 2), x + (squares / 2)].SetType(0);
                chessman[y + (squares / 2), x + (squares / 2)].coordinateChessman = new Vector2(x, y);
                chessman[y + (squares / 2), x + (squares / 2)].transform.position = new Vector3(x, y) * chessmanPrefab.transform.localScale.x　* postionClearance;
            }
        }
        for (int i = 0; i < squares; i++)
        {
            chessman[i,0].choicePossible = true;
            chessman[0, i].choicePossible = true;
            chessman[i, squares -1].choicePossible = true;
            chessman[squares -1, i].choicePossible = true;

            chessman[i, 0].SetType(0);
            chessman[0, i].SetType(0);
            chessman[i, squares - 1].SetType(0);
            chessman[squares - 1, i].SetType(0);
        }

        bordJudg.ChessmanTransfer(chessman);
    }

    //ボタンを押したときのコマの入れ替え
    public void MoveChessmanRightSlide(ChessmanState selectObject)
    {
        switch (selectObject.chessType)
        {
            //bord[y,x]の範囲 [0~squares+2, 0~squares+2]
            case ChessmanState.chessmanType.non_active:
                bord[(int)selectObject.coordinateChessman.y + (squares / 2)+1, 0] = playerTurn % 2 + 1;
                break;
            case ChessmanState.chessmanType.circle_active:
                bord[(int)selectObject.coordinateChessman.y + (squares / 2)+1, 0] = 1;
                break;
            case ChessmanState.chessmanType.cross_active:
                bord[(int)selectObject.coordinateChessman.y + (squares / 2)+1, 0] = 2;
                break;
        }

        //selectObject.coordinateChessman の範囲 [-(squares / 2) ~ (squares / 2)](x,y)
        for (int i = (int)selectObject.coordinateChessman.x; i >= -(squares / 2); i--)
        {
            chessman[(int)selectObject.coordinateChessman.y + (squares / 2), i + (squares / 2)].transform.position = new Vector2(i - 1, (int)selectObject.coordinateChessman.y) * chessmanPrefab.transform.localScale.x * postionClearance;
            bord[(int)selectObject.coordinateChessman.y + (squares / 2) + 1, i + (squares / 2)+1] = bord[(int)selectObject.coordinateChessman.y + (squares / 2) + 1, i + (squares / 2)];
            
        }
        chessman[(int)selectObject.coordinateChessman.y + (squares / 2), 0].transform.position = new Vector2(-(squares / 2) - 1 - waku_width, (int)selectObject.coordinateChessman.y) * chessmanPrefab.transform.localScale.x * postionClearance;
        ChessDrow();
        StartCoroutine(LineMoveChessman( "right", selectObject.coordinateChessman));
    }

    public void MoveChessmanLeftSlide(ChessmanState selectObject)
    {

        switch (selectObject.chessType)
        {
            //bord[y,x]の範囲 [0~squares+2, 0~squares+2]
            case ChessmanState.chessmanType.non_active:
                bord[(int)selectObject.coordinateChessman.y + (squares / 2) + 1, squares + 1] = playerTurn % 2 + 1;
                break;
            case ChessmanState.chessmanType.circle_active:
                bord[(int)selectObject.coordinateChessman.y + (squares / 2) + 1, squares + 1] = 1;
                break;
            case ChessmanState.chessmanType.cross_active:
                bord[(int)selectObject.coordinateChessman.y + (squares / 2) + 1, squares + 1] = 2;
                break;
        }

        //selectObject.coordinateChessman の範囲 [-(squares / 2) ~ (squares / 2)](x,y)
        for (int i = (int)selectObject.coordinateChessman.x; i <= (squares / 2); i++)
        {
            chessman[(int)selectObject.coordinateChessman.y + (squares / 2), i + (squares / 2)].transform.position = new Vector2(i + 1, (int)selectObject.coordinateChessman.y) * chessmanPrefab.transform.localScale.x * postionClearance;
            bord[(int)selectObject.coordinateChessman.y + (squares / 2) + 1, i + (squares / 2) + 1] = bord[(int)selectObject.coordinateChessman.y + (squares / 2) + 1, i + (squares / 2)+2];
        }
        chessman[(int)selectObject.coordinateChessman.y + (squares / 2), squares-1].transform.position = new Vector2((squares / 2) + 1 + waku_width, (int)selectObject.coordinateChessman.y) * chessmanPrefab.transform.localScale.x * postionClearance;
        ChessDrow();
        StartCoroutine(LineMoveChessman("left", selectObject.coordinateChessman));
    }

    public void MoveChessmanDownSlide(ChessmanState selectObject)
    {

        switch (selectObject.chessType)
        {
            //bord[y,x]の範囲 [0~squares+2, 0~squares+2]
            case ChessmanState.chessmanType.non_active:
                bord[squares + 1, (int)selectObject.coordinateChessman.x + (squares / 2) + 1] = playerTurn % 2 + 1;
                break;
            case ChessmanState.chessmanType.circle_active:
                bord[squares + 1, (int)selectObject.coordinateChessman.x + (squares / 2) + 1] = 1;
                break;
            case ChessmanState.chessmanType.cross_active:
                bord[squares + 1, (int)selectObject.coordinateChessman.x + (squares / 2) + 1] = 2;
                break;
        }

        //selectObject.coordinateChessman の範囲 [-(squares / 2) ~ (squares / 2)](x,y)
        for (int i = (int)selectObject.coordinateChessman.y; i <= (squares / 2); i++)
        {
            chessman[i + (squares / 2), (int)selectObject.coordinateChessman.x + (squares / 2)].transform.position = new Vector2((int)selectObject.coordinateChessman.x, i + 1) * chessmanPrefab.transform.localScale.x * postionClearance;
            bord[i + (squares / 2) + 1, (int)selectObject.coordinateChessman.x + (squares / 2) + 1] = bord[i + (squares / 2) + 2, (int)selectObject.coordinateChessman.x + (squares / 2) + 1];
        }
        chessman[squares - 1, (int)selectObject.coordinateChessman.x + (squares / 2)].transform.position = new Vector2((int)selectObject.coordinateChessman.x, (squares / 2) + 1 + waku_width) * chessmanPrefab.transform.localScale.x * postionClearance;
        ChessDrow();
        StartCoroutine(LineMoveChessman("down", selectObject.coordinateChessman));
    }

    public void MoveChessmanUpSlide(ChessmanState selectObject)
    {
        switch (selectObject.chessType)
        {
            //bord[y,x]の範囲 [0~squares+2, 0~squares+2]
            case ChessmanState.chessmanType.non_active:
                bord[0, (int)selectObject.coordinateChessman.x + (squares / 2) + 1] = playerTurn % 2 + 1;
                break;
            case ChessmanState.chessmanType.circle_active:
                bord[0, (int)selectObject.coordinateChessman.x + (squares / 2) + 1] = 1;
                break;
            case ChessmanState.chessmanType.cross_active:
                bord[0, (int)selectObject.coordinateChessman.x + (squares / 2) + 1] = 2;
                break;
        }

        //selectObject.coordinateChessman の範囲 [-(squares / 2) ~ (squares / 2)](x,y)
        for (int i = (int)selectObject.coordinateChessman.y; i >= -(squares / 2); i--)
        {
            chessman[i + (squares / 2), (int)selectObject.coordinateChessman.x + (squares / 2)].transform.position = new Vector2((int)selectObject.coordinateChessman.x, i-1) * chessmanPrefab.transform.localScale.x * postionClearance;
            bord[i + (squares / 2) + 1, (int)selectObject.coordinateChessman.x + (squares / 2) + 1] = bord[i +(squares / 2), (int)selectObject.coordinateChessman.x + (squares / 2) + 1];
        }
        chessman[0, (int)selectObject.coordinateChessman.x + (squares / 2)].transform.position = new Vector2((int)selectObject.coordinateChessman.x, -(squares / 2) - 1 - waku_width) * chessmanPrefab.transform.localScale.x * postionClearance;
        ChessDrow();
        StartCoroutine(LineMoveChessman("up", selectObject.coordinateChessman));
    }

    //ずらしたコマを元に戻す
    private IEnumerator LineMoveChessman(string lineMoveDilrection ,Vector2 coordinateChessman)
    {
        yield return new WaitForSeconds(1);
        switch (lineMoveDilrection)
        {
            case "right":
                //選択した１つ横から５つ分動かす
                
                for (int i = (int)coordinateChessman.x ; i >= -(squares / 2); i--)
                {
                    chessman[(int)coordinateChessman.y + (squares / 2), i + (squares / 2)].transform.position = new Vector2( i, (int)coordinateChessman.y) * chessmanPrefab.transform.localScale.x * postionClearance;
                    
                }
                chessman[(int)coordinateChessman.y + (squares / 2), 0].transform.position = new Vector2(-(squares/2), (int)coordinateChessman.y) * chessmanPrefab.transform.localScale.x * postionClearance;
                
                break;

            case "left":
                for (int i = (int)coordinateChessman.x; i <= (squares / 2); i++)
                {
                    chessman[(int)coordinateChessman.y + (squares / 2), i + (squares / 2)].transform.position = new Vector2(i, (int)coordinateChessman.y) * chessmanPrefab.transform.localScale.x * postionClearance;

                }
                chessman[(int)coordinateChessman.y + (squares / 2), squares -1].transform.position = new Vector2((squares / 2), (int)coordinateChessman.y) * chessmanPrefab.transform.localScale.x * postionClearance;
                
                break;

            case "down":
                for (int i = (int)coordinateChessman.y; i <= (squares / 2); i++)
                {
                    chessman[i + (squares / 2), (int)coordinateChessman.x + (squares / 2)].transform.position = new Vector2((int)coordinateChessman.x, i) * chessmanPrefab.transform.localScale.x * postionClearance;

                }
                chessman[squares - 1, (int)coordinateChessman.x + (squares / 2)].transform.position = new Vector2((int)coordinateChessman.x, (squares / 2)) * chessmanPrefab.transform.localScale.x * postionClearance;
                
                break;

            case "up":
                for (int i = (int)coordinateChessman.y; i >= -(squares / 2); i--)
                {
                    chessman[i + (squares / 2), (int)coordinateChessman.x + (squares / 2)].transform.position = new Vector2((int)coordinateChessman.x, i) * chessmanPrefab.transform.localScale.x * postionClearance;

                }
                chessman[0, (int)coordinateChessman.x + (squares / 2)].transform.position = new Vector2((int)coordinateChessman.x, -(squares / 2)) * chessmanPrefab.transform.localScale.x * postionClearance;
                
                break;
        }
        ChessSelectCheck();
        bordJudg.BordTransfer(bord);
        playerTurn++;

        // CPU用の処理
        if (playerTurn % 2 == 1 && bordJudg.Judg)
        {
            // CPUMove();
        }
    }

    // CPU 用の動き
    private void CPUMove()
    {
        ChessmanState[] chessStorage = new ChessmanState[squares * squares];
        int storeageNum = 0;
        int moveChessCount = 0;
        string[] moveChesName = new string[4];
        
        // 選べる場所の取得
        for (int y = 0; y < squares; y++)
        {
            for (int x = 0; x < squares; x++)
            {
                if (chessman[y, x].choicePossible == true)
                {
                    chessStorage[storeageNum] = chessman[y, x];
                    storeageNum++;
                }
            }
        }

        if (chessStorage == null)
        {
            return;
        }
        int run = Random.Range(0, storeageNum);


        // 選べる方向の条件分岐
        {
            if (chessStorage[run].coordinateChessman.x == -squares/2)
            {
                moveChesName[moveChessCount] = "right";
                moveChessCount++;
            }
            if (chessStorage[run].coordinateChessman.x == squares / 2)
            {
                moveChesName[moveChessCount] = "left";
                moveChessCount++;
            }
            if (chessStorage[run].coordinateChessman.y == squares / 2)
            {
                moveChesName[moveChessCount] = "down";
                moveChessCount++;
            }
            if (chessStorage[run].coordinateChessman.y == -squares / 2)
            {
                moveChesName[moveChessCount] = "up";
                moveChessCount++;
            }
        }
        int run2 = Random.Range(0, moveChessCount);

        switch (moveChesName[run2])
        {
            case "right":
                MoveChessmanRightSlide(chessStorage[run]);
                break;
            case "left":
                MoveChessmanLeftSlide(chessStorage[run]);
                break;
            case "down":
                MoveChessmanDownSlide(chessStorage[run]);
                break;
            case "up":
                MoveChessmanUpSlide(chessStorage[run]);
                break;
        }
    }

    public int TurnPlayer()
    {
        return playerTurn;
    }

    // 描画するための処理
    public void ChessDrow()
    {
        
        for (int y = 1; y <= squares; y++)
        {
            for (int x = 1; x <= squares; x++)
            {
                chessman[y - 1, x - 1].SetType(bord[y, x]);
            }
        }
        
    }

    // ターンプレイヤーが選択できるコマ
    private void ChessSelectCheck()
    {
        for (int i = 0; i < squares; i++)
        {

            chessman[i, 0].choicePossible = true;
            chessman[0, i].choicePossible = true;
            chessman[i, squares - 1].choicePossible = true;
            chessman[squares - 1, i].choicePossible = true;
            
        }
        for (int y = 1; y <= squares; y++)
        {
            for (int x = 1; x <= squares; x++)
            {
                if (bord[y, x] == playerTurn % 2 + 1)
                {
                    chessman[y - 1, x - 1].choicePossible = false;
                }
                chessman[y - 1, x - 1].SetType(bord[y, x]);
            }
        }
        
    }
    
    //デバッグ用
    private void BordCheck()
    {
        for (int i=0; i < squares + 2; i++)
        {
            for (int j = 0; j < squares + 2; j++)
            {
                Debug.Log("x"+j+", y"+i+":"+bord[i, j]);
            }
        }
    }

    
}
