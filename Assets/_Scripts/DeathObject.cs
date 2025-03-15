using System.Collections;
using UnityEngine;

public class DeathObject : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ExplodeDie());
    }

    IEnumerator ExplodeDie()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_ping, transform.position);
        yield return new WaitForSeconds(.01f);

        
        AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_smash, transform.position);
        GetComponentInChildren<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 3);
    }

}
