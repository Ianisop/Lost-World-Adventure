using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlatformEffector2D))]
public class OneWayPlatform : MonoBehaviour
{
    PlatformEffector2D platformEffector;
    Collider2D platformCollider;

    WaitForSeconds waitTime = new WaitForSeconds(0.2f);

    // Start is called before the first frame update
    void Start()
    {
        platformEffector= GetComponent<PlatformEffector2D>();
        platformCollider = GetComponent<Collider2D>();

        PlayerControlManager control = PlayerControlManager.instance;
        control.OnMoveDown += OnMoveDown;
    }

    void OnMoveDown(InputAction.CallbackContext context)
    {
        platformCollider.enabled = false;
        StartCoroutine(EndMoveDownCoroutine());
    }

    IEnumerator EndMoveDownCoroutine()
    {
        yield return waitTime;

        platformCollider.enabled = true;
    }
}
