using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PushableRock : MonoBehaviour
{
    [Header("Ayarlar")]
    [SerializeField] private float pushSpeed = 8f;

    private Rigidbody2D rb;
    private bool isPushed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.linearDamping = 0f;

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }
    public void Push(Vector2 direction)
    {
        rb.linearVelocity = direction * pushSpeed;
        isPushed = true;
    }

    private void LateUpdate()
    {
        if (!isPushed)
        {
            rb.linearVelocity = Vector2.zero; 
        }
        isPushed = false;
    }
}