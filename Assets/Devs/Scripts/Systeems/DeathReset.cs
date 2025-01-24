using UnityEngine;

public class DeathReset : MonoBehaviour
{
    private Player Player;
    private Enemy[] enemy;
    private Bat bats;

    public delegate void OnDeathReste();
    public static OnDeathReste onPLayerDeath;

   

    public delegate void OnBatDeath();
    public static OnBatDeath onBatDeath;

    private void Start()
    {
        enemy = FindObjectsOfType<Enemy>();

        Player = FindObjectOfType<Player>();
        onPLayerDeath += PlayerDeath;

        bats = FindAnyObjectByType<Bat>();
        onBatDeath += BatDeath;
    }

    private void Update()
    {
        if (!Player.isAlive)
        {
            onPLayerDeath();
        }

        //for (int i = 0; i < enemy.Length; i++)
        //{
        //    if (!enemy[i].isAlive)
        //    {
        //        onEnemyDeath();
        //    }
        //}

        //if (bats.isAlive)
        //{
        //    onBatDeath();
        //}
    }

    private void PlayerDeath()
    {
        Player.animator.SetBool("isAlive", Player.isAlive);
        Player.inventory.container.Clear();
    }

    private void EnemyDeath()
    { 
        
    }

    private void BatDeath()
    {
        FindFirstObjectByType<LootList>().DroppedTheItem(transform.position);
    }

}
