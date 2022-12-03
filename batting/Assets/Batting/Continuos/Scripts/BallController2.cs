using UnityEngine;

public class BallController2 : MonoBehaviour
{
    EnvController2 envController;

    public void RegisterEnv(EnvController2 _envController)
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
