using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameObjct : MonoBehaviour {

    public GameObject onBord;
    public GameObject onBackGroundImage;
    public GameObject titleBackImage;
    public GameObject returnButton;

    public void BordOnStart()
    {
        StartCoroutine("BordStartOn");
    }
    
    private IEnumerator BordStartOn()
    {
        yield return new WaitForSeconds(0.5f);

        onBord.SetActive(true);
        onBackGroundImage.SetActive(true);
        titleBackImage.SetActive(false);
        this.transform.parent.gameObject.SetActive(false);
        returnButton.SetActive(true);
    }
}
