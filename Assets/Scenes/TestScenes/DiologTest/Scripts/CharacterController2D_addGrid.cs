using UnityEngine;

// This script is a basic 2D character controller that allows
// the player to run and jump. It uses Unity's new input system,
// which needs to be set up accordingly for directional movement
// and jumping buttons.

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D_addGrid : MonoBehaviour
{

    [Header("Movement Params")]


    // grid Move
    private Vector2 moveDirection;
    private bool isMoving = false;


    public LayerMask obstacleLayer;
    public float obstacleDetectionRays;



    private void Awake()
    {

    }

    void Start()
    {
        Application.targetFrameRate = 60;
    }


    void Update()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x / 0.25f) * 0.25f, Mathf.Round(transform.position.y / 0.25f) * 0.25f, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
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
            if (moveDirection.x != 0) moveDirection.y = 0;
            moveDirection = new Vector2(moveDirection.x, moveDirection.y) / 2;

            if (moveDirection != Vector2.zero)
            {
                Vector2 targetPosition = (Vector2)transform.position + moveDirection;

                if (!Physics2D.Raycast(transform.position, moveDirection, obstacleDetectionRays, obstacleLayer))
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
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }


}