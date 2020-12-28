using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class starSpawner : MonoBehaviour
// {

//     public GameObject starPrefab;
//     public GameObject mainScript;
//     private float gameHeight;
//     private bool run;
//     // Start is called before the first frame update
//     void Start()
//     {
//         run = true;
//         mainScript = GameObject.Find("Cube Builder");
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         gameHeight = mainScript.GetComponent<buildScript>().currentHeight;
//         print(gameHeight + gameHeight % 5);
//         if (gameHeight % 5 == 0 && run == true) {
//             print(gameHeight + "creating stars");
//             createStar();
//         } if (gameHeight % 5 != 0 && run == false) {
//             run = true;
//         } else {
//             run = false;
//         }

//         if (gameHeight % 50 == 0) {
//             GameObject.Find("Star");
//         }
//     }

//     void createStar()
//     {
//         int i = 0;
//         while(i < 5) {
//             GameObject star = Instantiate(starPrefab, new Vector3(1f, gameHeight, 1f), Quaternion.identity);
//             i = i + 1;
//         }
//         run = false;
//     }
// }
