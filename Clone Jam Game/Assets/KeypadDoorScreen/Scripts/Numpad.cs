using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Numpad : MonoBehaviour
{
    public string currentNumber;
    public string correctNumber;
    public Sprite openSprite;
    public Sprite closedSprite;
    public bool isOpen = false;
    public TextMeshPro textMeshPro;
    private PlayerInputActions playerInputActions;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask buttonLayer; // Set this to your 'UI_World' layer in the inspector

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        playerInputActions.Enable();
        playerInputActions.Player.MouseClick.performed += HandleInteraction;
    }

    void OnDisable()
    {
        playerInputActions.Disable();
    }

    void HandleInteraction(InputAction.CallbackContext context)
    {
        // 1. Convert mouse position to World Point
        Vector3 mousePos = Camera.main.ScreenToWorldPoint((Vector3)Mouse.current.position.ReadValue());
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // 2. Raycast at that position
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 0f, buttonLayer);

        // 3. Check if we hit something
        if (hit.collider != null)
        {
            string buttonValue = hit.collider.gameObject.name;
            Send(buttonValue);
        }
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closedSprite;
    }

    public void Send(string number)
    {
        currentNumber += number;
        textMeshPro.text = $"CODE: {currentNumber}";
        if(correctNumber.Length <= currentNumber.Length && correctNumber != currentNumber)
        {
            isOpen = false;
            currentNumber = "";
            spriteRenderer.sprite = closedSprite;
            textMeshPro.text = $"WRONG!";
        } else if(correctNumber == currentNumber)
        {
            isOpen = true;
            spriteRenderer.sprite = openSprite;
            textMeshPro.text = $"CORRECT!";
        }
    }
}
