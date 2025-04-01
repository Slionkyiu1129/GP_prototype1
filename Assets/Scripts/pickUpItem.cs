using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpItem : MonoBehaviour
{
    private bool isHeld = false;  // �P�_���~�O�_�Q����
    private GameObject player;    // ���a����
    private Camera mainCamera;    // �D�۾�
    private GameObject currentItem;  // ��e���������~

    void Start()
    {
        mainCamera = Camera.main; // �T�O�ϥΥD�۾�
    }

    void Update()
    {
        // �p�G���a���U P ��B�I���쪫�~
        if (Input.GetKeyDown(KeyCode.P) && !isHeld)
        {
            TryPickupItem();
        }

        // �p�G���a���U O ����I����Ŧa
        if (Input.GetKeyDown(KeyCode.O) && isHeld)
        {
            TryDropItem();
        }
    }

    // ���վ߰_���~
    void TryPickupItem()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); // �ήg�u�˴��I����m
        if (hit.collider != null && hit.collider.CompareTag("PickupItem")) // �T�O�I�����O�i�߰_�����~
        {
            // �߰_���~
            currentItem = hit.collider.gameObject;
            currentItem.SetActive(false); // ���ê��~�]�]�i�H�O������ܪ��A�^
            isHeld = true;
        }
    }

    // ���թ�U���~
    void TryDropItem()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); // �ήg�u�˴���m��m
        if (hit.collider == null) // �p�G�I�����O�Ŧa�]�ΨS���I������^
        {
            // ��U���~
            currentItem.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition); // �N���~��m���I����m
            currentItem.SetActive(true); // ��ܪ��~
            isHeld = false;
        }
    }
}
