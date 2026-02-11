using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PushableRock : MonoBehaviour
{
    [Header("Ayarlar")]
    // 0.0001f çok düþüktü, onu 5f gibi hissedilir bir hýza çektim.
    [SerializeField] private float pushSpeed = 5f;

    private Rigidbody2D rb;
    private bool isPushed; // Bu frame'de itildi mi kontrolü

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        
        rb.gravityScale = 0f;          
        rb.freezeRotation = true;      
        rb.linearDamping = 0f;         
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; 
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