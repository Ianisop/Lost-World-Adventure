using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    
    public float playerHealth;
    public float maxHealth;
    public static PlayerHealth Instance;
    public GameObject heartPrefab;
    public List<HeartsDisplay> hearts = new List<HeartsDisplay>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        setMaxHealth(maxHealth);
        setHealth(maxHealth);
    }

    void Update()
    {
        CreateHearts();
        if(playerHealth <= 0)
        {
            Time.timeScale = 0;
            Destroy(gameObject);

            FindObjectOfType<GameOver>(true).OnGameOver();
        }
    }

    //methods to handle the value of the player's health
    public void setHealth(float value)
    {
        playerHealth = value;
        CreateHearts();
    }
    public void setMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        CreateHearts();
    }
    public void heal(float healAmount)
    {
        Debug.Log("Healing");
        float temp = playerHealth + healAmount; 
        if(temp > maxHealth)
            playerHealth = maxHealth;
        else
            playerHealth = temp;
        
        CreateHearts();
    }
    public void TakeDamage(float damageTaken)
    {
        List<AbilityManager.Ability> abilities = AbilityManager.Instance.getCurrentAbilities();
        if(abilities.Contains(AbilityManager.Instance.healing))
        {
            Debug.Log("Resisting");
            playerHealth -= (damageTaken - 1);
        }
        else
        {
            playerHealth -= damageTaken;
        }
        
        foreach(var x in player_global_vars.Instance.srs)
        {
            x.color = player_global_vars.Instance.hit_color;
        }
        Invoke("takeDamageEffect", 0.2f);
        setHealth(playerHealth);

        CreateHearts();
    }


    //methods that handles the UI display of hearts
    public void CreateHearts()
    {
        ClearHearts();

        float maxHealthRemainder = maxHealth % 2;
        int heartsAmount = (int)((maxHealth/2) + maxHealthRemainder);

        for (int i = 0; i < heartsAmount; i++)
        {
            CreateEmptyHeart();
        } 

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRem = (int)Mathf.Clamp(playerHealth - (i*2),0,2);
            hearts[i].SetHearthStatus((HeartsDisplay.HeartStatus)heartStatusRem);
        }
    }
    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HeartsDisplay>();
    }
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
        newHeart.transform.localScale = new Vector3(1f,1f,1f);

        //Spawn empty heart and display empty heart
        HeartsDisplay heartComponent = newHeart.GetComponent<HeartsDisplay>();
        heartComponent.SetHearthStatus(HeartsDisplay.HeartStatus.empty);
        hearts.Add(heartComponent);
    }

    void takeDamageEffect()
    {
        foreach(var x in player_global_vars.Instance.srs)
        {
            x.color = Color.white;
        }
        
    }

}
