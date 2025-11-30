using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }
    public GameObject mainMenuUI;
    public GameObject logoScreen;
    
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        logoScreen.SetActive(true);
        logoScreen.GetComponent<Animator>().Play("Show");
    }
    
}
