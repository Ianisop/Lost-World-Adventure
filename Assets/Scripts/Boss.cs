using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float speed;
    private float distance;
    private float attack_timer;
    private GameObject fistObj;
    public float health = 100;
    public SpriteRenderer[] srs;
    public Image image;
    private float minValue = 0.0f;
    private float maxValue = 1.0f;
    public float repeatRate_attack;
    [Tooltip("how many seconds until it runs the attack function again")]
    public float repeatRate_move;
    [Tooltip("how many seconds until it moves the boss to the fist")]
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        fistObj = GameObject.FindGameObjectWithTag("fist");
        srs = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //what the fuck
        var t1 = health - minValue;
        var t2 = maxValue - minValue;
        var t3 = t1 / t2;
        float fillAmount = t3;

        // Set the fill amount of the image
        image.fillAmount = fillAmount;
    
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        if(player_global_vars.Instance.boss_fight)
        {
            InvokeRepeating("attack", 2f, repeatRate_attack);
            InvokeRepeating("move", 10f, repeatRate_move);
            //InvokeRepeating("rats", 20f, 10f);


        }
    }


    void attack()
    {
        fistObj.GetComponent<FistScript>().attack();
    }

    void move()
    {
        Vector2 target = player.transform.position;
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

}
