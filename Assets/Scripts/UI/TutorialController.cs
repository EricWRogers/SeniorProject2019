using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public VideoClip tutorialClip;

    public GameObject skipText;

    double tutorialLength;

    public string gameScene = "DuplicateMain";

    bool wantsToSkip = false;
    float wantsToSkipTime;
    float skipTimeout = 2f;

    void Start()
    {
        tutorialLength = tutorialClip.length;
        skipText.SetActive(false);
        TutorialOverCall();
    }
    
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {           

            if(wantsToSkip)
            {
                SceneManager.LoadScene(gameScene);
            }

            wantsToSkip = true;
            wantsToSkipTime = Time.time;
        }

        if(Time.time > wantsToSkipTime + skipTimeout)
        {
            wantsToSkip = false;
        }

        if(wantsToSkip)
        {
            skipText.SetActive(true);
        }
        else
        {
            skipText.SetActive(false);
        }
    }

    void TutorialOverCall()
    {
        StartCoroutine(TutorialOver());
    }

    IEnumerator TutorialOver()
    {
        videoPlayer.Play();

        yield return new WaitForSeconds((float)tutorialLength);

        SceneManager.LoadScene(gameScene);
    }
}
