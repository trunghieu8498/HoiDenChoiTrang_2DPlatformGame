using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<StarController> stars = new List<StarController>();

    public void ResetMap()
    {
        foreach (StarController s in stars)
        {
            s.gameObject.SetActive(true);
        }
    }
}
