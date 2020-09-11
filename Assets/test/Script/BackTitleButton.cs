using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTitleButton : MonoBehaviour {

    public GameObject returnReallyImage;

    public void OnObject()
    {
        returnReallyImage.SetActive(true);
    }

    public void OffObject()
    {
        returnReallyImage.SetActive(false);
    }
}
