using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class StarPool : MonoBehaviour
{
    public List<StarController> stars = new List<StarController>();

    void Start()
    {
        stars = new List<StarController>(GetComponentsInChildren<StarController>());
    }

    public void ResetStars()
    {
        Debug.Log("Resetting Stars in StarPool");
        foreach (var star in stars)
        {
            star.gameObject.SetActive(true);
        }
    }
}
