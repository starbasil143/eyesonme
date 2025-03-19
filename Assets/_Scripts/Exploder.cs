using UnityEngine;
using UnityEngine.Rendering;

public class Exploder : MonoBehaviour
{
    public GameObject deathObject;
    public void Explode()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_wall_mirror, transform.position);
        Instantiate(deathObject, transform.position, transform.rotation, transform.parent);
    }

    public void KillSelf()
    {
        Destroy(gameObject);
    }
}
