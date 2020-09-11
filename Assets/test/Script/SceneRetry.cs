using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRetry : MonoBehaviour {
    public UnityAds movie;
    [SerializeField]
    private bool movieFlag = true;  // 動画を流すかどうかのチェック

    public void RetryScene()
    {
        // 最後までやった場合に動画が流れる。
        if (movieFlag)
        {
            movie.MovieFlagOn();
        }
        StartCoroutine("RetrySceneDeley");
    }

    private IEnumerator RetrySceneDeley()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("testScene");
    }

}
