using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenAnimOver : MonoBehaviour
{
    // If we need to add delay to the destruction
    [SerializeField] private float delay = 0f;

    void Start()
    {
        // Explosion GO destroyed when animation is over
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
