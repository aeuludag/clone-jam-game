using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedObjectChangedEventArgs> OnSelectedObjectChanged;
    public class OnSelectedObjectChangedEventArgs : EventArgs
    {
        public InteractableObject selectedObject;
    }

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;


    private bool isWalking;
    private Vector3 interactDir;
    private InteractableObject selectedObject;
    private Animator animator;

    private Animator GetAnimator()
    {
        return animator;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            //GameObject temp = Instance.gameObject;
            //Instance = this;
            //Destroy(temp);
            Destroy(Instance);
            return;
        }
        Instance = this;
        animator = GetComponent<Animator>();
    }

    // This acts as a middle-man to start the Coroutine
    private void StartSpawnRoutine()
    {
        StartCoroutine(SpawnWithDelayRoutine());
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        if (!string.IsNullOrEmpty(SceneTransitionManager.TargetSpawnName))
        {
            StartCoroutine(SpawnWithDelayRoutine());
        }
    }
    private IEnumerator SpawnWithDelayRoutine()
    {
        // 1. Wait until the end of the current frame 
        // This allows Unity to finish loading the hierarchy
        yield return new WaitForEndOfFrame();

        // 2. (Optional) Wait for a physical split-second 
        // yield return new WaitForSeconds(0.05f); 

        if (!string.IsNullOrEmpty(SceneTransitionManager.TargetSpawnName))
        {
            GameObject spawnPoint = GameObject.Find(SceneTransitionManager.TargetSpawnName);

            if (spawnPoint != null)
            {
                // Teleport
                transform.position = spawnPoint.transform.position;
                Debug.Log("Teleported to: " + spawnPoint.name);
            }
            else
            {
                Debug.LogError("Could not find: " + SceneTransitionManager.TargetSpawnName);
            }

            // 3. Clear the static data ONLY after the move is confirmed
            SceneTransitionManager.TargetSpawnName = null;
        }
    }



    /*private void MoveToSpawnPoint()
    {
        GameObject spawnPoint = GameObject.Find(SceneTransitionManager.TargetSpawnName);
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
    }*/


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
        animator.SetBool("IsWalking", IsWalking());
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