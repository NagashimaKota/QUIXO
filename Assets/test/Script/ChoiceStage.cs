using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceStage : MonoBehaviour {

    public Bord bord;

    private void Start()
    {
        bord.squares = 3 + this.GetComponent<Dropdown>().value * 2;
    }

    public void ChangeStage(Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                bord.squares = 3;
                break;
            case 1:
                bord.squares = 5;
                break;
            case 2:
                bord.squares = 7;
                break;
        }

    }

}
