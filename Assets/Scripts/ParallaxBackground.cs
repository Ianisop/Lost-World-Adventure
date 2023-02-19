using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float minBackgroundHeight;
    public float maxBackgroundHeight;

    public float playerMinHeight;
    public float playerMaxHeight;

    PlayerMovement player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        float remapValue = Remap(player.transform.position.y, playerMinHeight, playerMaxHeight, minBackgroundHeight, maxBackgroundHeight);
        transform.localPosition = new Vector3(0f, remapValue, 10);
    }

    public float Remap(float aValue, float aIn1, float aIn2, float aOut1, float aOut2)
    {
        float t = (aValue - aIn1) / (aIn2 - aIn1);
        return aOut1 + (aOut2 - aOut1) * t;
    }
}