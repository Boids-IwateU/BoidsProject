using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class Player : MonoBehaviour
{
    //Animatorを入れる変数
    private Animator animator;

    //ユニティちゃんの位置を入れる
    Vector3 playerPos;

    public float speed = 15.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    float inputHorizontal;
    float inputVertical;

    [SerializeField] float smooth = 10f;

    void Start()

    {

    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");


        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {

        }
        else if (Input.GetKey(KeyCode.A))
        {

        }
        else if (Input.GetKey(KeyCode.S))
        {

        }
        else if (Input.GetKey(KeyCode.D))
        {

        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

}

