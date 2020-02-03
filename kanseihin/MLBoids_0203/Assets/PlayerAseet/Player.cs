using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class Player : MonoBehaviour
{
  //Animatorを入れる変数
  private Animator animator;

  //ユニティちゃんの位置を入れる
  Vector3 playerPos;

  public Camera MainCamera;
  public float RotationSensitivity = 100f;
  public float speed = 15.0F;
  public float jumpSpeed = 8.0F;
  public float gravity = 20.0F;
  private Vector3 moveDirection = Vector3.zero;

  float countW = 0;
  float countS = 0;
  float countA = 0;
  float countD = 0;

  float inputHorizontal;
  float inputVertical;

  //[SerializeField] float smooth = 10f;

  void Start()

  {
    countW = 1;
  }

  void Update()
  {
    var rotX = Input.GetAxis("Mouse X") * Time.deltaTime * RotationSensitivity;
    transform.Rotate(0, rotX, 0);
    CharacterController controller = GetComponent<CharacterController>();

    inputHorizontal = Input.GetAxisRaw("Horizontal");
    inputVertical = Input.GetAxisRaw("Vertical");

    // transformを取得
    Transform myTransform = this.transform;
    Transform cameraTrans = MainCamera.gameObject.transform;

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
      countW = 1;
      //countW++;
      //countA = 0;
      countS = 0;
      //countD = 0;
    }
    else if (Input.GetKey(KeyCode.A) /*&& countA == 0*/)
    {
      //myTransform.Rotate(0, -10f, 0);
      ////MainCamera.gameObject.transform.rotation = cameraTrans.rotation;
      //myTransform.Rotate(0, -90.0f, 0);
      //countA++;
      //countW = 0;
      //countD = 0;
      //countS = 0;
    }
    else if (Input.GetKey(KeyCode.S) /*&& countS == 0*/)
    {
      countS = 1;
      //myTransform.Rotate(0, 180.0f, 0);
      //countS++;
      countW = 0;
      //countA = 0;
      //countD = 0;
    }
    else if (Input.GetKey(KeyCode.D) && countD == 0)
    {
      //myTransform.Rotate(0, 10f, 0);
      /*
      myTransform.Rotate(0, 90.0f, 0);
      countD++;
      countW = 0;
      countA = 0;
      countS = 0;
      */
    }
    if (countW > 0)
    {
      moveDirection.y -= gravity * Time.deltaTime;
      controller.Move(moveDirection * Time.deltaTime);
    }
    else if (countS > 0)
    {
      moveDirection.y -= gravity * Time.deltaTime;
      controller.Move(moveDirection * Time.deltaTime);
    }
  }

}