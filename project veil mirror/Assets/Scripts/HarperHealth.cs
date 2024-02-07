using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarperHealth : MonoBehaviour
{
    public int harperHealth;
    private int harperCurrentHealth;

    private Animator anim;


    private MovementPrototyping move;

    // Start is called before the first frame update
    void Start()
    {
        harperCurrentHealth = harperHealth;
        anim = GetComponent<Animator>();
        move = GetComponent<MovementPrototyping>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (harperCurrentHealth <= 0)
        {
            anim.Play("Harper Death");
          
            move.enabled = false;
            Destroy(gameObject, 2f);
        }
    }
   
    
    //
    public void harperHurt(int Damage)
    {
        //move.SetHurt();
        Debug.Log("Harper Took damage");
        harperCurrentHealth -= Damage;
    }

  
}
