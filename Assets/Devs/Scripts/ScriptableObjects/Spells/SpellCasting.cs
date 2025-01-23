using Unity.VisualScripting;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    public Transform castPoint;

    public Spells fireBall;
    public GameObject fire;

    public Spells iceShard;
    public GameObject ice;

    public Spells auraBlast;
    public GameObject aura;

    public Spells healing;
    public GameObject heal;

    public Spells explosion;
    public GameObject explosions;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

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
