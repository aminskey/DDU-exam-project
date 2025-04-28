using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody rb;

    [SerializeField] Transform center;
    [SerializeField] float radius = 78f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask mask;

    Animator anim;
    PlayerVariables vr;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        vr = GetComponent<PlayerVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal"), vert = Input.GetAxis("Vertical");
        Vector3 move = (transform.forward * vert + transform.right * horiz);
        Vector3 toCenter = transform.position - center.position;
        float yVel = rb.velocity.y;
        float currSpeed = speed;

        if (Input.GetButton("Jump") && isGrounded()) {
            yVel = 5f;
        }

        if (Input.GetMouseButtonDown(0) && !anim.GetBool("IsAttacking")) {
            anim.SetBool("IsAttacking", true);
        }
        if (Input.GetMouseButtonDown(1) && !anim.GetBool("IsDefending"))
        {
            anim.SetBool("IsDefending", true);
            vr.usingShield = true;
        } else if(Input.GetMouseButtonUp(1) && anim.GetBool("IsDefending")) {
            anim.SetBool("IsDefending", false);
            vr.usingShield = false;
        }

        if (toCenter.magnitude > radius) { 
            Vector3 clampedPos = center.position + toCenter.normalized * radius;
            rb.MovePosition(clampedPos);
        }
        if (Input.GetKey(KeyCode.LeftShift) && vr.stamina > 0f & move != Vector3.zero)
        {
            vr.stamina -= 0.005f;
            currSpeed = speed * 2f;
        }
        else if (vr.stamina < 1f){
            vr.stamina += 0.001f;
        }

        if(move.magnitude > 0f || yVel > 0f) rb.velocity = new Vector3(move.x * currSpeed, yVel, move.z * currSpeed);

        if (vr.health < 0f)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    
    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.2f, mask);
    }
}
