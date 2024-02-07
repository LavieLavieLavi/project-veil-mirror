using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Enemy Movement")]
    [SerializeField] private float enemySpeed;
    private Vector3 initialScale;
    private bool movingLeft;

    [Header("Idle Patrol behavior")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    //Components
    [SerializeField] private Animator anim;


    private void Awake()
    {
        initialScale = enemy.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MovetowardsDirection(-1);
            }
            else
            {
                //change dir
                DirectionChange();
            }
            
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MovetowardsDirection(1);
            }
            else
            {
                //change dir
                DirectionChange();
            }
                
        }
  
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);

        idleTimer += Time.deltaTime;

        if(idleTimer> idleDuration)
            movingLeft =  !movingLeft;

    }

    private void MovetowardsDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * _direction,
            initialScale.y, initialScale.z);


        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * enemySpeed, enemy.position.y, enemy.position.z);
    }

   
}
