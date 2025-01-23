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
    [TextArea(20, 20)]
    public string description;
    public Sprite sprite;
    public int dropRate;
    public int Value;

}
