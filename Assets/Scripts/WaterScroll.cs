using UnityEngine;

public class WaterScroll : MonoBehaviour
{
    public Material mat;
    public float speed = 0.2f;

    void Update()
    {
        Vector2 off = mat.GetTextureOffset("_MainTex");
        off.x += speed * Time.deltaTime;
        mat.SetTextureOffset("_MainTex", off);
    }
}
