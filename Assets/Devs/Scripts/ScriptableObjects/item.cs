using UnityEngine;

public class Item : MonoBehaviour
{
   public Spells PickUp;


    public int spawnRate;
    private void Awake()
    {
        spawnRate = PickUp.dropRate;
    }
}