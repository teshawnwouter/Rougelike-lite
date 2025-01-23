using Unity.VisualScripting;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    public Transform castPoint;
    private Player player;

    [Header("Spells")]
    public Spells fireBall;
    public Spells auraBlast;
    public Spells iceShard;
    public Spells healing;
    public Spells explosion;

    [Header("GameObjects")]
    public GameObject fire;
    public GameObject ice;
    public GameObject aura;
    public GameObject heal;
    public GameObject explosions;


    private void Awake()
    {
        player = GetComponent<Player>();
    }

    /// <summary>
    /// dit zijn de spells die je kan casten
    /// </summary>
  
    public void FireBall()
    {
        if (fireBall != null)
        {
            if (player.inventory.container.Contains(fireBall)) { Instantiate(fire,castPoint.position,Quaternion.identity); }
        }
    }

    public void IceShard()
    {
        if (iceShard != null)
        {
            if (player.inventory.container.Contains(iceShard)) { Instantiate(ice, castPoint.position,Quaternion.identity); }
        }
    }

    public void AuraBurst()
    {
        if (auraBlast != null)
        {
            if (player.inventory.container.Contains(auraBlast)) { Instantiate(aura, castPoint.position, Quaternion.identity); }
        }
    }

    public void Healing()
    {
        if (healing != null)
        {
            if (player.inventory.container.Contains(healing)) { Instantiate(healing, castPoint.position, Quaternion.identity); }
        }
    }

    public void Explosion()
    {
        if (explosion != null)
        {
            if (player.inventory.container.Contains(explosion)) { Instantiate(explosions, castPoint.position, Quaternion.identity); }
        }
    }

}
