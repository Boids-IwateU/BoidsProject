using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]

public class CameraRotate : MonoBehaviour
{

    public Transform Target;
    public float DistanceToPlayerM = 2f;    // カメラとプレイヤーとの距離[m]
    public float SlideDistanceM = 0f;       // カメラを横にスライドさせる；プラスの時右へ，マイナスの時左へ[m]
    public float HeightM = 1.2f;            // 注視点の高さ[m]
    public float RotationSensitivity = 100f;// 感度

    void Start()
    {
        if (Target == null)
        {
            Debug.LogError("ターゲットが設定されていない");
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        var rotX = Input.GetAxis("Mouse X") * Time.deltaTime * RotationSensitivity;
        var rotY = Input.GetAxis("Mouse Y") * Time.deltaTime * RotationSensitivity;

        var lookAt = Target.position + Vector3.up * HeightM;

        // 回転
        transform.RotateAround(lookAt, Vector3.up, rotX);
        // カメラがプレイヤーの真上や真下にあるときにそれ以上回転させないようにする
        if (transform.forward.y > 0.5f && rotY < 0)
        {
            rotY = 0;
        }

        if (transform.forward.y < -0.7f && rotY > 0)
        {
            rotY = 0;
        }

        transform.RotateAround(lookAt, transform.right, rotY);

        // カメラとプレイヤーとの間の距離を調整
        transform.position = lookAt - transform.forward * DistanceToPlayerM;

        // 注視点の設定
        transform.LookAt(lookAt);

        // カメラを横にずらして中央を開ける
        transform.position = transform.position + transform.right * SlideDistanceM;
    }
}


/* 1/6時点
using UnityEngine;
public class CameraRotate : MonoBehaviour
{
    //回転させるスピード
    public float rotate_speed = 1.0f;
    public GameObject player;
    public GameObject mainCamera;
    private const int ROTATE_BUTTON = 1;
    private const float ANGLE_LIMIT_UP = 60f;
    private const float ANGLE_LIMIT_DOWN = -60f;
    void Start()
    {
        mainCamera = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        transform.position = player.transform.position;
        if (Input.GetMouseButton(ROTATE_BUTTON))
        {
            rotateCameraAngle();
        }
        float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        transform.eulerAngles = new Vector3(
            Mathf.Clamp(angle_x, ANGLE_LIMIT_DOWN, ANGLE_LIMIT_UP),
            transform.eulerAngles.y,
            transform.eulerAngles.z
        );
    }
    private void rotateCameraAngle()
    {
        Vector3 angle = new Vector3(
            Input.GetAxis("Mouse X") * rotate_speed,
            Input.GetAxis("Mouse Y") * rotate_speed,
            0
        );
        transform.eulerAngles += new Vector3(angle.y, angle.x);
    }
}
*/

/* 12/16時点のカメラ
  //プレイヤーを変数に格納
    public GameObject Player;
    //回転させるスピード
    public float rotateSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
    }
    void Update()
    {
        //回転角度
        float angleH = Input.GetAxis("Mouse X");
        float angleV = Input.GetAxis("Mouse Y");
        if(angleV < 0.5)
        {
            angleV = Input.GetAxis("Mouse X");
        }
        else
        {
            angleV = -0.5f;
        }
        //プレイヤーの位置情報
        Vector3 playerPos = Player.transform.position;
        //カメラの角度制限
        //カメラを回転させる
        transform.RotateAround(playerPos, Vector3.up, angleH * rotateSpeed);
        transform.RotateAround(playerPos, Vector3.right, angleV * rotateSpeed);
    }
   
*/
