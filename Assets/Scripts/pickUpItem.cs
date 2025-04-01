using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpItem : MonoBehaviour
{
    private bool isHeld = false;  // 判斷物品是否被持有
    private GameObject player;    // 玩家物件
    private Camera mainCamera;    // 主相機
    private GameObject currentItem;  // 當前持有的物品

    void Start()
    {
        mainCamera = Camera.main; // 確保使用主相機
    }

    void Update()
    {
        // 如果玩家按下 P 鍵且點擊到物品
        if (Input.GetKeyDown(KeyCode.P) && !isHeld)
        {
            TryPickupItem();
        }

        // 如果玩家按下 O 鍵並點擊到空地
        if (Input.GetKeyDown(KeyCode.O) && isHeld)
        {
            TryDropItem();
        }
    }

    // 嘗試撿起物品
    void TryPickupItem()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); // 用射線檢測點擊位置
        if (hit.collider != null && hit.collider.CompareTag("PickupItem")) // 確保點擊的是可撿起的物品
        {
            // 撿起物品
            currentItem = hit.collider.gameObject;
            currentItem.SetActive(false); // 隱藏物品（也可以是改變顯示狀態）
            isHeld = true;
        }
    }

    // 嘗試放下物品
    void TryDropItem()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); // 用射線檢測放置位置
        if (hit.collider == null) // 如果點擊的是空地（或沒有碰撞物件）
        {
            // 放下物品
            currentItem.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition); // 將物品放置到點擊位置
            currentItem.SetActive(true); // 顯示物品
            isHeld = false;
        }
    }
}
