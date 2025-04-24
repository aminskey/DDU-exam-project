using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float damage=0.25f;
    [SerializeField] Transform wielder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.transform != wielder.transform && c.transform.TryGetComponent(out PlayerVariables p))
        {
            if (!p.usingShield)
            {
                Debug.Log("Hit fighter!");
                p.health -= damage;
            }
        }
    }
}
