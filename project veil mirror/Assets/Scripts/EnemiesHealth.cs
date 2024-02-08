using Mirror;
using UnityEngine;

public class EnemiesHealth : NetworkBehaviour
{
    [SyncVar]
    public int enemyHealth = 100; // Initial health

    int enemyCurrentHealth;
    Animator anim;
    VengefulShoot vengefulShoot;

    [SyncVar]
    bool hasAddedScore = false; // Flag to track if score has been added

    void Start()
    {
        anim = GetComponent<Animator>();
        enemyCurrentHealth = enemyHealth;
        vengefulShoot = GetComponent<VengefulShoot>();
    }

    void Update()
    {
        if (!isServer)
            return;

        if (enemyCurrentHealth <= 0 && !hasAddedScore)
        {
            // Death logic here
            anim.Play("Vengeful Death");
            vengefulShoot.enabled = false;
            Destroy(gameObject, 1f);
            AddScore(50); // You can pass score increment as an argument
            hasAddedScore = true; // Set the flag to true after adding score
        }
    }

    public void enemyTakeDamage(int damage)
    {
        if (!isServer)
            return;

        anim.Play("Vengeful Hurt");
        enemyCurrentHealth -= damage;
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