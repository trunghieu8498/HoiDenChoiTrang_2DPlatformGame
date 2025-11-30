using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource gruntSource;
    public AudioClip footstepClip;
    public AudioClip jumpClip;
    public AudioClip landClip;
    public AudioClip gruntClip;


    // thời gian cắt clip
    private float grunt1_Start = 0f;
    private float grunt1_End = 0.40f;

    private float grunt2_Start = 1.45f;
    private float grunt2_End = 1.80f;

    public void PlayFootstep()
    {
        if (!audioSource.isPlaying) audioSource.PlayOneShot(footstepClip);
    }

    public void PlayJump()
    {
        PlayGrunt();
        audioSource.clip = jumpClip;
        audioSource.time = 0.2f;
        audioSource.Play();
    }

    public void PlayLand()
    {
        audioSource.clip = landClip;
        audioSource.time = 0.35f;
        audioSource.Play();
        StartCoroutine(StopAfter(audioSource, 0.2f));
    }

    public void PlayGrunt()
    {
        if (gruntClip == null) return;

        // chọn random grunt1 hoặc grunt2
        bool playGrunt1 = Random.value < 0.5f;

        gruntSource.clip = gruntClip;

        if (playGrunt1)
        {
            gruntSource.time = grunt1_Start;
            gruntSource.Play();
            StartCoroutine(StopAfter(gruntSource, grunt1_End - grunt1_Start));
        }
        else
        {
            gruntSource.time = grunt2_Start;
            gruntSource.Play();
            StartCoroutine(StopAfter(gruntSource, grunt2_End - grunt2_Start));
        }
    }

    private System.Collections.IEnumerator StopAfter(AudioSource _audioSource, float duration)
    {
        yield return new WaitForSeconds(duration);
        _audioSource.Stop();
    }
}
