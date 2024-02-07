using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VengefulShoot : MonoBehaviour
{
    public GameObject vengefulProjectile;
    public Transform projectilePos;

    public float attackInterval = 2f;
    public float projectileDelay = 0.5f; // Delay before spawning the projectile

    //AnimationStates
    Animator anim;
    const string VENGEFUL_IDLE = "Vengeful Idle";
    const string VENGEFUL_ATK = "Vengeful Attack";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(AttackRoutine());
    }

    // Coroutine to handle attacking
    IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);

            ChangeAnimationState(VENGEFUL_ATK);
            yield return new WaitForSeconds(projectileDelay);
            shootingmode();
            yield return new WaitForSeconds(GetAnimationLength(VENGEFUL_ATK) - projectileDelay);
            ChangeAnimationState(VENGEFUL_IDLE);
        }
    }

    void shootingmode()
    {
        // Instantiate and shoot the projectile
        Instantiate(vengefulProjectile, projectilePos.position, Quaternion.identity);
    }

    void ChangeAnimationState(string newState)
    {
        anim.Play(newState);
    }

    float GetAnimationLength(string stateName)
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == stateName)
            {
                return clip.length;
            }
        }
        return 0f;
    }
}

