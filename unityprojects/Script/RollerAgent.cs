using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MLAgents;

public class RollerAgent : Agent {

    Rigidbody rBody;
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public Transform Target;
    public override void AgentReset()
    {
        if (this.transform.position.y < 0)
        {
            //回転加速度と加速度のリセット
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            //エージェントを初期位置に戻す
            this.transform.position = new Vector3(0, 0.5f, 0);
        }
        //ターゲット再配置
        Target.position = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

    public override void CollectObservations()
    {
        // ターゲットとエージェントの位置
        AddVectorObs(Target.position);
        AddVectorObs(this.transform.position);

        //エージェントの速度
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.z);
    }

    public float speed = 10;
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //行動
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        rBody.AddForce(controlSignal * speed);


        //報酬
        //ボール（エージェント）が動いた距離から箱（ターゲット）への距離を取得
        float distanceToTarget = Vector3.Distance(this.transform.position, Target.position);

        //箱（ターゲット）に到達した場合
        if (distanceToTarget < 1.42f)
        {
            //報酬を与え完了
            SetReward(1.0f);
            Done();
        }

        //床から落ちた場合
        if (this.transform.position.y < 0)
        {
            Done();
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }

}
