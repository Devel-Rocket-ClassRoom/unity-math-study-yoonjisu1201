using UnityEngine;
using TMPro;
using System;
public class UIPractice : MonoBehaviour
{
    private Transform target;
    private Vector3 offset = new Vector3(0f, 5f, -10f);
    private float positionSmmothTime = 0.3f;
    private float rotationSmoothSpeed = 5f;

    private float targetZoomDistance;
    private float zoomSmoothTime = 0.2f;

    private float currentZoomDistance;
    private float minZoomDistance = 3f;
    private float maxZoomDistance = 15f;
    private float zoomSpeed = 3f;
    private float zoomVeolcity;
    private Vector3 positionVelocity = Vector3.zero;

    private void LateUpdate()
    {
        targetZoomDistance = currentZoomDistance;
        if (target == null)
        {
            return;
        }
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");

        if (scroll != 0)
        {
            targetZoomDistance += scroll * zoomSpeed;
            targetZoomDistance = Mathf.Clamp(targetZoomDistance, minZoomDistance, maxZoomDistance);
        }
        currentZoomDistance = Mathf.SmoothDamp(
            currentZoomDistance,
            targetZoomDistance,
            ref zoomVeolcity,
            zoomSmoothTime);


        Vector3 cameraPos = target.position + offset;
        Vector3 targetPos = target.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(cameraPos), rotationSmoothSpeed * Time.deltaTime);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            cameraPos,
            ref positionVelocity,
            positionSmmothTime);
    }
}
