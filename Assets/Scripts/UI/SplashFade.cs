using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashFade : MonoBehaviour
{
    public Image gameSplash;
    public Image studioSplash;
    public Image sauSplash;

    public string loadScene;

    public float imageScreenTime = 2.5f;
    public float emptyScreenTime = 2.5f;

    IEnumerator Start()
    {
        sauSplash.canvasRenderer.SetAlpha(0.0f);
        gameSplash.canvasRenderer.SetAlpha(0.0f);
        studioSplash.canvasRenderer.SetAlpha(0.0f);

        FadeIn(sauSplash);
        yield return new WaitForSeconds(imageScreenTime);
        FadeOut(sauSplash);
        yield return new WaitForSeconds(emptyScreenTime);

        FadeIn(studioSplash);
        yield return new WaitForSeconds(imageScreenTime);
        FadeOut(studioSplash);
        yield return new WaitForSeconds(emptyScreenTime);

        FadeIn(gameSplash);
        yield return new WaitForSeconds(imageScreenTime);
        FadeOut(gameSplash);
        yield return new WaitForSeconds(emptyScreenTime);

        SceneManager.LoadScene(loadScene);
    }

    void FadeIn(Image i)
    {
        i.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut(Image i)
    {
        i.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
