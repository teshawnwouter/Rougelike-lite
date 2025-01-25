using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathReset : MonoBehaviour
{
    private Player Player;

    public delegate void OnDeathReste();
    public static OnDeathReste onPLayerDeath;
    private void Start()
    {


        Player = FindObjectOfType<Player>();
        onPLayerDeath += PlayerDeath;


    }

    private void Update()
    {
        if (!Player.isAlive)
        {
            onPLayerDeath();
        }
    }

    private void PlayerDeath()
    {
        Player.animator.SetBool("isAlive", Player.isAlive);
        Player.inventory.container.Clear();
    }
}
