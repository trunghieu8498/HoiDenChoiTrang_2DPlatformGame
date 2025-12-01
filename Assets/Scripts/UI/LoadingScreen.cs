using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video;

public class LoadingScreen : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    void Start()
    {
        StartCoroutine(PlayVideo());
    }

    // Play the video and wait for it to finish before loading the game scene
    IEnumerator PlayVideo()
    {
        videoPlayer.Play();
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f); // Small delay to ensure smooth transition
        MainMenuManager.Instance.LoadGameScene();
    }
}
