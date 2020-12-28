using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsMenu : MonoBehaviour
{

    private bool menuOpen = false;

    public static bool textParticles = true;

    public GameObject menu;
    public RawImage textParticlesOn;


    // Menu Logic


    void Start()
    {
        menu.SetActive(false);
    }

    void Update()
    {
        if (menuOpen) {
            HideIfClickedOutside();
        }
    }

    public void openMenu()
    {
        if (menuOpen == true) {
            menuOpen = false;
            menu.SetActive(false);
        } else {
            menuOpen = true;
            menu.SetActive(true);
        }
    }

    private void HideIfClickedOutside() {
        if (Input.GetMouseButtonDown(0) && 
             !RectTransformUtility.RectangleContainsScreenPoint(menu.GetComponent<RectTransform>(), Input.mousePosition, Camera.main)) {
                 menu.SetActive(false);
                 menuOpen = false;
             }
    }


    // Settings


    // particle settings
    public void textParticleSetting() {
        textParticles = !textParticles;
        textParticlesOn.enabled = !(textParticlesOn.enabled);
    }

}
