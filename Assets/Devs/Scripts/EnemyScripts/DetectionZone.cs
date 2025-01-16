using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    Collider2D col;
    public List<Collider2D> detectioncolls = new List<Collider2D>();

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectioncolls.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        detectioncolls.Remove(other);
    }

}
