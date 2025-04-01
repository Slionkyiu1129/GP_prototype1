using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteMove : MonoBehaviour
{
    
    public float movePx;
    public Vector2 startPosition = new Vector2(12f, -12f);
    public Vector2 endPosition = new Vector2(-24f, 24f);
    
    void Start()
    {
        // 設置初始位置
        transform.position = startPosition;
        StartCoroutine(AutoMove());
    }

    IEnumerator AutoMove()
    {
        while (true) // 無限循環
        {
            // 如果到達終點
            if (transform.position.x <= endPosition.x && transform.position.y >= endPosition.y)
            {
                // 重置到起點
                transform.position = startPosition;
            }

            // 每次移動1個單位
            transform.position = new Vector3(
                transform.position.x - movePx, // 向左移動1單位
                transform.position.y + movePx, // 向上移動1單位
                0
            );

            yield return new WaitForSeconds(0.016f); // 添加約60fps的延遲
        }
    }
}
