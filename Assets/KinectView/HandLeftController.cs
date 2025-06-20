using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandLeftController : MonoBehaviour
{
    // Start is called before the first frame update
    private int isInsideBox = 0;
    private float timeInsideBox = 0f;
    private float requiredTime = 3f;

    /*
    public GameObject prefab;
    public GameObject HandL_in;
    */
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Transform leftHand = transform.Find("hand_l");

        if (leftHand.Find("HandL_In") == null)
        {
            HandL_in = Instantiate(prefab, leftHand);
            HandL_in.name = "HandL_In";
            HandL_in.transform.localPosition = Vector3.zero;
        }
        */

        if (isInsideBox == 1)
        {
            timeInsideBox += Time.deltaTime; // Increment timer while inside box

            if (timeInsideBox >= requiredTime)
            {
                Debug.Log("Hand has been inside the box1 for 3 seconds!");
                // Do whatever you need to do after 3 seconds inside the box
                //Load Scene here
                LoadScene1();
            }
        } else if (isInsideBox == 2)
        {
            timeInsideBox += Time.deltaTime; // Increment timer while inside box

            if (timeInsideBox >= requiredTime)
            {
                Debug.Log("Hand has been inside the box2 for 3 seconds!");
                // Do whatever you need to do after 3 seconds inside the box
                //Load Scene here
                LoadScene2();
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Game1") && gameObject.CompareTag("HandL"))
        {
            isInsideBox = 1;
            Debug.Log("Hand entered the box1!");
        } else if (other.gameObject.CompareTag("Game2") && gameObject.CompareTag("HandL"))
        {
            isInsideBox = 2;
            Debug.Log("Hand entered the box2!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Game1") && gameObject.CompareTag("HandL"))
        {
            isInsideBox = 0;
            timeInsideBox = 0f; // Reset the timer when exiting the box
            Debug.Log("Hand exited the box1!");
        } else if (other.gameObject.CompareTag("Game2") && gameObject.CompareTag("HandL"))
        {
            isInsideBox = 0;
            timeInsideBox = 0f; // Reset the timer when exiting the box
            Debug.Log("Hand exited the box2!");
        }
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene("MainScene"); //Assets/Scenes/SampleScene.unity
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene("FireballBeatSaber"); //Assets/Scenes/SampleScene.unity
    }
    
}
