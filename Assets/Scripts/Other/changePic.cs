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
        LightSaveData savedLight = saveManager.Instance.GetLightState(gameObject.name);
        if (savedLight != null)
        {
            isFirstSpriteActive = savedLight.isFirstSpriteActive;
            isLightActive = savedLight.isLightOn;
        }

        spriteRenderer.sprite = isFirstSpriteActive ? firstSprite : secondSprite;

        if (sceneLight != null)
        {
            sceneLight.enabled = isLightActive;
        }
    }

    void Update()
    {
        // "O" can switch the sprite
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Space))
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
            isLightActive = !isLightActive;
            sceneLight.enabled = isLightActive;
        }
        saveManager.Instance.SaveLightState(gameObject.name, isLightActive, isFirstSpriteActive);
        saveManager.Instance.SaveGame();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerNear = false;
        }
    }

}
