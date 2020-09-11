using UnityEngine;
using UnityEngine.Advertisements;//Advertisementクラスを使うのに必要

public class Manetizetion : MonoBehaviour {

	// Use this for initialization
	void Start () {

        string gameID = "3630599";

        //広告の初期化
        Advertisement.Initialize(gameID, testMode: true);
        
    }
	
	// Update is called once per frame
	void Update () {
	}
}
