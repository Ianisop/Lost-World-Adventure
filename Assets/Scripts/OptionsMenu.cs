using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;

    public void OnBackButtonPressed()
    {
        menu.SetActive(false);
    }
}
