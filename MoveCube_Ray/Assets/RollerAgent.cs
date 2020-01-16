using UnityEngine;
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
        //回転加速度と加速度のリセット
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        //エージェントを初期位置に戻す
        this.transform.position = new Vector3( 0, 0.5f, 0);
        this.transform.rotation = Quaternion.identity;
        //ターゲット再配置
        Target.position = new Vector3(Random.Range(-15f, 15f), 0.5f, Random.Range(-15f, 15f));
    }

    public override void CollectObservations()
    {
        const float rayDistance = 35f;
        float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
        float[] rayAngles1 = { 25f, 95f, 165f, 50f, 140f, 75f, 115f };
        float[] rayAngles2 = { 15f, 85f, 155f, 40f, 130f, 65f, 105f };

        string[] detectableObjects = { "target", "wall"};
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles1, detectableObjects, 0f, 0f));
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles2, detectableObjects, 0f, 0f));
        /*
        // ターゲットとエージェントの位置
        AddVectorObs(Target.position);
        AddVectorObs(this.transform.position);

        //エージェントの速度
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.z);
        */
    }
    
    public float rotateSpeed;
    public float speed;
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //行動
        Vector3 controlSignal = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;
        rotateDir = transform.up * vectorAction[0];
        controlSignal = transform.forward * Mathf.Abs(vectorAction[1]);
        transform.Rotate(rotateDir * rotateSpeed);
        rBody.AddForce(controlSignal * speed);

        AddReward(-1f / agentParameters.maxStep);

        //報酬
        //ボール（エージェント）が動いた距離から箱（ターゲット）への距離を取得
        float distanceToTarget = Vector3.Distance(this.transform.position, 
                                                 Target.position);

        //箱（ターゲット）に到達した場合
        if (distanceToTarget < 1.42f)
        {
            //報酬を与え完了
            SetReward(2.0f);
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