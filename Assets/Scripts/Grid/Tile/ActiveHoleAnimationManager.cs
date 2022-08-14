using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveHoleAnimationManager : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private TileSafety safety;

    private void Awake()
    {
        safety.OnSafetyChanged += (safe) =>
        {
            animator.SetBool("Active", !safe);
        };
    }

}
