using UnityEngine;
using UnityEngine.UI;

public class HideButtonUI : MonoBehaviour
{
    public  RectTransform panel;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(HideBtn);
    }

    public void HideBtn()
    {
        float angle = transform.eulerAngles.z;
        if (angle == 0f)
        {
            panel.position += new Vector3(0, -(panel.rect.height + 10), 0);
        }
        else if (angle == 270f)
        {
            panel.position += new Vector3(-(panel.rect.width + 10), 0, 0);
        }
        else if (angle == 180f)
        {
            panel.position += new Vector3(0, (panel.rect.height + 10), 0);
        }
        else if (angle == 90f)
        {
            panel.position += new Vector3((panel.rect.width + 10), 0, 0);
        }
        transform.rotation = Quaternion.Euler(0, 0, angle - 180);
    }
}
