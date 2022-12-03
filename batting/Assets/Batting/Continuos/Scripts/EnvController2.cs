using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EnvController2 : Agent
{
    public GameObject ball;

    public float ballSpeedX = 1000f;
    public float ballSpeedY = 130f;
    public float ballThrowDelay = 1.0f;
    public float judgeDelay = 5f;

    public Batter2 batter;

    Rigidbody ballRb;
    Vector3 ballPos_org;

    const float Stay = 0.0f;
    const float Forward = 1.0f;
    const float Backward = -1.0f;

    public override void Initialize()
    {
        ballRb = ball.GetComponent<Rigidbody>();
        ballPos_org = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z);
        ball.GetComponent<BallController2>().RegisterEnv(this);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(ball.transform.position.x);
        sensor.AddObservation(ball.transform.position.y);
        sensor.AddObservation(ball.transform.position.z);
        sensor.AddObservation(ballRb.velocity.x);
        sensor.AddObservation(ballRb.velocity.y);
        sensor.AddObservation(ballRb.velocity.z);

        sensor.AddObservation(batter.jointY_1.localRotation.y);
        sensor.AddObservation(batter.jointY_3.localRotation.y);
        sensor.AddObservation(batter.jointY_2.localRotation.y);
        sensor.AddObservation(batter.jointZ_1.localRotation.z);
        sensor.AddObservation(batter.jointZ_3.localRotation.z);
        sensor.AddObservation(batter.jointZ_2.localRotation.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var vectorActions = actionBuffers.ContinuousActions;

        // manipulate batter
        for (int i = 0; i < vectorActions.Length; ++i)
        {
            batter.Manipulate(i, vectorActions[i]);
        }

        // Fail to hit
        if (ball.transform.position.x < -1.0f)
        {
            AddReward(-1.0f);
            EndEpisode();
        }
        // Homerun 
        if (ball.transform.position.magnitude > 50f)
        {
            AddReward(1.0f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        // Joint_Y_1
        if (Input.GetKey(KeyCode.Alpha1))
            continuousActionsOut[0] = Forward;
        if (Input.GetKey(KeyCode.Q))
            continuousActionsOut[0] = Backward;

        // Joint_Y_2
        if (Input.GetKey(KeyCode.Alpha2))
            continuousActionsOut[1] = Forward;
        if (Input.GetKey(KeyCode.W))
            continuousActionsOut[1] = Backward;

        // Joint_Y_1
        if (Input.GetKey(KeyCode.Alpha3))
            continuousActionsOut[2] = Forward;
        if (Input.GetKey(KeyCode.E))
            continuousActionsOut[2] = Backward;

        // Joint_Z_1
        if (Input.GetKey(KeyCode.Alpha4))
            continuousActionsOut[3] = Forward;
        if (Input.GetKey(KeyCode.R))
            continuousActionsOut[3] = Backward;

        // Joint_Z_1
        if (Input.GetKey(KeyCode.Alpha5))
            continuousActionsOut[4] = Forward;
        if (Input.GetKey(KeyCode.T))
            continuousActionsOut[4] = Backward;

        // Joint_Z_3
        if (Input.GetKey(KeyCode.Alpha6))
            continuousActionsOut[5] = Forward;
        if (Input.GetKey(KeyCode.Y))
            continuousActionsOut[5] = Backward;
    }

    public override void OnEpisodeBegin()
    {
        ResetScene();

        Invoke("Throw", ballThrowDelay);
    }

    public void ResetScene()
    {
        ResetBall();
        ResetBatter();

        CancelInvoke();
    }

    void ResetBall()
    {
        ball.GetComponent<MeshRenderer>().material.color = Color.cyan;

        ball.transform.position = ballPos_org;
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
        ballRb.useGravity = false;
    }

    void ResetBatter()
    {
        batter.ResetJoint();
    }

    void Throw()
    {
        ball.GetComponent<MeshRenderer>().material.color = Color.yellow + Color.red;

        ballRb.useGravity = true;

        Vector3 velocity = new Vector3(-ballSpeedX, ballSpeedY, 0);
        ballRb.AddForce(velocity);
    }

    public void SuccessToHit()
    {
        Invoke("SuccessToHitDelayed", judgeDelay);
    }

    void SuccessToHitDelayed()
    {
        float point = ball.transform.position.magnitude / 100f;
        AddReward(point);        
        EndEpisode();
    }
}
