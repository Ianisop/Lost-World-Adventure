using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 4f;
    [Tooltip("how much damage each time you collide with the fist ")]
    private GameObject player;
    Vector2 direction;
    public float speed = 2f;
    private float distance;
    private bool canTakeDamage = true;
    public float invulnerabilityTime = 2f;
    [Tooltip("how many seconds before you can take damage again")]

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        
    }

    public void attack()
    {
        Vector2 target = player.transform.position;
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("player") && canTakeDamage)
        {
            PlayerHealth.Instance.TakeDamage(damage);
            canTakeDamage = false;
            Invoke("ResetCanTakeDamage", invulnerabilityTime);
        }

    }



    void ResetCanTakeDamage()
    {
        canTakeDamage = true;
    }
}
