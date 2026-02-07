using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    public Vector3 offset;
    public Chocolate itemHeld;
    public Collider2D belt;
    public float grabRadius = 1.0f;
    
    private PlayerInputActions playerInputActions;
    [SerializeField] private Collider2D handHitbox;
    [DoNotSerialize] public int points;

    public AudioClip good;
    public AudioClip bad;
    public AudioSource audioSource;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        playerInputActions.Enable();
        playerInputActions.Player.MouseClick.performed += OnClick;
    }

    void OnDisable()
    {
        playerInputActions.Disable();
    }

    void Update()
    {
        // 1. Convert Screen Space (Mouse) to World Space
        var mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        
        // 2. Apply position to the hand
        transform.position = worldPos + offset;
        
        if (itemHeld != null)
        {
            itemHeld.transform.position = worldPos;
        }
    }

    public void OnClick(InputAction.CallbackContext obj)
    {
        if (itemHeld == null)
        {
            GetChocolate();
        }
        else
        {
            DropChocolate();
        }
    }

    public void GetChocolate()
    {
        // USE transform.position (World Space), NOT mousePos (Screen Space)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position - offset, grabRadius);
        
        Chocolate nearestChocolate = null;
        float minDistance = Mathf.Infinity;

        foreach (var col in colliders)
        {
            Chocolate chocolate = col.GetComponent<Chocolate>();
            if (chocolate != null)
            {
                float dist = Vector2.Distance(transform.position - offset, col.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    nearestChocolate = chocolate;
                }
            }
        }

        if (nearestChocolate != null)
        {
            nearestChocolate.isHeld = true;
            itemHeld = nearestChocolate;
        }
    }

    public void DropChocolate()
    {
        if (itemHeld == null) return;

        itemHeld.isHeld = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position - offset, grabRadius);

        foreach (var col in colliders)
        {
            ChocolateBox chocolateBox = col.GetComponent<ChocolateBox>();
            if (chocolateBox != null)
            {
                chocolateBox.AddChocolate(itemHeld);
                break;
            }
        }
        if(!colliders.Contains(belt))
        {
            itemHeld.isHeld = true;
        }

        itemHeld = null;
    }

    public void IncreasePoint()
    {
        audioSource.clip = good;
        audioSource.Play();
        points++;
    }

    public void DecreasePoint()
    {
        audioSource.clip = bad;
        audioSource.Play();
        points--;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw at the object's current world position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position - offset, grabRadius);
    }
}