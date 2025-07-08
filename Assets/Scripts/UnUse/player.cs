
using UnityEngine;

public class player : MonoBehaviour
{
    private Vector2 moveDirection;
    private bool isMoving = false;


    public LayerMask obstacleLayer;
    public float obstacleDetectionRays;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    
    void Update()
    {

        transform.position = new Vector3(Mathf.Round(transform.position.x/ 0.25f) * 0.25f, Mathf.Round(transform.position.y / 0.25f) * 0.25f, transform.position.z);

        

        if (!isMoving)
        {
            // 讀取輸入
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            

            if (moveX != 0) moveY = 0; // 防止斜向移動
            moveDirection = new Vector2(moveX, moveY)/2;

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
