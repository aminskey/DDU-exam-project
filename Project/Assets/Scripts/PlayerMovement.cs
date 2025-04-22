using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float sensX, sensY;
    float xRot, yRot;
    public float speed = 10f;
    Rigidbody rb;

    [SerializeField] Transform center;
    [SerializeField] float radius = 78f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        float horiz = Input.GetAxis("Horizontal"), vert = Input.GetAxis("Vertical");
        Vector3 move = (transform.forward * vert + transform.right * horiz) * speed;
        Vector3 toCenter = transform.position - center.position;
        float yVel = rb.velocity.y;

        if (Input.GetButton("Jump") && isGrounded()) {
            yVel = 5f;
        }

        if (toCenter.magnitude > radius) { 
            Vector3 clampedPos = center.position + toCenter.normalized * radius;
            rb.MovePosition(clampedPos);
        }

        yRot += mouseX;
        xRot -= mouseY;

        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        rb.velocity = new Vector3(move.x, yVel, move.z);
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.2f, mask);
    }
}
