using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMsg : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject msg;

    // Start is called before the first frame update
    void Start()
    {
        msg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        msg.SetActive(false);    
    }

    public void ShowMSG()
    {
        Debug.Log("Showing Message");
        msg.SetActive(true);
    }

    public void Interact(GameObject b) { }
}
