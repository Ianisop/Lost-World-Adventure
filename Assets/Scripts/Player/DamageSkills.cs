using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSkills : MonoBehaviour
{
    public AbilityManager abilityManager;
    private SpriteRenderer enemy_sr;
    private SpriteRenderer[] enemy_srs;
    private Color originalColor;
    private Color hit_color = new Color(229, 0, 0);
    private int[] skills = {0,1,2};
    private LayerMask targetLayers;
    public BoxCollider2D hitBox;

     private void OnTriggerEnter2D(Collider2D collision)
    {
        List<AbilityManager.Ability> abilities = abilityManager.getCurrentAbilities();
    
        List<Collider2D> hits = new List<Collider2D>();  
        hits.Add(collision);

        foreach(Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                enemy_srs = hit.GetComponentInParent<parole_enemy_ai>().srs;
                foreach (var x in enemy_srs)
                {
                    x.color = hit_color;
                }
                
                Debug.Log("Haiyaah! Enemy hit by strong attack");
                hit.gameObject.GetComponent<Enemy_Health>().health -= 3;
                
            }
            if (hit.CompareTag("rat"))
            {
                enemy_sr = collision.gameObject.GetComponentInParent<SpriteRenderer>();
                enemy_sr.color = hit_color;
    
                Debug.Log("Haiyaah! Enemy hit by strong attack");
                hit.gameObject.GetComponent<Enemy_Health>().health -= 3;
                
            }
            if (hit.CompareTag("turret"))
            {
                enemy_srs = hit.GetComponentInParent<stationary_enemy_ai>().srs;
                foreach (var x in enemy_srs)
                {
                    x.color = hit_color;
                }
               
                Debug.Log("Haiyaah! Enemy hit by strong attack");
                hit.gameObject.GetComponent<Enemy_Health>().health -= 3;
                
            }
            
        }
     
           
        
    }

   
}
