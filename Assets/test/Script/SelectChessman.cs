using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChessman : MonoBehaviour {

    public RectTransform[] uiButtonView; //BTLR
    public GameObject selectObject;
    public Bord stegeBord;

    private Transform selectObjectTransform;
    private ChessmanState selectChess;
    private bool f = true;

    private void Start()
    {
        selectObjectTransform = selectObject.GetComponent<Transform>();
    }

    void Update()
    {

        //ボタンを上げたとき
        if (Input.GetMouseButtonUp(0) && f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            
            //なにかと衝突した時、それが選択できるオブジェクトなら処理
            if (hit.collider && hit.collider.GetComponent<ChessmanState>().choicePossible)
            {
                selectChess = hit.collider.GetComponent<ChessmanState>();

                if ((int)selectChess.chessType == 3 || (int)selectChess.chessType == stegeBord.TurnPlayer() % 2  + 4)
                {
                    selectObjectTransform.position = hit.transform.position;
                    selectObject = selectChess.gameObject;
                    //selectObject.gameObject.SetActive(false);

                    SpriteRenderer sp = selectObject.GetComponent<SpriteRenderer>();
                    sp.color = new Color(1, 1, 1, 0.5f);
                    
                    UiViewer();
                }
            }
            else
            {
                SpriteRenderer sp = selectObject.GetComponent<SpriteRenderer>();
                sp.color = new Color(1, 1, 1, 1);
                //selectObject.SetActive(true);
                StartCoroutine(UiButtonOllFalse());
            }
        }
        
        //ボタンを押したとき
        if (Input.GetMouseButtonDown(0))
        {
            //selectObject.SetActive(true);
            SpriteRenderer sp = selectObject.GetComponent<SpriteRenderer>();
            sp.color = new Color(1, 1, 1, 1);
        }
    }

    private IEnumerator UiButtonOllFalse(float time = 0)
    {
        yield return new WaitForSeconds(time);
        uiButtonView[0].gameObject.SetActive(false);
        uiButtonView[1].gameObject.SetActive(false);
        uiButtonView[2].gameObject.SetActive(false);
        uiButtonView[3].gameObject.SetActive(false);

        selectObject.SetActive(true);
    }

    void UiViewer()
    {
        //BTLR
        
        //下の５つを選択
        if (selectChess.coordinateChessman.y == -(stegeBord.squares/2) && -(stegeBord.squares / 2) <= selectChess.coordinateChessman.x && selectChess.coordinateChessman.x <= (stegeBord.squares / 2))
        {
            uiButtonView[0].gameObject.SetActive(false);
            uiButtonView[1].gameObject.SetActive(true);
            uiButtonView[2].gameObject.SetActive(true);
            uiButtonView[3].gameObject.SetActive(true);

            if (selectChess.coordinateChessman.x == -(stegeBord.squares / 2))
            {
                uiButtonView[2].gameObject.SetActive(false);
            }
            else if (selectChess.coordinateChessman.x == (stegeBord.squares / 2))
            {
                uiButtonView[3].gameObject.SetActive(false);
            }

            uiButtonView[0].position = new Vector3(RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).x, uiButtonView[0].position.y, uiButtonView[0].position.z);
            uiButtonView[1].position = new Vector3(RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).x, uiButtonView[1].position.y, uiButtonView[1].position.z);
            uiButtonView[2].position = new Vector3(uiButtonView[2].position.x, RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).y, uiButtonView[2].position.z);
            uiButtonView[3].position = new Vector3(uiButtonView[3].position.x, RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).y, uiButtonView[3].position.z);

        }

        //上の５つを選択
        if (selectChess.coordinateChessman.y == (stegeBord.squares / 2) && -(stegeBord.squares / 2) <= selectChess.coordinateChessman.x && selectChess.coordinateChessman.x <= (stegeBord.squares / 2))
        {

            uiButtonView[0].gameObject.SetActive(true);
            uiButtonView[1].gameObject.SetActive(false);
            uiButtonView[2].gameObject.SetActive(true);
            uiButtonView[3].gameObject.SetActive(true);

            if (selectChess.coordinateChessman.x == -(stegeBord.squares / 2))
            {
                uiButtonView[2].gameObject.SetActive(false);
            }
            else if (selectChess.coordinateChessman.x == (stegeBord.squares / 2))
            {
                uiButtonView[3].gameObject.SetActive(false);
            }
            uiButtonView[0].position = new Vector3(RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).x, uiButtonView[0].position.y, uiButtonView[0].position.z);
            uiButtonView[1].position = new Vector3(RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).x, uiButtonView[1].position.y, uiButtonView[1].position.z);
            uiButtonView[2].position = new Vector3(uiButtonView[2].position.x, RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).y, uiButtonView[2].position.z);
            uiButtonView[3].position = new Vector3(uiButtonView[3].position.x, RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).y, uiButtonView[3].position.z);

        }

        //左の５つを選択
        if (selectChess.coordinateChessman.x == -(stegeBord.squares / 2) && -(stegeBord.squares / 2) <= selectChess.coordinateChessman.y && selectChess.coordinateChessman.y <= (stegeBord.squares / 2))
        {
            uiButtonView[0].gameObject.SetActive(true);
            uiButtonView[1].gameObject.SetActive(true);
            uiButtonView[2].gameObject.SetActive(false);
            uiButtonView[3].gameObject.SetActive(true);

            if (selectChess.coordinateChessman.y == (stegeBord.squares / 2))
            {
                uiButtonView[1].gameObject.SetActive(false);
            }
            else if (selectChess.coordinateChessman.y == -(stegeBord.squares / 2))
            {
                uiButtonView[0].gameObject.SetActive(false);
            }

            uiButtonView[0].position = new Vector3(RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).x, uiButtonView[0].position.y, uiButtonView[0].position.z);
            uiButtonView[1].position = new Vector3(RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).x, uiButtonView[1].position.y, uiButtonView[1].position.z);
            uiButtonView[2].position = new Vector3(uiButtonView[2].position.x, RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).y, uiButtonView[2].position.z);
            uiButtonView[3].position = new Vector3(uiButtonView[3].position.x, RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).y, uiButtonView[3].position.z);

        }
        //右に５つに選択
        if (selectChess.coordinateChessman.x == (stegeBord.squares / 2) && -(stegeBord.squares / 2) <= selectChess.coordinateChessman.y && selectChess.coordinateChessman.y <= (stegeBord.squares / 2))
        {
            uiButtonView[0].gameObject.SetActive(true);
            uiButtonView[1].gameObject.SetActive(true);
            uiButtonView[2].gameObject.SetActive(true);
            uiButtonView[3].gameObject.SetActive(false);

            if (selectChess.coordinateChessman.y == (stegeBord.squares / 2))
            {
                uiButtonView[1].gameObject.SetActive(false);
            }
            else if (selectChess.coordinateChessman.y == -(stegeBord.squares / 2))
            {
                uiButtonView[0].gameObject.SetActive(false);
            }

            uiButtonView[0].position = new Vector3(RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).x, uiButtonView[0].position.y, uiButtonView[0].position.z);
            uiButtonView[1].position = new Vector3(RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).x, uiButtonView[1].position.y, uiButtonView[1].position.z);
            uiButtonView[2].position = new Vector3(uiButtonView[2].position.x, RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).y, uiButtonView[2].position.z);
            uiButtonView[3].position = new Vector3(uiButtonView[3].position.x, RectTransformUtility.WorldToScreenPoint(Camera.main, selectChess.gameObject.transform.position).y, uiButtonView[3].position.z);

        }
    }
    
    //ボタンからコマの移動をするときに呼ぶ関数
    public void ChengeChessmanPosition(string name)
    {
        f = false;
        switch (name)
        {
            case "B":
                
                Debug.Log("↑");
                stegeBord.MoveChessmanUpSlide(selectChess);
                break;
            case "T":
                Debug.Log("↓");
                stegeBord.MoveChessmanDownSlide(selectChess);
                break;
            case "L":
                Debug.Log("→");
                stegeBord.MoveChessmanRightSlide(selectChess);
                break;
            case "R":
                Debug.Log("←");
                stegeBord.MoveChessmanLeftSlide(selectChess);
                break;
        }
        f = true;
    }


}
