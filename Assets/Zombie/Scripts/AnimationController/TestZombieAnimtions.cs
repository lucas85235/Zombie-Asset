using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestZombieAnimtions : MonoBehaviour
{
    private IZombieAnimations zombieAnimations;

    void Start()
    {
        zombieAnimations = GetComponent<IZombieAnimations>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            zombieAnimations.Idle();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            zombieAnimations.Walk();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            zombieAnimations.Walk(0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            zombieAnimations.Walk(1f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            zombieAnimations.Attack();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            zombieAnimations.HitReaction();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            zombieAnimations.Death();
        }
    }
}
