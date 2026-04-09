using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5;
    public float rotateSpeed = 360f;
    private float rotate;
    private float move;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        rotate = Input.GetAxisRaw("Horizontal");  //좌우 회전
        move = Input.GetAxisRaw("Vertical");  //앞뒤 이동
    }
    private void FixedUpdate()
    {
        float angle = rotate * rotateSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, angle, 0f));

        Vector3 delta = move * transform.forward * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + delta);
    }
}
