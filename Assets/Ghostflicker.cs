using UnityEngine;

public class GhostFlicker : MonoBehaviour
{
    public Renderer rend;
    private float baseAlpha = 0.4f;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float flicker = Mathf.PingPong(Time.time * 0.5f, 0.2f);
        Color c = rend.material.color;
        c.a = baseAlpha + flicker;
        rend.material.color = c;
    }
}
