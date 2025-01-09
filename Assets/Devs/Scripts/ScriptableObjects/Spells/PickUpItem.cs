using UnityEngine;
public abstract class PickUpItem : ScriptableObject
{
    public enum PickUpType
    {
        health,
        spell,
    }

    [Header("variables")]
    public GameObject prefab;
    public PickUpType pickUpType;
    [TextArea(20, 20)]
    public string description;
}
