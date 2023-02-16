using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill_me : MonoBehaviour
{
    // Start is called before the first frame update
    public static kill_me Instance;
    public float damage = 0.5f;


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
        
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("player"))
        {
            PlayerHealth.Instance.TakeDamage(0.5f);
        }
    }
}
