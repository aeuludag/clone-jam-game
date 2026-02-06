using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedObjectChangedEventArgs> OnSelectedObjectChanged;
    public class OnSelectedObjectChangedEventArgs : EventArgs
    {
        public InteractableObject selectedObject;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    // REMOVED: LayerMasks are removed since you are using Default for everything
    // [SerializeField] private LayerMask interactableLayerMask; 
    // [SerializeField] private LayerMask collisionsLayerMask;


    private bool isWalking;
    private Vector3 interactDir;
    private InteractableObject selectedObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance!");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
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
        Vector3 moveDirection = new Vector3(inputVector.x, inputVector.y, 0f);

        if (moveDirection != Vector3.zero)
        {
            interactDir = moveDirection;
        }

        float interactDistance = 2f;

        // UPDATED: Removed layerMask parameter. It now checks everything on Default layer.
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, interactDir, interactDistance);

        if (raycastHit.collider != null)
        {
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

        // UPDATED: Removed collisionsLayerMask. It detects everything.
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, playerRadius, moveDirection, moveDistance);

        bool canMove = true;

        if (hit.collider != null)
        {
            // Stop movement if we hit anything
            canMove = false;

            // Check if it is a Rock
            if (hit.collider.TryGetComponent(out PushableRock rock))
            {
                // Push the rock
                rock.Push(moveDirection.normalized);
            }
        }

        if (!canMove)
        {
            // Sliding Logic (Updated to remove LayerMasks)
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

    public Vector2 GetMovementVector()
    {
        return gameInput.GetmovementVector();
    }
}