using UnityEngine;

public class DeathReset : MonoBehaviour
{
    private Player Player;

    public delegate void OnDeathReste();
    public static OnDeathReste onDeath;

    private void Start()
    {
        Player = FindObjectOfType<Player>();
        onDeath += Death;
    }

    private void Update()
    {
        if (!Player.isAlive)
        {
            onDeath();
        }
    }

    private void Death()
    {
        Player.animator.SetBool("isAlive", Player.isAlive);
        Player.inventory.container.Clear();
    }
}
