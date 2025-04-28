using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerVariables : MonoBehaviour
{
    public float stamina = 1f, health=1f;
    public bool usingShield = false;
    [SerializeField] Slider staminaBar, healthBar;


    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        staminaBar.value = stamina;
        healthBar.value = health;
    }
}
