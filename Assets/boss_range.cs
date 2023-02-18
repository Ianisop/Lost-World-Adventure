using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_range : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("player"))
        {
            player_global_vars.Instance.boss_fight = true;
        }
    }
}
