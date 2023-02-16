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

     private void OnTriggerEnter2D(Collider2D collision)
    {
        List<Ability> abilities = abilityManager.getCurrentAbilities();
        float skillDmg = 3;
        foreach (var ability in abilities)
        {
            if(ability.abilityName == "Strong Attack")
                skillDmg = 3;
        }
        
        if (collision.CompareTag("Enemy") || collision.CompareTag("rat") || collision.CompareTag("turret"))
        {
            enemy_srs = collision.GetComponentInParent<parole_enemy_ai>().srs;
            foreach (var x in enemy_srs)
            {
                x.color = hit_color;
            }
            Invoke("ResetSpritesColor", 0.1f);
            collision.gameObject.GetComponent<Enemy_Health>().health -= skillDmg;
            
        }
    }
}
