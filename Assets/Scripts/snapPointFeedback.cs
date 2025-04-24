using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapPointFeedback : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite highlightSprite;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = defaultSprite;
    }

    public void Highlight(bool show)
    {
        sr.sprite = show ? highlightSprite : defaultSprite;
    }
}
