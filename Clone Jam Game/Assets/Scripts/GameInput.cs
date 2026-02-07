using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    // --- EVENTLER ---
    // Karýþýklýðý önlemek için ismini deðiþtirdim:
    public event EventHandler OnInteractAction;
    public event EventHandler OnPauseToggle;

    // --- DEÐÝÞKENLER ---
    [SerializeField] private DialogueBox dialogueBox; // Inspector'dan atamayý unutma
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // Etkileþim Tuþu Baðlantýsý
        playerInputActions.Player.Interact.performed += Interact_Performed;

        playerInputActions.Player.Pause.performed += Pause_Performed;
    }

    private void Pause_Performed(InputAction.CallbackContext obj)
    {
        // Event'i tetikle (PauseToggle ismini kullanýyoruz)
        OnPauseToggle?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(InputAction.CallbackContext obj)
    {
        // Diyalog kutusu yoksa veya kapalýysa etkileþime izin ver
        if (dialogueBox == null || !dialogueBox.gameObject.activeSelf)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }
    }

    public Vector2 GetmovementVector()
    {
        // Diyalog açýksa hareket etme
        if (dialogueBox != null && dialogueBox.gameObject.activeSelf)
        {
            return Vector2.zero;
        }

        // Hareket verisini oku
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    // Hafýza temizliði (Memory Leak önlemek için þart)
    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_Performed;
        playerInputActions.Player.Pause.performed -= Pause_Performed;
        playerInputActions.Dispose();
    }
}