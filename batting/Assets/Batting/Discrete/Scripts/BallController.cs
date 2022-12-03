using UnityEngine;

public class BallController : MonoBehaviour
{
    EnvController envController;

    public void RegisterEnv(EnvController _envController)
    {
        envController = _envController;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.Equals(envController.batter.bat))
        {
            envController.SuccessToHit();
        }
    }
}
