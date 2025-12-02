using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LobbyScreen : MonoBehaviour
{
    public RawImage rawImage;
    public GameObject loadingIcon;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public string videoFileName = "lobby.mp4";

    private bool isPlaying = false;

    void Awake()
    {
        InitVideoSettings();
        // Không load, không play ở đây
    }

    private void InitVideoSettings()
    {
        if (audioSource != null)
        {
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.SetTargetAudioSource(0, audioSource);
            videoPlayer.EnableAudioTrack(0, true);
        }
        else
        {
            videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
        }

        videoPlayer.isLooping = true;
        videoPlayer.renderMode = VideoRenderMode.APIOnly;
    }

    /// <summary>
    /// Gọi hàm này khi muốn load + play video.
    /// Ví dụ gắn vào nút PLAY hoặc lúc mở màn Lobby.
    /// </summary>
    public void LoadAndPlayVideo()
    {
        StartCoroutine(LoadVideoRoutine());
    }

    IEnumerator LoadVideoRoutine()
    {
        string url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName).Replace("\\", "/");
        videoPlayer.url = url;

        videoPlayer.Prepare();
        if (loadingIcon != null) loadingIcon.SetActive(true);

        float t = 0f;
        while (!videoPlayer.isPrepared && t < 8f)
        {
            t += Time.deltaTime;
            yield return null;
        }

        if (!videoPlayer.isPrepared)
        {
            Debug.LogError("VIDEO FAILED TO LOAD");
            yield break;
        }

        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        if (audioSource != null) audioSource.Play();

        isPlaying = true;
        if (loadingIcon != null) loadingIcon.SetActive(false);
    }

    void Update()
    {
        // nếu đang play mà bị dừng bất ngờ thì tự load lại
        if (isPlaying && !videoPlayer.isPlaying)
        {
            StartCoroutine(LoadVideoRoutine());
            isPlaying = false;
        }
    }
}
