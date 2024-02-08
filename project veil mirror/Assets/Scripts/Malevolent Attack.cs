using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalevolentAttack : MonoBehaviour
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

    //TODO:: Fix facing direction if i have time and fuk this shit im tired

    // Coroutine to handle attacking
    IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);

            anim.SetTrigger("attack");
            yield return new WaitForSeconds(projectileDelay);
            shootingmode();
            yield return new WaitForSeconds(GetAnimationLength("Malevolent Attack") - projectileDelay);
            anim.Play("Malevolent Idle");
        }
    }

    void shootingmode()
    {
        // Instantiate and shoot the projectile
        Instantiate(vengefulProjectile, projectilePos.position, Quaternion.identity);
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

