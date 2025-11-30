using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitor : MonoBehaviour
{
    public static Transitor Instance { get; private set; }
    private Animator animator;
    public Transform player;
    private Vector2 newPlayerPosition;
    private bool isNextMap = true;

    void Awake()
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
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SwitchToBlack(bool _isNextMap, Vector2 newPos)
    {
        newPlayerPosition = newPos;
        animator.SetTrigger("Switch");
        Debug.Log("switch to black");
        isNextMap = _isNextMap;
    }
    public void SwitchToClear()
    {
        if (isNextMap)
            GameManager.Instance.NextMap();
        else
            GameManager.Instance.PreviousMap();

        player.position = newPlayerPosition;
        animator.SetTrigger("Switch");
    }
}
