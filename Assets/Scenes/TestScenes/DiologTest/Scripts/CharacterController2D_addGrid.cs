using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D_addGrid : MonoBehaviour
{
    [Header("Movement Params")]
    // grid Move
    private Vector2 moveDirection;
    public Vector2 MoveDirection => moveDirection; // 新增這行
    private bool isMoving = false;

    public LayerMask obstacleLayer;
    public float obstacleDetectionRays;
    public bool IsPushing { get; private set; }

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // Debug.DrawRay(transform.position, moveDirection, Color.red);
        Debug.DrawRay(
            (Vector2)transform.position + new Vector2(0, -0.25f),
            moveDirection,
            Color.red
        );
        transform.position = new Vector3(
            Mathf.Round(transform.position.x / 0.25f) * 0.25f,
            Mathf.Round(transform.position.y / 0.25f) * 0.25f,
            transform.position.z
        );
    }

    private void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        if (TogglePack.isInventoryOpen)
        {
            return;
        }

        HandleHorizontalMovement();
    }

    private void HandleHorizontalMovement()
    {
        moveDirection = InputManager.GetInstance().GetMoveDirection();

        if (!isMoving)
        {
            if (moveDirection.x != 0)
                moveDirection.y = 0;
            moveDirection = new Vector2(moveDirection.x, moveDirection.y) / 2;

            if (moveDirection != Vector2.zero)
            {
                Vector2 targetPosition = (Vector2)transform.position + moveDirection;

                if (moveDirection.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1); // 朝右
                }
                else if (moveDirection.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1); // 朝左（鏡像）
                }
                RaycastHit2D hit = Physics2D.Raycast(
                    (Vector2)transform.position + new Vector2(0, -0.25f),
                    moveDirection,
                    obstacleDetectionRays
                );

                if (hit.collider == null)
                {
                    // 沒碰到東西，正常移動
                    IsPushing = false;
                    StartCoroutine(Move(targetPosition));
                }
                else
                {
                    if (hit.collider.CompareTag("Pushable"))
                    {
                        // 碰到可以推的東西，播放推動畫
                        IsPushing = true;
                    }
                    else
                    {
                        IsPushing = false;
                    }
                }
                // if (!Physics2D.Raycast(transform.position, moveDirection, obstacleDetectionRays, obstacleLayer))
                // {
                //     StartCoroutine(Move(targetPosition));
                // }
                //if (
                //   !Physics2D.Raycast(
                //        (Vector2)transform.position + new Vector2(0, -0.25f),
                //        moveDirection,
                //        obstacleDetectionRays,
                //        obstacleLayer
                //    )
                //)
                {
                    StartCoroutine(Move(targetPosition));
                }
            }
        }
    }

    private System.Collections.IEnumerator Move(Vector2 targetPosition)
    {
        isMoving = true;
        float elapsedTime = 0f;
        float duration = 0.1f; // 平滑移動時間

        Vector2 startPosition = transform.position;
        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(
                startPosition,
                targetPosition,
                elapsedTime / duration
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }
}
