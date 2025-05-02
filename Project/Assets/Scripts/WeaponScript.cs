using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float damage=0.25f;
    [SerializeField] Transform wielder;

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
        if (c.transform != wielder.transform && c.transform.TryGetComponent(out Execution e))
        {
            e.anim.SetBool("IsDying", true);
        }
    }
}
