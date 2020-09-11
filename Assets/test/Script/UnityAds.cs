using UnityEngine;
// 追記
using UnityEngine.Advertisements;


public class UnityAds : MonoBehaviour {
    public static bool movieFlag = false;
    
    private void Start()
    {
        
        if (movieFlag)
        {
            ShowAd();
            movieFlag = false;
        }
    }

    public void MovieFlagOn()
    {
        movieFlag = true;
    }
    
    // 広告を表示するメソッド。UIのボタンなどで呼ぼう
    public void ShowAd()
    {
        
        // IsReadyは広告の準備ができていれば「ture」を返す
        if (Advertisement.IsReady())
        {
            // 広告の表示
            Advertisement.Show();
        }
        else
        {
            Debug.Log("non");
        }
    }
}
