using UnityEngine;
using Mirror;

public class HarperHealth : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHealthChanged))]
    public int harperHealth = 100; // Starting health

    private Animator anim;
    private MovementPrototyping move;

    void Start()
    {
        anim = GetComponent<Animator>();
        move = GetComponent<MovementPrototyping>();
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (harperHealth <= 0)
        {
            anim.Play("Harper Death");
            move.enabled = false;

            // Call the RpcGameOver method to trigger game over on all clients
            RpcGameOver();
        }
    }

    public void harperHurt(int damage)
    {
        if (!isServer)
            return;

        harperHealth -= damage;
        if (harperHealth < 0)
            harperHealth = 0;
    }

    void OnHealthChanged(int oldHealth, int newHealth)
    {
        // This method is called whenever the health changes across the network
        // You can use it to update UI, play effects, etc.
    }

    [ClientRpc]
    void RpcGameOver()
    {
        // Call the gameOver method on the local player's GameOverMenu
        FindObjectOfType<GameOverMenu>().gameOver();
    }
}
