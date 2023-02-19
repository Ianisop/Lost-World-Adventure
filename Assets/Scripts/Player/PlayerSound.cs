using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioSource attack;
    public AudioSource jump;
    public AudioSource footstep;
    public AudioSource landing;
    public AudioSource fall;
    public AudioSource stealth;

    public void OnFootstep()
    {
        footstep.Play();
    }
    public void OnAttack()
    {
        attack.Play();
    }
    public void OnJump()
    {
        jump.Play();
    }
    public void OnLand()
    {
        landing.Play();
    }
}
