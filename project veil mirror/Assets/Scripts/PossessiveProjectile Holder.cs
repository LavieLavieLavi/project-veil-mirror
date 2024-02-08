using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessiveProjectileHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;
 

    // Update is called once per frame
    void Update()
    {
        if (enemy == null) return;
       transform.localScale= enemy.localScale;
    }
}
