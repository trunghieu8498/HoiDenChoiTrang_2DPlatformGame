using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiepTucButton : MonoBehaviour
{
    public void OnTiepTucButtonClicked()
    {
        GameManager.Instance.GoToColoringGame();
    }
}
