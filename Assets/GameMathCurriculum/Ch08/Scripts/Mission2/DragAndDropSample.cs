using UnityEngine;

public class DragAndDropSample : MonoBehaviour
{
    public Camera camera;
    public LayerMask ground;
    public LayerMask target;
    public LayerMask dropZone;
    public GameObject dragingObject;
    private bool isDraging = false;

    private void Update()
    {
        Ray ray  = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, target))
            {
                Debug.Log("Drag Start");
                isDraging = true;
                dragingObject = hitInfo.collider.gameObject;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDraging)
            {
                if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, dropZone))
                {
                    dragingObject.transform.position = hitInfo.collider.transform.position;
                }
            }

            Debug.Log("Drag End");
            isDraging = false;
            dragingObject = null;
        }

        if (isDraging)
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, ground))
            {
                Debug.Log(hitInfo.point);
                dragingObject.transform.position = hitInfo.point;    //  ????
            }
        }
    }
}
