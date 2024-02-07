using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesHealth : MonoBehaviour
{
    public int enemyHealth;
    private int enemyCurrentHealth;
    private VengefulShoot vengefulShoot;
    //Components
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemyCurrentHealth = enemyHealth;
        vengefulShoot = GetComponent<VengefulShoot>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            //death logic here
            //play death animation
            anim.Play("Vengeful Death");
            vengefulShoot.enabled = false;  
            Destroy(gameObject, 1f);
        }
    }

    public void enemyTakeDamage(int Damage)
    {
        anim.Play("Vengeful Hurt");
        enemyCurrentHealth -= Damage;
    }


}
