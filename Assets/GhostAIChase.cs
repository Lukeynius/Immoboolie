using UnityEngine;

public class SimpleChase : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    void Update()
    {
        if (player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;

        // Höhe ignorieren, damit der Geist nicht nach oben/unten rennt
        dir.y = 0;

        transform.position += dir * speed * Time.deltaTime;
    }
}
