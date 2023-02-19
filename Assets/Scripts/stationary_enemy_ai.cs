using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stationary_enemy_ai : MonoBehaviour
{
    // Start is called before the first frame update

    public bool attack;
    public static stationary_enemy_ai Instance;

    public GameObject projectilePrefab; 
    private GameObject player; 
    public int numProjectiles; 
    public float delayBetweenProjectiles;
    private Vector2 direction;
    private float angle;
    public SpriteRenderer[] srs;
    public float distance;

    public AudioSource shoot;
    public AudioSource notice;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        srs = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector3 directions = player.transform.position - transform.position;
        directions.Normalize();

        if ((transform.position.x > player.transform.position.x))
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }

        
        // calculate the angle between the spawner and the player
        direction = (player.transform.position - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
    }

    public IEnumerator StartShooting()
    {
        if (attack && player_global_vars.Instance.stealthed == false)
        {
            yield return SpawnProjectiles();
            yield return new WaitForSeconds(2);
        }
    }

    public IEnumerator SpawnProjectiles()
    { 

        for (int i = 0; i < numProjectiles; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, angle, 0));
            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidbody.velocity = direction * 10f; // adjust the velocity to control the speed of the projectiles

            angle += 360f / numProjectiles;

            shoot.Play();
            yield return new WaitForSeconds(delayBetweenProjectiles);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            attack = true;
            StartCoroutine(StartShooting());
            Debug.Log("player detected");

            notice.Play();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("player") && attack == true)
        {
            
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            Debug.Log("player left");
            attack = false;
            StopAllCoroutines();
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
