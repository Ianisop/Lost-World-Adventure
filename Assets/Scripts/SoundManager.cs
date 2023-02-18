using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Vector2 velocity;
    public AudioSource audio_source;
    public AudioClip footstepSound;
    public float minTimeBetweenSteps; // minimum time between footstep sounds
    public float minTimeBetweenSteps_sprint; // minimum time between footstep sounds
    private float timeSinceLastStep = 0f; // time that has passed since the last footstep sound

    void Start()
    {

    }

    void Update()
    {
        velocity = gameObject.GetComponent<PlayerMovement>().velocity;
        if (Mathf.Abs(velocity.x) > 0 && timeSinceLastStep >= minTimeBetweenSteps)
        {
            audio_source.PlayOneShot(footstepSound, 1f);
            timeSinceLastStep = 0f;
        }

        timeSinceLastStep += Time.deltaTime;
    }
}
