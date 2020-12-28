using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeMenu : MonoBehaviour
{

    public bool menuOpen = false;
    public GameObject menu;

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
    // Update is called once per frame
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
}
