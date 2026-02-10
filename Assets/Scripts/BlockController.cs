using UnityEngine;
using UnityEngine.InputSystem;
//--------------------------------------------------
public class BlockController : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;
    BlockSpawner spawner;

    bool hasLanded = false;
    float aliveTime = 0f;


    [Header("着地判定までの猶予時間")]
    public float landingDelay = 0.3f;

    [Header("横移動スピード")]
    public float moveSpeed = 5f;

    [Header("接地判定")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckDistance = 0.02f;

//-----------------------------------------------------------
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void SetSpawner(BlockSpawner s)
    {
        spawner = s;
    }

    void Update()
    {
        if (hasLanded) return;

        aliveTime += Time.deltaTime;
        HandleMove();

        if (aliveTime < landingDelay) return;

        //着地判定
        if (IsGrounded())
        {
            hasLanded = true;

            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;

            spawner?.OnBlockLanded();
            
            GameManager.Instance.OnBlockLanded(this);
        }
    }

    //左右移動
    void HandleMove()
    {
        float move = 0f;

        if (Keyboard.current.aKey.isPressed) move = -1f;
        if (Keyboard.current.dKey.isPressed) move = 1f;

        Vector2 velocity = rb.linearVelocity;
        velocity.x = move * moveSpeed;
        rb.linearVelocity = velocity;
    }

bool IsGrounded()
{
   Bounds b = col.bounds;

    float dist = groundCheckDistance;
Vector2 left   = new Vector2(b.min.x + 0.05f, b.min.y + 0.01f);
Vector2 center = new Vector2(b.center.x,      b.min.y + 0.01f);
Vector2 right  = new Vector2(b.max.x - 0.05f, b.min.y + 0.01f);

    return
        Physics2D.Raycast(left, Vector2.down, dist, groundLayer) ||
        Physics2D.Raycast(center, Vector2.down, dist, groundLayer) ||
        Physics2D.Raycast(right, Vector2.down, dist, groundLayer);
}
}