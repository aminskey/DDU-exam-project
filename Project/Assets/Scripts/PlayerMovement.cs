using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody rb;

    [SerializeField] Transform center;
    [SerializeField] float radius = 78f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask mask;

    Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal"), vert = Input.GetAxis("Vertical");
        Vector3 move = (transform.forward * vert + transform.right * horiz) * speed;
        Vector3 toCenter = transform.position - center.position;
        float yVel = rb.velocity.y;

        if (Input.GetButton("Jump") && isGrounded()) {
            yVel = 5f;
        }

        if (Input.GetMouseButtonDown(0) && !anim.GetBool("AttackTrue")) {
            anim.SetBool("AttackTrue", true);
        }

        if (toCenter.magnitude > radius) { 
            Vector3 clampedPos = center.position + toCenter.normalized * radius;
            rb.MovePosition(clampedPos);
        }

        rb.velocity = new Vector3(move.x, yVel, move.z);
    }
    
    public void setFalse(string a) {
        anim.SetBool(a, false);
    }
    
    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.2f, mask);
    }
}
