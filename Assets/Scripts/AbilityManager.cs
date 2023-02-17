using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class AbilityManager : MonoBehaviour
{
    public class Ability
    {
        public string abilityName;
        public float damage;
        public float cooldown;
        public event Action OnUse;
        //public static event onuse OnUse;
        public Ability(string name, float dmg, float cd, Action onuse)
        {
            abilityName = name;
            damage = dmg;
            cooldown = cd;
            OnUse = onuse;

        }
        public void use()
        {
            OnUse?.Invoke();
        }
    }


    public static AbilityManager Instance;

    //ability images
    public Image a_image_1;
    public Image a_image_2;
    public Image a_image_3;
    public Image a_bgr_image_1;
    public Image a_bgr_image_2;
    public Image a_bgr_image_3;

    //cooldowns
    public float a_nextUseTime_1 = 0f;
    public float a_nextUseTime_2 = 0f;
    public float a_nextUseTime_3 = 0f;
    public float target_time;

    //current abilities
    private Ability current_ability_1;
    private Ability current_ability_2;
    private Ability current_ability_3;
    public Ability healing;
    public Ability dash;
    public Ability strongAttack;
    public Ability InvChameleon;
    public List<Ability> all_abilities = new List<Ability>();


    //others
    public SpriteRenderer sr;
    public PlayerMovement player_movement;
    public GameObject player;
    public Rigidbody2D r;
    public Animator playerAnim;
    public SpriteRenderer[] srs;
    public GameObject strongHit;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {

        //define all abilities
        InvChameleon = new Ability("Stealth", 0f, 20f, InvChameleonAction);
        dash = new Ability("dash", 0f, 10f, dashAction);
        strongAttack = new Ability ("Strong Attack", 3f, 12f, strongAttackAction);
        healing = new Ability("Healing", 0f, 40f, healingAction);

        //Add abilities
        all_abilities.Add(healing);
        all_abilities.Add(strongAttack);
        all_abilities.Add(InvChameleon);
        all_abilities.Add(dash);

        current_ability_1 = GetRandomAbility();
        current_ability_2 = GetRandomAbility();
        current_ability_3 = GetRandomAbility();
        //get compoenents
        player = GameObject.FindGameObjectWithTag("player");
        
        

        

        Debug.Log("Current Abilities: " + current_ability_1.abilityName + ", " + current_ability_2.abilityName + ", " + current_ability_3.abilityName);
    }


    void Update()
    {
        //image fill
        float fillAmount1 = 1 - (a_nextUseTime_1 - Time.time) / current_ability_1.cooldown;
        a_image_1.fillAmount = fillAmount1;

        float fillAmount2 = 1 - (a_nextUseTime_2 - Time.time) / current_ability_2.cooldown;
        a_image_2.fillAmount = fillAmount2;

        float fillAmount3 = 1 - (a_nextUseTime_3 - Time.time) / current_ability_3.cooldown;
        a_image_3.fillAmount = fillAmount3;

        //check for input
        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= a_nextUseTime_1)
        {
            StartCoroutine(Haptic(a_bgr_image_1));
            current_ability_1.use();
            a_nextUseTime_1 = Time.time + current_ability_1.cooldown;

        }
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= a_nextUseTime_2)
        {
            StartCoroutine(Haptic(a_bgr_image_2));
            current_ability_2.use();

            a_nextUseTime_2 = Time.time + current_ability_2.cooldown;
        }

        if (Input.GetKeyDown(KeyCode.R) && Time.time >= a_nextUseTime_3)
        {
            StartCoroutine(Haptic(a_bgr_image_3));
            current_ability_3.use();

            a_nextUseTime_3 = Time.time + current_ability_3.cooldown;
        }

    }

    IEnumerator Haptic(Image img)
    {
        img.rectTransform.sizeDelta = new Vector2(60, 70);
        yield return new WaitForSeconds(0.1f);
        img.rectTransform.sizeDelta = new Vector2(77, 80);
    }




    void healingAction()
    {
        PlayerHealth.Instance.heal(2);
    }

    void InvChameleonAction()
    {
        StartCoroutine(stealth());
    }

    void dashAction()
    {
        player_movement.StartDash();

    }
        //still a work in progress
    void strongAttackAction()
    {
        StartCoroutine(strong());
    }

    IEnumerator stealth()
    {
        player_global_vars.Instance.stealthed = true;
        
        Debug.Log("starting stealth");
        Color initialColor;
        initialColor = sr.color;
        foreach(var x in srs)
        {
            x.color = Color.gray;
            player_movement.overrideMaxSpeed = 11;
            player_movement.ifOverrideMaxSpeed = true;
        
        }
        yield return new WaitForSeconds(5f);
        foreach (var x in srs)
        {
            x.color = initialColor;
            player_movement.ifOverrideMaxSpeed = false;
            player_movement.overrideMaxSpeed = 9;

        }
        player_global_vars.Instance.stealthed = false;

    }

    IEnumerator strong()
    {
        //activate hit box to deal damage
        strongHit.SetActive(true);
        //Start Passive buff
       
        Debug.Log("Knife Attacks are now boosted");

        //Just something to visualize the passive buff
        foreach (var x in srs)
        {
            if (x.sprite.name == "test_6" || x.sprite.name == "test_9" || x.sprite.name == "test_10")
                x.color = Color.blue;
        }
        yield return new WaitForSeconds(1f);
        strongHit.SetActive(false);

    }

    public List<Ability> getCurrentAbilities()
    {
        List<Ability> abilities = new List<Ability>();
        abilities.Add(current_ability_1);
        abilities.Add(current_ability_2);
        abilities.Add(current_ability_3);

        return abilities;
    }


    Ability GetRandomAbility()
    {
        List<Ability> abilities = getCurrentAbilities();
        Ability picked_ability;
        bool is_valid = false;
        int i;

        i = UnityEngine.Random.Range(0, all_abilities.Count);
        while(is_valid == false)
        {
            //if the random ability is equal to one of the abilities in the list
            if (abilities.Contains(all_abilities[i]))
                i = UnityEngine.Random.Range(0, all_abilities.Count); //search for new ability
            else
                is_valid = true;
        }
            
            picked_ability = all_abilities[i];
            if (picked_ability.abilityName == "Strong Attack")
                 player_global_vars.Instance.is_boosted = true;
                 
        return picked_ability;
    }
}
