using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    // Start is called before the first frame update
    public float health { 
        get { return _health; } 
        set { 
            _health = value; 
            hurtSound?.Play();
        } 
    }
    public float _health = 100f;
    public static Enemy_Health Instance;
    public AudioSource hurtSound;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            if(gameObject.tag == "Enemy") gameObject.GetComponentInParent<parole_enemy_ai>().SelfDestruct();
            if(gameObject.tag == "rat") gameObject.GetComponentInParent<rat_ai>().SelfDestruct();
            if(gameObject.tag == "turret") gameObject.GetComponentInParent<stationary_enemy_ai>().SelfDestruct();


        }
    }
}
