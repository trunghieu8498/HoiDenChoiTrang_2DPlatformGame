using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        StartCoroutine(FillBar());
    }

    IEnumerator FillBar()
    {
        float time = 0f;
        float duration = 5f;

        while (time < duration)
        {
            time += Time.deltaTime;
            slider.value = Mathf.Lerp(0f, 1f, time / duration);
            yield return null;
        }

        slider.value = 1f;
        // MainMenuManager.Instance.LoadGameScene();
    }
}
