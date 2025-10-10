using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class C1OperationUIOut : MonoBehaviour
{
    public PlayableDirector director;
    private Vector2 moveDirection;
    public GameObject operationUI;

    private Color color;

    
    //private bool hasInput = false;

    void Update()
    {
        color = operationUI.GetComponent<SpriteRenderer>().color;
        if (color.a == 1.0f)
        {
            GetInput();
        }
    }

    public void GetInput()
    {
        moveDirection = InputManager.GetInstance().GetMoveDirection();
        Debug.Log("Timeline GetInput");

        if (moveDirection != Vector2.zero)
        {
            Debug.Log("Timeline GetInput Move");
            //hasInput = true; 
            StartCoroutine(FadeOutSprite());
        }
    }

    private IEnumerator FadeOutSprite()
    {
        var spriteRenderer = operationUI.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float duration = 6.0f; // 漸變時間
            float elapsed = 0f;
            color = spriteRenderer.color;
            float startAlpha = color.a;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                color.a = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
                spriteRenderer.color = color;
                yield return null;
            }
            color.a = 0f;
            spriteRenderer.color = color;
        }
    }
}
