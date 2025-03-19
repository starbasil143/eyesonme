using UnityEngine;
using UnityEngine.Rendering;

public class Exploder : MonoBehaviour
{
    public GameObject deathObject;
    public TimelineManager _winCutscene;
    public void Explode()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.sfx_ping, transform.position);
        Instantiate(deathObject, transform.position, transform.rotation, transform.parent);
    }

    public void KillSelf()
    {
        Destroy(gameObject);
    }

    public void StartWinCutscene()
    {
        _winCutscene.gameObject.SetActive(true);
    }
}
