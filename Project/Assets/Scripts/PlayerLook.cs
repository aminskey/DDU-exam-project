using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact(GameObject player);
    public void ShowMSG();
}


public class PlayerLook : MonoBehaviour
{
    [SerializeField] float sensX, sensY;
    [SerializeField] Transform parentBody;
    float xRot=0f, yRot=0f;
    [SerializeField] float lookRange=10f;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        
        yRot += mouseX;
        xRot -= mouseY;

        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        parentBody.rotation = Quaternion.Euler(0f, yRot, 0f);

        Ray r = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(r, out RaycastHit hit, lookRange))
        {
            Debug.Log("Ray cast hit");
            if (hit.collider.gameObject.TryGetComponent(out IInteractable obj)) {
                Debug.Log("Interactable found");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    obj.Interact(this.gameObject);
                }
                else
                {
                    obj.ShowMSG();
                }
            }
        }
        

    }

    bool isInRange(float x, float off, float min, float max)
    {
        return (x + off) < max && (x - off) > min;
    }
}
