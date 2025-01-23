using UnityEngine;
using UnityEngine.UI;
public abstract class PickUpItem : ScriptableObject
{
    public enum PickUpType
    {
        health,
        spell,
    }
    [Header("variables")]
    public PickUpType pickUpType;
    public Sprite sprite;
    public int dropRate;
    public int Value;

}
