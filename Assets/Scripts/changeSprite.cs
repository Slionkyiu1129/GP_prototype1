using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeSprite : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites; // 存多張圖
    private bool inLine = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeSprite(int index)
    {
        if (index >= 0 && index < sprites.Length) 
        {
            spriteRenderer.sprite = sprites[index]; // 切換到指定的圖
        }
    }

    void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.tag == "Sprite"){
            //Debug.Log("InLine");
            inLine = true;
            ChangeSprite(1);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Sprite"){
            //Debug.Log("OutLine");
            inLine = false;
            ChangeSprite(0);
        }
    }
}
