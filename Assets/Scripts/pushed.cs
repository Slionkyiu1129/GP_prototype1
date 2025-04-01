using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushed : MonoBehaviour
{
    private bool isMoving = false;
    public LayerMask obstacleLayer;



    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isMoving)
        {
            // 获取玩家的输入
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            if (moveX != 0) moveY = 0; // 防止斜向移动
            Vector2 pushDirection = new Vector2(moveX, moveY);
            // Debug.Log(pushDirection);
            Vector2 targetPosition = (Vector2)transform.position + pushDirection;
            


            bool canPush = false;
            Vector2 playerPosition = collision.gameObject.transform.position; //取得玩家位置

            if(pushDirection.x == 1 && playerPosition.y == transform.position.y)  //向右推
                canPush = true;
            if(pushDirection.x == -1 && playerPosition.y == transform.position.y)  //向左推
                canPush = true;
            if(pushDirection.y == 1 && playerPosition.y == transform.position.y - 1.0f)  //向上推
                canPush = true;
             if(pushDirection.y == -1 && playerPosition.y == transform.position.y + 1.0f)  //向下推
                canPush = true;

            if (canPush)
            {
                // 检查目标位置是否有障碍物
                if (!Physics2D.Raycast(transform.position, pushDirection, 0.6f, obstacleLayer))
                {
                    StartCoroutine(Move(targetPosition));
                }
            }
        }
    }

    private IEnumerator Move(Vector2 snappedPosition)
    {
        isMoving = true;
        float elapsedTime = 0f;
        float duration = 0.1f; // 平滑移动时间

        Vector2 startPosition = transform.position;
        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(startPosition, snappedPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = snappedPosition;
        isMoving = false;
    }

    // Vector2 SnapToGrid(Vector2 targetPosition)
    // {
    //     float snappedX = Mathf.Round(targetPosition.x / 0.5f) * 0.5f;
    //     float snappedY = Mathf.Round(targetPosition.y / 0.5f) * 0.5f;
    //     return new Vector2(snappedX, snappedY);
    // }

}
