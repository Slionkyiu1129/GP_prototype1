using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeSprite : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    private bool isPlayerNear = false;
    private bool isSpriteShown = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // 一開始隱藏圖片
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.U) && !isSpriteShown)
        {
            spriteRenderer.enabled = true;
            isSpriteShown = true; // 確保只會顯示一次
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
