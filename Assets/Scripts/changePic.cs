using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class changePic : MonoBehaviour
{
    public Sprite firstSprite;
    public Sprite secondSprite;
    private SpriteRenderer spriteRenderer;
    private bool isPlayerNear = false;
    private bool isFirstSpriteActive = true;
    public Light2D sceneLight;
    private bool isLightActive = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = firstSprite;
        if (sceneLight != null)
        {
            sceneLight.enabled = true;
        }
    }

    void Update()
    {
        // "O" can switch the sprite
        if (isPlayerNear && Input.GetKeyDown(KeyCode.O))
        {
            ToggleSprite();
        }
    }

    private void ToggleSprite()
    {
        if (isFirstSpriteActive)
        {
            spriteRenderer.sprite = secondSprite;
        }
        else
        {
            spriteRenderer.sprite = firstSprite;
        }
        isFirstSpriteActive = !isFirstSpriteActive;

        if (sceneLight != null)
        {
            isLightActive = !isLightActive; // ���� Light ���A
            sceneLight.enabled = isLightActive; // �]�w Light �}��
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �T�O�O���a�i�J
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
