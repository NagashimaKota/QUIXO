using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuluDisplaySwith : MonoBehaviour {

    public GameObject ruleImage;

    public void RuleOnDisplay()
    {
        ruleImage.SetActive(true);
    }

    public void RuleOffDisplay()
    {
        ruleImage.SetActive(false);
    }
}
