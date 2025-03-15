using Unity.VisualScripting;
using UnityEngine;

public class BoundsSetter : MonoBehaviour
{
    void Start()
    {
        Vector2 spriteSize = gameObject.GetComponent<SpriteRenderer>().size;
        gameObject.GetComponent<BoxCollider2D>().size = spriteSize;
        gameObject.GetComponent<BoxCollider2D>().offset = Vector2.zero;
    }
}
