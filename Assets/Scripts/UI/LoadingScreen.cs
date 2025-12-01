using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitLoading());
    }

    IEnumerator WaitLoading()
    {
        yield return new WaitForSeconds(1f);
        MainMenuManager.Instance.LoadGameScene();
    }
}
