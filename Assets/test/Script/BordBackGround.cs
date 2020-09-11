using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordBackGround : MonoBehaviour {

    public Sprite[] backGround;
    public SpriteRenderer backGroundImage;
    private Bord bord;

    private void Start()
    {
        bord = this.GetComponent<Bord>();
        switch (bord.squares)
        {
            case 3:
                backGroundImage.sprite = backGround[0];
                backGroundImage.transform.localScale *= 1.5f;
                break;
            case 5:
                backGroundImage.sprite = backGround[1];
                break;
            case 7:
                backGroundImage.sprite = backGround[2];
                backGroundImage.transform.localScale /= 1.5f;
                break;
        }
    }
}
