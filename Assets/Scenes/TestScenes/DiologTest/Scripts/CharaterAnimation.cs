using UnityEngine;

public class CharaterAnimation : MonoBehaviour
{
    private CharacterController2D_addGrid characterController;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // 取得 CharacterController2D_addGrid 元件
        characterController = GameObject.FindObjectOfType<CharacterController2D_addGrid>();
    }

    void Update()
    {
        // 取得移動方向
        Vector2 moveDirection = characterController.MoveDirection;

        if (moveDirection != Vector2.zero)
        {
            animator.SetFloat("MoveX", moveDirection.x);
            animator.SetFloat("MoveY", moveDirection.y);
            animator.SetFloat("Speed", 1f); // 你可以根據 isMoving 調整
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }

        animator.SetBool("isPushing", characterController.IsPushing);
    }

    void LateUpdate()
    {
        if (characterController.IsPushing)
        {
            Debug.Log("正在推！");
        }
    }
    // private void FixedUpdate()
    // {
    //     HandleHorizontalMovement();
    // }

    // private void HandleHorizontalMovement()
    // {

    // }
}
