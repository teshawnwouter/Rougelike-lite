using UnityEngine;
public enum PickUpType
{
    health,
    spell,
}

public abstract class PickUpItem : ScriptableObject 
{
    public GameObject prefab;
    public PickUpType pickUpType;
    [TextArea(20,20)]
    public string description;
}
