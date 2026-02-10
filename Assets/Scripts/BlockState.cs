using UnityEngine;
//-----------------------------------------------

public class BlockState : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public bool IsStopped()
    {
        return rb.linearVelocity.magnitude < 0.05f;
    }
}
