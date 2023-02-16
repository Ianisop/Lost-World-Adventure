using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public PlayerMovement player_movement;
    public SpriteRenderer[] sr;
    public Transform[] transforms;
    public PlayerControlManager control;
    void Start()
    {
        sr = GetComponentsInChildren<SpriteRenderer>();
        transforms = GetComponentsInChildren<Transform>();
        control = PlayerControlManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (control.MoveDir.x < 0)
        {
            gameObject.transform.localScale = new Vector2(-1, gameObject.transform.localScale.y);
        }
        if (control.MoveDir.x > 0)
        {
            gameObject.transform.localScale = new Vector2(1, gameObject.transform.localScale.y);
        }




        anim.SetFloat("x", player_movement.velocity.sqrMagnitude);
    }
}
