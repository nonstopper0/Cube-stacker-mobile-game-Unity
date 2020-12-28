using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buildScript : MonoBehaviour
{

    [Header("Money Related Variables")]
    [SerializeField]
    public static long money;
    public static int moneyMultiplier;
    public static int moneyMultiplierMultiplier;
    public static int idleLevel;
    public static int clickLevel;
    public static int prestige;

    [Header("Settings Varialbes")]
    public static bool textParticles = true;

    [Header("Debug variables")]
    public long costToUpgradeClickLevel;
    public long costToUpgradeIdleTime;
    public int clicksToLevelUp;
    // seconds between each auto build
    public float idleClickerTimeDelay;
    // time tracker used for progress bar
    private float timeElapsed = 0f;

    public float cameraSpeed;
    public float currentHeight = 1f;

    [Header("Object Assignments")]

    public GameObject cubePrefab;

    // upgrade click button
    public Text clickLevelDisplayText;
    public Text clickLevelUpgradeText;
    public Slider clickLevelSlider;

    // upgrade idle button 
    public Text idleLevelDisplayText;
    public Text idleLevelUpgradeText;
    public Slider idleLevelSlider;

    public Text heightText;
    public Text moneyText;

    public Text idleTimeDisplayText;
    public Slider progressBar;

    public Camera mainCamera;


    IEnumerator IdleClicker()
    {
        yield return new WaitForSeconds(idleClickerTimeDelay);

        // reset progress bar once 5 seconds has been reached to stop time delays on upgrade of idleClickerTimeDelay
        progressBar.value = 0;
        timeElapsed = 0;
        this.onBuildClick();
        StartCoroutine(IdleClicker());
    } 

    void Start()
    {
        cameraSpeed = 9.5f;

        prestige = 1;
        money = 0;
        moneyMultiplier = 1;
        moneyMultiplierMultiplier = 1;

        idleLevel = 1;
        idleClickerTimeDelay = 5f;
        costToUpgradeIdleTime = 500;

        clickLevel = 1;
        costToUpgradeClickLevel = 200;
        
        


        //starts the clicker using the idleClickerTimeDelay value
        StartCoroutine(IdleClicker());
    }

    void Update()
    {
        // update on screen variables
        moneyText.text =  System.String.Format("{0:n0}", money);
        heightText.text = (currentHeight / 3).ToString("0.0") + "m";
        idleTimeDisplayText.text = idleClickerTimeDelay.ToString("0.00") + "/s";

        // Progress Bar update
        if (timeElapsed < idleClickerTimeDelay) {
            progressBar.value = Mathf.Lerp(0, 1, timeElapsed / idleClickerTimeDelay);
            timeElapsed += Time.deltaTime;
        }

        //Camera adjustments to follow cubes
        Vector3 mainCameraPosition = mainCamera.transform.position;
        Vector3 newCameraPosition = new Vector3(mainCameraPosition.x, currentHeight + 2.5f, mainCameraPosition.z);
        mainCamera.transform.position = Vector3.MoveTowards(mainCameraPosition, newCameraPosition, cameraSpeed * Time.deltaTime);
    }

    public void onUpgradeClickLevelClick()
    {
        if (money >= costToUpgradeClickLevel && clickLevel < 100) {

            // modify values for next level
            money -= System.Convert.ToInt64(costToUpgradeClickLevel);
            costToUpgradeClickLevel = System.Convert.ToInt64(200 * System.Math.Pow(1.20, clickLevel));
            clickLevel += 1;

            // calculate money per click (moneyMultiplier increase) based on each 25th level..
            if (clickLevel % 25 == 0) {
                moneyMultiplierMultiplier *= 2;
                moneyMultiplier *= moneyMultiplierMultiplier;
            } else {
                // I apply the prestige by multiplying only the number being added because multiplying the number itself would cause unwated expotential growth.
                moneyMultiplier += moneyMultiplierMultiplier * prestige;
            }

            // used for data logging
            clicksToLevelUp = System.Convert.ToInt32(costToUpgradeClickLevel / moneyMultiplier);

            // Display new values
            clickLevelDisplayText.text = "Lvl. " + clickLevel;
            clickLevelSlider.value = clickLevel % 25;
            clickLevelUpgradeText.text = System.String.Format("{0:n0}", costToUpgradeClickLevel);
        } 
    }

    public void onUpgradeIdleTimeClick()
    {
        if (money >= costToUpgradeIdleTime) {
            progressBar.value = 0;
            timeElapsed = 0;
            // modify values for next level
            money -= System.Convert.ToInt64(costToUpgradeIdleTime);
            costToUpgradeIdleTime = System.Convert.ToInt64(500 * System.Math.Pow(1.40, idleLevel));
            idleLevel += 1;

            // THIS IS ALL HOPELESS
            
            // if (idleLevel % 10 == 0 && idleClickerTimeDelay - 1f > 0f) {
            //     idleClickerTimeDelay -= 1;
            // } else if (idleClickerTimeDelay - 0.05f > 0.05f && idleLevel % 10 != 0) {
            //         idleClickerTimeDelay -= 0.05f;
            // } else {
            //     idleLevelDisplayText.text = "Lvl. Max";
            // }

            // Display new values
            idleLevelDisplayText.text = "Lvl. " + idleLevel;
            idleLevelSlider.value = idleLevel % 10;
            idleLevelUpgradeText.text = System.String.Format("{0:n0}", costToUpgradeIdleTime);
        }
    }

    public void onBuildClick()
    {
        this.buildCube();
    }

    private void buildCube() 
    {
        GameObject cube = Instantiate(cubePrefab);
        cube.gameObject.name = currentHeight.ToString();
        cube.transform.position = new Vector3(0f, currentHeight, 0f);
        currentHeight += 1;
        money += System.Convert.ToInt32(moneyMultiplier);
    }
}
