using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private InteractableObject heldObject; 
    private Vector3 lastInteractDir;       

    public event EventHandler<OnSelectedObjectChangedEventArgs> OnSelectedObjectChanged;

    public class OnSelectedObjectChangedEventArgs : EventArgs
    {
        public InteractableObject selectedObject;
    }

    [SerializeField] private float moveSpeed = 7f; 
    [SerializeField] private GameInput gameInput;

    [SerializeField] private LayerMask interactionsLayerMask;
    [SerializeField] private LayerMask collisionsLayerMask;


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
        // SENARYO 1: ELÝM DOLU MU? (Önce býrakmayý dene)
        if (heldObject != null)
        {
            // "Elimdeki þey bir CarryableBox mý?" diye kontrol et ve dönüþtür
            if (heldObject is CarryableBox boxToDrop)
            {
                Vector3 dropPosition = transform.position + (lastInteractDir*1.5f);
                boxToDrop.Drop(dropPosition);
            }

            heldObject = null; // Eli boþalt
        }
        // SENARYO 2: ELÝM BOÞ VE YERDE BÝR ÞEY VAR MI?S
        else if (selectedObject != null)
        {
            // "Yerdeki þey bir CarryableBox mý?" diye kontrol et
            if (selectedObject is CarryableBox boxToPick)
            {
                // Evet, bu bir kutu! O zaman AL.
                heldObject = boxToPick;
                boxToPick.PickUp();
                SetSelectedObject(null);
            }
            else
            {
                // Hayýr, bu kutu deðil (Tabela, NPC vb.). Normal etkileþime gir.
                selectedObject.Interact();
            }
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();

        // Animasyon Kodlarý
        Vector2 dir = gameInput.GetmovementVector();
        if (dir != Vector2.zero)
        {
            animator.SetFloat("InputX", dir.x);
            animator.SetFloat("InputY", dir.y);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
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
            lastInteractDir = moveDirection; 
        }

        if (moveDirection != Vector3.zero)
        {
            interactDir = moveDirection;
        }

        float interactDistance = 2f;

        // Etkileþim için LayerMask kullanmýyoruz, her þeyi görebilir.
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, interactDir, interactDistance, interactionsLayerMask);

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

        // DÜZELTME: collisionsLayerMask parametresini geri ekledik.
        // Böylece Trigger'larý görmezden gelebileceðiz.
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, playerRadius, moveDirection, moveDistance, collisionsLayerMask);

        bool canMove = true;

        if (hit.collider != null)
        {
            canMove = false;

            // Taþa çarpýnca itme kodu
            if (hit.collider.TryGetComponent(out PushableRock rock))
            {
                rock.Push(moveDirection.normalized);
            }
        }

        if (!canMove)
        {
            // Kayma (Sliding) mantýðýna da maskeyi ekledik
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = (moveDirection.x != 0) && !Physics2D.CircleCast(transform.position, playerRadius, moveDirX, moveDistance, collisionsLayerMask);

            if (canMove)
            {
                moveDirection = moveDirX;
            }
            else
            {
                Vector3 moveDirY = new Vector3(0, moveDirection.y, 0).normalized;
                canMove = (moveDirection.y != 0) && !Physics2D.CircleCast(transform.position, playerRadius, moveDirY, moveDistance, collisionsLayerMask);

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