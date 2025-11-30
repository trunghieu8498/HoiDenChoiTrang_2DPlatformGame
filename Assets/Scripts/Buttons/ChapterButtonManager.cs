using UnityEngine;
using UnityEngine.UI;

public class ChapterButtonManager : MonoBehaviour
{
    public ChapterButton[] buttons;

    public void OnButtonClick(int index)
    {
        if (buttons[index].isUnlocked)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i != index)
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void ShowAllChapters()
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true);
            button.SetUnSelected();
        }
    }
}
