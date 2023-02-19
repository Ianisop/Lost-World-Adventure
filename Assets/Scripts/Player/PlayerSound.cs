using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioSource attack;
    public AudioSource jump;
    public AudioSource footstep;
    public AudioSource fall;
    public AudioSource landing;
    public AudioSource stealth;

    public void OnFootstep()
    {
        footstep.Play();
    }
}
