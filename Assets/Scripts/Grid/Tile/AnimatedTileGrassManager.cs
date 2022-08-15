using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTileGrassManager : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float randomDelay = 0;


    private IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(0, randomDelay));
        animator.SetInteger("Anim", Random.Range(1, 4));
    }
}
