using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeHitBox : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer enemy_sr;
    private SpriteRenderer[] enemy_srs;
    private Color originalColor;
    private Color hit_color = new Color(229, 0, 0);
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {

            enemy_srs = collision.GetComponentInParent<parole_enemy_ai>().srs;
            foreach (var x in enemy_srs)
            {
                x.color = hit_color;
            }
            Invoke("ResetSpritesColor", 0.1f);

            if(player_global_vars.Instance.is_boosted == true)
                collision.gameObject.GetComponent<Enemy_Health>().health -= 2;
            else
                collision.gameObject.GetComponent<Enemy_Health>().health -= 1;
            
        }
        if (collision.CompareTag("rat"))
        {
            enemy_sr = collision.gameObject.GetComponentInParent<SpriteRenderer>();
            enemy_sr.color = hit_color;
            Invoke("ResetSpriteColor", 0.1f);
            Enemy_Health enemy_Health = collision.gameObject.GetComponent<Enemy_Health>();
            if (enemy_Health != null)
            {
                if (player_global_vars.Instance.is_boosted == true)
                    enemy_Health.health -= 2;
                else
                    enemy_Health.health -= 1;

            }

        }
        if (collision.CompareTag("turret"))
        {
            Debug.Log("hit the turret");
            enemy_srs = collision.GetComponentInParent<stationary_enemy_ai>().srs;
            foreach (var x in enemy_srs)
            {
                x.color = hit_color;
            }
            Invoke("ResetSpritesColor", 0.1f);
            if(player_global_vars.Instance.is_boosted == true)
                collision.gameObject.GetComponent<Enemy_Health>().health -= 2;
            else
                collision.gameObject.GetComponent<Enemy_Health>().health -= 1;

        }
        if (collision.CompareTag("Boss"))
        {
            Debug.Log("hit the boss");
            enemy_srs = collision.GetComponentInParent<Boss>().srs;
            foreach (var x in enemy_srs)
            {
                x.color = hit_color;
            }
            Invoke("ResetSpritesColor", 0.1f);
            if (player_global_vars.Instance.is_boosted == true)
                collision.gameObject.GetComponent<Boss>().health -= 3;
            else
                collision.gameObject.GetComponent<Boss>().health -= 3;

        }
    }


    void ResetSpritesColor()
    {
        foreach(var x in enemy_srs)
        {
            x.color = Color.white;
        }

    }
    void ResetSpriteColor()
    {
        enemy_sr.color = Color.white;
    }
}
