using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 3f;

    private float dampSmoothTime = 0.3f;
    private Vector3 offset = new Vector3(0f, 5f, -10f);
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        //카메라 이동
        Vector3 golPos = target.TransformPoint(offset);
        transform.position = Vector3.SmoothDamp(
            transform.position,
            golPos,
            ref velocity,
            dampSmoothTime);

        //카메라 회전
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
       
    }

}
