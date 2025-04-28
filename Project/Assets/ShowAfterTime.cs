using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAfterTime : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] float time;

    // Start is called before the first frame update
    void Start()
    {
        obj.SetActive(false);
        Invoke("Show", time);
    }

    void Show() { obj.SetActive(true); }
}
