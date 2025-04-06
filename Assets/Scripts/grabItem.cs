using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class grabItem : MonoBehaviour
{
    public Transform grabPoint; // 設一個位置放抓起來的東西
    private GameObject nearbyObject; // 儲存碰到的物件
    private GameObject grabbedObject; // 抓著的物件

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sprite"))
        {
            nearbyObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == nearbyObject)
        {
            nearbyObject = null;
        }
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (grabbedObject == null && nearbyObject != null)
            {
                // 抓起物件
                grabbedObject = nearbyObject;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }
            else if (grabbedObject != null)
            {
                // 放下物件
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                grabbedObject.transform.SetParent(null);
                grabbedObject = null;
            }
        }
    }

    /*
    [SerializeField]
    private Transform grabPoint;

    [SerializeField]
    private Transform rayPoint;
    [SerializeField]
    private float rayDistance;

    private GameObject grabbedObject;
    private int layerIndex;

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Sprite");
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);
        
        if (hitInfo.collider!=null &&hitInfo.collider.gameObject.layer == layerIndex)
        {
            Debug.Log("命中了：" + hitInfo.collider.name);
            //grab object
            if(Keyboard.current.spaceKey.wasPressedThisFrame && grabbedObject == null)
            {
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }
            //release object
            else if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                grabbedObject.transform.SetParent(null);
                grabbedObject = null;
            }
        }
        else
        {
            Debug.Log("沒命中，可能距離太遠或方向不對");
        }

        Debug.DrawRay(rayPoint.position, transform.right * rayDistance);
    }
    */
}
