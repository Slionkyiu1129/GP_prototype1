using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCmoveaway : MonoBehaviour
{
    public float speed = 2f;
    private bool moveUp = false;

    void Update()
    {
        if (moveUp)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if (transform.position.y > Camera.main.transform.position.y + 10f)
            {
                Destroy(gameObject); // 移出畫面後刪掉
            }
        }
    }

    public void StartMoveUp()
    {
        moveUp = true;
    }
}
