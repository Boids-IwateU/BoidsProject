﻿using UnityEngine;
using MLAgents;

public class RollerAgent:Agent
{
    Rigidbody rBody;
    RayPerception rayPer;
    void Start(){
        rBody = GetComponent<Rigidbody>();
        rayPer = GetComponent<RayPerception>();
    }

    public Transform Target;
    public override void AgentReset()
    {
        if (this.transform.position.y < 1.0   || this.transform.position.y > 21  ||
            this.transform.position.x > 21 || this.transform.position.x < -21 ||
            this.transform.position.z > 21 || this.transform.position.z < -21 )
        {
            //回転加速度と加速度のリセット
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            //エージェントを初期位置に戻す
            this.transform.position = new Vector3( 0, 5.0f, 0);
        }
        //ターゲット再配置
        Target.position = new Vector3(Random.Range(-20.0f,20.0f), Random.Range(1.0f,20.0f),Random.Range(-20.0f,20.0f));
    }


    ////////////////////

    public override void CollectObservations()
    {
        const float rayDistance = 50f;
        float[] rayAngles = { 90f, 45f, 135f, 70f, 110f };
        string[] detectableObjects = { "target" , "ground", "agent", "block"};
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 20f));
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 10f));
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, -10f));
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, -20f));
        /* ターゲットとエージェントの位置
        AddVectorObs(Target.position);
        AddVectorObs(this.transform.position);

        //エージェントの速度
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.y);
        AddVectorObs(rBody.velocity.z);*/
    }

    //rigidB.AddForce(Vector3.up *jspeed);
    //rigidB.velocity = Vector3.ClampMagnitude(rigidB.velocity, maxpush);

    public float speed = 1000;
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //行動
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x =  vectorAction[0];
        controlSignal.y =  vectorAction[1];
        controlSignal.z =  vectorAction[2];
        rBody.AddForce(controlSignal * speed);


        //報酬
        //ボール（エージェント）が動いた距離から箱（ターゲット）への距離を取得
        float distanceToTarget = Vector3.Distance(this.transform.position,Target.position);

        //箱（ターゲット）に到達した場合
        if (distanceToTarget < 1.42f)
        {
            //報酬を与え完了
            SetReward(1.0f);
            Done();
        }


        //床から落ちた場合
        if (this.transform.position.y < 1.0   || this.transform.position.y > 21  ||
            this.transform.position.x > 21 || this.transform.position.x < -21 ||
            this.transform.position.z > 21 || this.transform.position.z < -21 )
        {
            Done();
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[3];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Jump");
        action[2] = Input.GetAxis("Vertical");
        return action;
    }

}

    
