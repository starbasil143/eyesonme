using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject deathObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(deathObject, transform.position, transform.rotation, transform.parent);
            collision.gameObject.GetComponentInChildren<CPlayer>().GainCharge();
            Destroy(gameObject);
            
        }   
    }
}
