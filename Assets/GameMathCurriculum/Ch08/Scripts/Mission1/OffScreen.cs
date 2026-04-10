using UnityEngine;
using UnityEngine.UI;

public class OffScreen : MonoBehaviour
{
    public Transform[] targets;
    public RectTransform[] indicators;
    public Camera cam;

    private Image image;


    private void Start()
    {
        cam = Camera.main;
        //GetComponent<Image>;
    }
    private void Update()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            UpdateIndicator(targets[i], indicators[i]);
        }
    }

    void UpdateIndicator(Transform target, RectTransform indicator)
    {
        Vector3 screenPos = cam.WorldToScreenPoint(target.position);
        bool isBehind = screenPos.z < 0;

        if (isBehind)
        {
            //screenPos *= -1;
            float flippedX = Screen.width - screenPos.x;
            float flippedY = Screen.height - screenPos.y;
            screenPos = new Vector3(flippedX, flippedY, 0f);
        }

        bool isOffScreen = isBehind || screenPos.x < 0 || screenPos.x > Screen.width ||
                                       screenPos.y < 0 || screenPos.y > Screen.height;

        if (isOffScreen)
        {
            Vector3 newIndicatorPos = new Vector3(Mathf.Clamp(screenPos.x, 0, Screen.width),
                                             Mathf.Clamp(screenPos.y, 0, Screen.height), 0f);
            indicator.position = newIndicatorPos;
            indicator.gameObject.SetActive(true);
        }
        else
        {
            indicator.gameObject.SetActive(false);
        }

        
    }
}
