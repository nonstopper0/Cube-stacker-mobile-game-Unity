using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeController : MonoBehaviour
{

    private float thisColor;
    private float lastColor;

    public GameObject floatingText;

    private int currentObjectNumber;

    private string converted;
    private string converted2;
    private string convertedText;



    void Start() 
    {
        string parentName = gameObject.transform.parent.name;
        currentObjectNumber = System.Convert.ToInt32(parentName);
        converted = (currentObjectNumber - 29).ToString();
        converted2 = (currentObjectNumber - 1).ToString();

        //Get current money per click for text
        double moneyMultiplier = buildScript.moneyMultiplier;
        double clickLevel = buildScript.clickLevel;


        // Spawn floating money if setting is on
        if (settingsMenu.textParticles == true) {
            GameObject moneyText = Instantiate(floatingText, transform.position, Quaternion.identity);
            moneyText.GetComponentInChildren<TextMesh>().text = "+" + moneyMultiplier;
            moneyText.name = "text" + currentObjectNumber;
            convertedText = "text" + (currentObjectNumber - 29).ToString();
        }

        //Delete old objects
        if (currentObjectNumber - 29 > 0) {
            this.DeleteOldObjects();
        }


        // if (currentObjectNumber > 1) {
        //     GameObject lastCube = GameObject.Find(converted2);
        //     Transform lastCubeChild = lastCube.transform.GetChild(0);
        //     lastColor = lastCubeChild.GetComponent<cubeController>().thisColor;
        //     bool lastDirection = lastCubeChild.GetComponent<cubeController>().thisDirection;
        //     print(lastColor);
        // } else {
        //     lastColor = 0f;
        // }

        if(clickLevel >= 25 && clickLevel < 50) {
            GetComponent<Renderer>().material.color = new Color(2.5f, 1f, 0f, 1);            
        } else if (clickLevel >= 50 && clickLevel < 75) {
            GetComponent<Renderer>().material.color = new Color(2f, 1f, 0f, 1);           
        } else if (clickLevel >= 75 && clickLevel < 100) {
            GetComponent<Renderer>().material.color = new Color(1.5f, 1f, 0f, 1);           
        } 
        else {
            GetComponent<Renderer>().material.color = new Color(3.0f, 1f, 0f, 1);           
        }
    }

    void DeleteOldObjects()
    {
        GameObject textToDelete = GameObject.Find(convertedText);
        GameObject objectToDelete = GameObject.Find(converted);
        Destroy(objectToDelete);
        Destroy(textToDelete);
    }
}
