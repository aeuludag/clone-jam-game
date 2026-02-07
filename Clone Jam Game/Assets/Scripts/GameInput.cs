using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnInteractAction;
    public event EventHandler OnPauseToggle;

    [SerializeField] private DialogueBox dialogueBox; 
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_Performed;

        playerInputActions.Player.Pause.performed += Pause_Performed;
    }

    void Start()
    {
        if(dialogueBox == null) { dialogueBox = GameObject.FindGameObjectWithTag("dialogueBox").GetComponent<DialogueBox>(); }
    }

    private void Pause_Performed(InputAction.CallbackContext obj)
    {
        OnPauseToggle?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(InputAction.CallbackContext obj)
    {
        // Diyalog kutusu yoksa veya kapal�ysa etkile�ime izin ver
        if ((dialogueBox == null || !dialogueBox.gameObject.activeSelf))
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }
    }

    public Vector2 GetmovementVector()
    {
        // Diyalog a��ksa hareket etme
        if ((dialogueBox != null && dialogueBox.gameObject.activeSelf))
        {
            return Vector2.zero;
        }

        // Hareket verisini oku
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    // Haf�za temizli�i (Memory Leak �nlemek i�in �art)
    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_Performed;
        playerInputActions.Player.Pause.performed -= Pause_Performed;
        playerInputActions.Dispose();
    }
}