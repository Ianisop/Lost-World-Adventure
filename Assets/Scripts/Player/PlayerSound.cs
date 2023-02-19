using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    // Done
    public AudioSource attack;
    public GameObject strongAttackHitBox;
    public AudioSource jump;
    public AudioSource footstep;
    public AudioSource dash;
    public AudioSource stealth;
    public AudioSource heavyAttackHit;
    public AudioSource healing;
    // Not sure how to do
    public AudioSource heavyAttackMiss;
    public AudioSource landing;
    public AudioSource fall;
    public AudioSource dashHit;

    public void OnAttack()
    {
        if (strongAttackHitBox.activeInHierarchy)
            heavyAttackHit.Play();
        else
            attack.Play();
    }
    public void OnJump() => jump.Play();
    public void OnFootstep() => footstep.Play();
    public void OnDash() => dash.Play();
    public void OnStealth() => stealth.Play();
    public void OnHeal() => healing.Play();

    // Not sure how to do
    public void OnLand() => landing.Play();
}
