using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batter : MonoBehaviour
{
    public Transform jointY_1;
    public Transform jointY_2;
    public Transform jointY_3;

    public Transform jointZ_1;
    public Transform jointZ_2;
    public Transform jointZ_3;

    public float jointRotationSpeed = 1000f;

    public GameObject bat;

    float jointY_1_org;
    float jointY_2_org;
    float jointY_3_org;
    float jointZ_1_org;
    float jointZ_2_org;
    float jointZ_3_org;

    // Start is called before the first frame update
    void Start()
    {
        jointY_1_org = jointY_1.localRotation.eulerAngles.y;
        jointY_2_org = jointY_2.localRotation.eulerAngles.y;
        jointY_3_org = jointY_3.localRotation.eulerAngles.y;

        jointZ_1_org = jointZ_1.localRotation.eulerAngles.z;
        jointZ_2_org = jointZ_2.localRotation.eulerAngles.z;
        jointZ_3_org = jointZ_3.localRotation.eulerAngles.z;
    }

    public void Manipulate(int JointNumber, float power)
    {
        // Joint_Y_1
        if (JointNumber == 0)
        {
            Vector3 rotateDir = transform.up * power;
            jointY_1.Rotate(rotateDir, Time.deltaTime * jointRotationSpeed);

            float angle = jointY_1.localEulerAngles.y;
            if (power < 0 && 280f < angle && angle < 320f)
                angle = 320f;
            if (power > 0 && 280f < angle && angle < 320f)
                angle = 280f;

            jointY_1.localRotation = Quaternion.Euler(0, angle, 0);
        }

        // Joint_Y_2
        if (JointNumber == 1)
        {
            Vector3 rotateDir = transform.up * power;
            jointY_2.Rotate(rotateDir, Time.deltaTime * jointRotationSpeed);

            float angle = jointY_2.localEulerAngles.y;
            if (power < 0 && 150f < angle && angle < 210f)
                angle = 210f;
            if (power > 0 && 150f < angle && angle < 210f)
                angle = 150f;

            jointY_2.localRotation = Quaternion.Euler(0, angle, 0);
        }

        // Joint_Y_3
        if (JointNumber == 2)
        {
            Vector3 rotateDir = transform.up * power;
            jointY_3.Rotate(rotateDir, Time.deltaTime * jointRotationSpeed);

            float angle = jointY_3.localEulerAngles.y;
            if (power < 0 && 150f < angle && angle < 210f)
                angle = 210f;
            if (power > 0 && 150f < angle && angle < 210f)
                angle = 150f;

            jointY_3.localRotation = Quaternion.Euler(0, angle, 0);
        }

        // Joint_Z_1
        if (JointNumber == 3)
        {
            Vector3 rotateDir = transform.forward * power;
            jointZ_1.Rotate(rotateDir, Time.deltaTime * jointRotationSpeed);

            float angle = jointZ_1.localEulerAngles.z;
            if (power < 0 && 130f < angle && angle < 260f)
                angle = 260f;
            if (power > 0 && 130f < angle && angle < 260f)
                angle = 130f;

            jointZ_1.localRotation = Quaternion.Euler(0, 0, angle);
        }

        // Joint_Z_2
        if (JointNumber == 4)
        {
            Vector3 rotateDir = transform.forward * power;
            jointZ_2.Rotate(rotateDir, Time.deltaTime * jointRotationSpeed);

            float angle = jointZ_2.localEulerAngles.z;
            if (power < 0 && 150f < angle && angle < 250f)
                angle = 250f;
            if (power > 0 && 150f < angle && angle < 250f)
                angle = 150f;

            jointZ_2.localRotation = Quaternion.Euler(0, 0, angle);
        }

        // Joint_Z_3
        if (JointNumber == 5)
        {
            Vector3 rotateDir = transform.forward * power;
            jointZ_3.Rotate(rotateDir, Time.deltaTime * jointRotationSpeed);

            float angle = jointZ_3.localEulerAngles.z;
            if (power < 0 && 90f < angle && angle < 270f)
                angle = 270f;
            if (power > 0 && 90f < angle && angle < 270f)
                angle = 90f;

            jointZ_3.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void ResetJoint()
    {
        jointY_1.localRotation = Quaternion.Euler(0, jointY_1_org, 0);
        jointY_2.localRotation = Quaternion.Euler(0, jointY_2_org, 0);
        jointY_3.localRotation = Quaternion.Euler(0, jointY_3_org, 0);

        jointZ_1.localRotation = Quaternion.Euler(0, 0, jointZ_1_org);
        jointZ_2.localRotation = Quaternion.Euler(0, 0, jointZ_2_org);
        jointZ_3.localRotation = Quaternion.Euler(0, 0, jointZ_3_org);

    }
}
