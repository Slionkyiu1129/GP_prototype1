using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class grabItem : MonoBehaviour
{
    public Transform grabPoint; // 設一個位置放抓起來的東西
    private GameObject nearbyObject; // 儲存碰到的物件
    private GameObject grabbedObject; // 抓著的物件
    public Transform[] snapPoints; // 在 Inspector 指派你設的放置點們
    public float snapRange = 0.5f; // 設定一個吸附範圍
    private Transform currentHighlightedPoint;
    public checkVaseCorrectManager checkVaseCorrectManager;

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
        // 檢查拿著物件時，是否有靠近吸附點
        if (grabbedObject != null)
        {
            HighlightNearestSnapPoint(grabbedObject.transform.position);
        }
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
                SnapToNearestPoint(grabbedObject);
                grabbedObject = null;
                // 放下時清除 highlight
                if (currentHighlightedPoint != null)
                {
                    currentHighlightedPoint.GetComponent<snapPointFeedback>().Highlight(false);
                    currentHighlightedPoint = null;
                }
                checkVaseCorrectManager.CheckAllPots();
            }
        }
    }
    private void HighlightNearestSnapPoint(Vector2 position)
    {
        float minDistance = Mathf.Infinity;
        Transform nearest = null;

        foreach (Transform point in snapPoints)
        {
            float distance = Vector2.Distance(position, point.position);
            if (distance < snapRange && distance < minDistance)
            {
                minDistance = distance;
                nearest = point;
            }
        }

        if (nearest != currentHighlightedPoint)
        {
            if (currentHighlightedPoint != null)
                currentHighlightedPoint.GetComponent<snapPointFeedback>().Highlight(false);
            if (nearest != null)
                nearest.GetComponent<snapPointFeedback>().Highlight(true);
            currentHighlightedPoint = nearest;
        }
    }
    private void SnapToNearestPoint(GameObject obj)
    {
        float minDistance = Mathf.Infinity;
        Transform closestPoint = null;

        foreach (Transform point in snapPoints)
        {
            float distance = Vector2.Distance(obj.transform.position, point.position);
            if (distance < snapRange && distance < minDistance)
            {
                minDistance = distance;
                closestPoint = point;
            }
        }
    
        if (closestPoint != null)
        {
            obj.transform.position = closestPoint.position;
        }
    }
}
