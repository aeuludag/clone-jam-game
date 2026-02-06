using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    // Event ismini ve argümanlarýný deðiþtirdik
    public event EventHandler<OnSelectedObjectChangedEventArgs> OnSelectedObjectChanged;
    public class OnSelectedObjectChangedEventArgs : EventArgs
    {
        public InteractableObject selectedObject;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask interactableLayerMask; // Ýsim güncellendi


    private bool isWalking;
    private Vector3 interactDir;
    private InteractableObject selectedObject; // Deðiþken tipi güncellendi

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Birden fazla Player instance'ý var!");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        // Eðer seçili bir obje varsa onun Interact fonksiyonunu çalýþtýr
        if (selectedObject != null)
        {
            selectedObject.Interact();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetmovementVector();

        // 2D yönlendirme
        Vector3 moveDirection = new Vector3(inputVector.x, inputVector.y, 0f);

        if (moveDirection != Vector3.zero)
        {
            interactDir = moveDirection;
        }

        float interactDistance = 2f;

        // 2D Raycast
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, interactDir, interactDistance, interactableLayerMask);
        Debug.DrawRay(transform.position, interactDir * interactDistance, Color.red);
        if (raycastHit.collider != null)
        {
            // Çarptýðýmýz þeyde "InteractableObject" scripti var mý?
            if (raycastHit.transform.TryGetComponent(out InteractableObject interactableObject))
            {
                if (interactableObject != selectedObject)
                {
                    SetSelectedObject(interactableObject);
                }
            }
            else
            {
                SetSelectedObject(null);
            }
        }
        else
        {
            SetSelectedObject(null);
        }

    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetmovementVector();
        Vector3 moveDirection = new Vector3(inputVector.x, inputVector.y, 0f);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .5f;

        // Çarpýþma kontrolü (2D)
        bool canMove = !Physics2D.CircleCast(transform.position, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = (moveDirection.x != 0) && !Physics2D.CircleCast(transform.position, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDirection = moveDirX;
            }
            else
            {
                Vector3 moveDirY = new Vector3(0, moveDirection.y, 0).normalized;
                canMove = (moveDirection.y != 0) && !Physics2D.CircleCast(transform.position, playerRadius, moveDirY, moveDistance);

                if (canMove)
                {
                    moveDirection = moveDirY;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        isWalking = moveDirection != Vector3.zero;
    }

    private void SetSelectedObject(InteractableObject selectedObject)
    {
        this.selectedObject = selectedObject;

        OnSelectedObjectChanged?.Invoke(this, new OnSelectedObjectChangedEventArgs
        {
            selectedObject = selectedObject
        });
    }
}