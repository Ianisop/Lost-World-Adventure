using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource main;
    public AudioSource boss;
    // Start is called before the first frame update
    void Start()
    {
        main.Play();
    }

    [ContextMenu("Boss")]
    public void OnBoss()
    {
        main.Stop();
        boss.Play();
    }
}
