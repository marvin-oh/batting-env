using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public GameObject target;

    Text textUI;

    void Start()
    {
        textUI = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textUI.text = target.transform.position.ToString();
    }
}
