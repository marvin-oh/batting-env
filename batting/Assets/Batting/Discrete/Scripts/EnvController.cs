using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EnvController : Agent
{
    public GameObject ball;

    public float ballSpeedX = 1000f;
    public float ballSpeedY = 130f;
    public float ballThrowDelay = 1.0f;
    public float judgeDelay = 5f;

    public Batter batter;

    Rigidbody ballRb;
    Vector3 ballPos_org;

    const int Stay = 0;
    const int Forward = 1;
    const int Backward = 2;

    public override void Initialize()
    {
        ballRb = ball.GetComponent<Rigidbody>();
        ballPos_org = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z);
        ball.GetComponent<BallController>().RegisterEnv(this);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(ball.transform.localPosition.x);
        sensor.AddObservation(ball.transform.localPosition.y);
        sensor.AddObservation(ball.transform.localPosition.z);
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
        var vectorActions = actionBuffers.DiscreteActions;

        // manipulate batter
        for (int i = 0; i < vectorActions.Length; ++i)
        {
            if (vectorActions[i] == Forward)
                batter.Manipulate(i, 1.0f);
            if (vectorActions[i] == Backward)
                batter.Manipulate(i, -1.0f);
        }

        // Fail to hit
        if (ball.transform.localPosition.x < -1.0f)
        {
            AddReward(-1.0f);
            EndEpisode();
        }
        // Homerun 
        if (ball.transform.localPosition.magnitude > 50f)
        {
            AddReward(1.0f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;

        // Joint_Y_1
        if (Input.GetKey(KeyCode.Alpha1))
            discreteActionsOut[0] = Forward;
        if (Input.GetKey(KeyCode.Q))
            discreteActionsOut[0] = Backward;

        // Joint_Y_2
        if (Input.GetKey(KeyCode.Alpha2))
            discreteActionsOut[1] = Forward;
        if (Input.GetKey(KeyCode.W))
            discreteActionsOut[1] = Backward;

        // Joint_Y_1
        if (Input.GetKey(KeyCode.Alpha3))
            discreteActionsOut[2] = Forward;
        if (Input.GetKey(KeyCode.E))
            discreteActionsOut[2] = Backward;

        // Joint_Z_1
        if (Input.GetKey(KeyCode.Alpha4))
            discreteActionsOut[3] = Forward;
        if (Input.GetKey(KeyCode.R))
            discreteActionsOut[3] = Backward;

        // Joint_Z_1
        if (Input.GetKey(KeyCode.Alpha5))
            discreteActionsOut[4] = Forward;
        if (Input.GetKey(KeyCode.T))
            discreteActionsOut[4] = Backward;

        // Joint_Z_3
        if (Input.GetKey(KeyCode.Alpha6))
            discreteActionsOut[5] = Forward;
        if (Input.GetKey(KeyCode.Y))
            discreteActionsOut[5] = Backward;
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
        // Out of the foul line
        float degree = Mathf.Atan2(ball.transform.localPosition.x, ball.transform.localPosition.z) * Mathf.Rad2Deg;
        if (degree <= 45 || degree >= 135)
        {
            AddReward(-1.0f);
            EndEpisode();
            return;
        }

        float point = ball.transform.localPosition.magnitude / 50f;
        AddReward(point);        
        EndEpisode();
    }
}
