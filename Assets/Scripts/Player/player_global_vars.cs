using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_global_vars : MonoBehaviour
{
    // Start is called before the first frame update
    public static player_global_vars Instance;
    public bool stealthed = false;
    public bool is_boosted = false; //added this variable so we can check if the strong attack passive is active
    public SpriteRenderer[] srs;
    public Color hit_color = new Color(229, 0, 0);
    public bool boss_fight;
    public GameObject prefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        stealthed = false;
        srs = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boss_fight)
        {
            prefab.SetActive(true);
        }
    }
}
