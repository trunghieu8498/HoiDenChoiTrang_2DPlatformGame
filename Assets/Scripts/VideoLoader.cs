using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    public RawImage rawImage;
    public GameObject loadingIcon;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public string videoFileName;

    public bool isLooping = false;
    public UnityEvent onVideoEnd;

    private bool hasLoaded = false;

    void Awake()
    {
        InitVideoSettings();
    }

    void InitVideoSettings()
    {
        if (audioSource != null)
        {
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.SetTargetAudioSource(0, audioSource);
        }
        else
        {
            videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
        }

        videoPlayer.isLooping = isLooping;
        videoPlayer.renderMode = VideoRenderMode.APIOnly;
    }

    public void LoadAndPlayVideo()
    {
        StartCoroutine(LoadVideo());
    }

    IEnumerator LoadVideo()
    {
        string url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName).Replace("\\", "/");
        videoPlayer.url = url;

        videoPlayer.Prepare();
        if (loadingIcon != null) loadingIcon.SetActive(true);

        while (!videoPlayer.isPrepared)
            yield return null;

        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        if (audioSource != null) audioSource.Play();

        hasLoaded = true;
        if (loadingIcon != null) loadingIcon.SetActive(false);
    }

    void OnEnable()
    {
        videoPlayer.loopPointReached += HandleEnd;

        // Luôn load video khi scene hoặc object enable
        StartCoroutine(LoadVideo());
    }

    void OnDisable()
    {
        videoPlayer.loopPointReached -= HandleEnd;
    }

    void HandleEnd(VideoPlayer vp)
    {
        if (!isLooping)
            onVideoEnd.Invoke();
    }
}
