using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PossessiveHealth : NetworkBehaviour
{
    [Header("Health")]
    [SyncVar]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;
    [SerializeField]private EnemyPatrol enemypatrol;

    [SyncVar]
    bool hasAddedScore = false; // Flag to track if score has been added

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        enemypatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        if (!isServer)
            return;
    }

    public void TakeDamage(float _damage)
    {
        if (!isServer)
            return;

        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                dead = true;
                AddScore(25);
                enemypatrol.DestroyGameObject();
                Destroy(gameObject,2f);
            }
        }
    }
   
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    void AddScore(int scoreIncrement)
    {
        ScoreCounter scoreCounter = FindObjectOfType<ScoreCounter>();
        if (scoreCounter != null)
        {
            scoreCounter.scoreValue += scoreIncrement;
            Debug.Log("Added " + scoreIncrement + " points!");
        }
    }
}
