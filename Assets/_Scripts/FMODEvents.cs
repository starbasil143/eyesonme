using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance { get; private set; }

    [Header("SFX Voices")]
    public EventReference voice_default;
    public EventReference voice_father;
    public EventReference voice_typing;
    public EventReference voice_inner;
    public EventReference voice_deep;
    public EventReference voice_nasal;
    public EventReference voice_soft;
    public EventReference voice_kid;
    public EventReference voice_stupid;

    [Header("SFX Player")]
    public EventReference sfx_beam_charge;
    public EventReference sfx_beam_fire;
    public EventReference sfx_beam_continue;
    public EventReference sfx_beam_reflect;
    public EventReference sfx_beam_hum;

    [Header("SFX Targets")]
    public EventReference sfx_wall;
    public EventReference sfx_wall_breakable;
    public EventReference sfx_wall_flimsy;
    public EventReference sfx_wall_bad;
    public EventReference sfx_wall_mirror;

    [Header("SFX Enemies")]
    public EventReference sfx_skeleton;
    public EventReference sfx_zombie;
    public EventReference sfx_revenant;
    public EventReference sfx_tank;

    [Header("SFX UI")]
    public EventReference sfx_select;
    public EventReference sfx_confirm;
    public EventReference sfx_startgame;
    
    [Header("SFX General")]
    public EventReference sfx_fill;
    public EventReference sfx_cheers;

    [Header("Ambience")]
    public EventReference ambience;

    [Header("Music")]
    public EventReference music;




    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("it broke. the fmodevents. the singleton.");
        }
    }


}
