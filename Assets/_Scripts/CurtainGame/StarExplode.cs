using System.Collections;
using UnityEngine;

public class StarExplode : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ExplodeDie());
    }

    IEnumerator ExplodeDie()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_ping, transform.position);
        yield return new WaitForSeconds(.1f);
        GetComponentInChildren<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 3);
    }
}
