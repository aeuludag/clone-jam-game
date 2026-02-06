using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PushableRock : MonoBehaviour
{
    [Header("Ayarlar")]

    [SerializeField] private float pushSpeed = 3f;
    [SerializeField] private LayerMask pushableLayerMask; // Sadece taşların olduğu layer


    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Taşı sürtünmeli yap ki itmeyi bırakınca hemen dursun
        rb.linearDamping = 10f;
        rb.gravityScale = 0f; // 2D top-down için yerçekimi kapa
        rb.freezeRotation = true; // Dönmemesi için
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Çarpan obje Player mı?
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            HandlePush(player);
        }
    }

    private void HandlePush(Player player)
    {
        Vector2 moveDir = player.GetMovementVector();

        if (moveDir == Vector2.zero)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float checkDistance = 1.5f; 


        Debug.DrawRay(player.transform.position, moveDir * checkDistance, Color.blue);

        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, moveDir, checkDistance, pushableLayerMask);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            rb.linearVelocity = moveDir * pushSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}