using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void setFalse(string a)
    {
        anim.SetBool(a, false);
    }
    public void Quit()
    {
        Invoke("Quit1", 0.125f);
    }

    void Quit1()
    {
        Application.Quit();
    }
}
