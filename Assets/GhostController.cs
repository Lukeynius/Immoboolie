using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;

        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }
}
