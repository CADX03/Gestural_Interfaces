using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandRightController : MonoBehaviour
{
    // Start is called before the first frame update

    private int isInsideBox = 0;
    private float timeInsideBox = 0f;
    private float requiredTime = 3f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInsideBox == 3)
        {
            timeInsideBox += Time.deltaTime; // Increment timer while inside box

            if (timeInsideBox >= requiredTime)
            {
                Debug.Log("Hand has been inside the box3 for 3 seconds!");
                // Do whatever you need to do after 3 seconds inside the box
                //Load Scene here
                LoadScene1();
            }
        } else if (isInsideBox == 4)
        {
            timeInsideBox += Time.deltaTime; // Increment timer while inside box

            if (timeInsideBox >= requiredTime)
            {
                Debug.Log("Hand has been inside the box4 for 3 seconds!");
                // Quit the game here
                //Load Scene here
                QuitGame();
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Game3") && gameObject.CompareTag("HandR"))
        {
            isInsideBox = 3;
            Debug.Log("Hand entered the box3!");
        } else if (other.gameObject.CompareTag("Game4") && gameObject.CompareTag("HandR"))
        {
            isInsideBox = 4;
            Debug.Log("Hand entered the box4!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Game3") && gameObject.CompareTag("HandR"))
        {
            isInsideBox = 0;
            timeInsideBox = 0f; // Reset the timer when exiting the box
            Debug.Log("Hand exited the box3!");
        } else if (other.gameObject.CompareTag("Game4") && gameObject.CompareTag("HandR"))
        {
            isInsideBox = 0;
            timeInsideBox = 0f; // Reset the timer when exiting the box
            Debug.Log("Hand exited the box4!");
        }
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene("Game_Scene"); //Assets/Scenes/SampleScene.unity
    }

    void QuitGame()
    {
        // If we are running in a standalone build of the game
        #if UNITY_STANDALONE
            // Quit the application
            Application.Quit();
        #endif

        // If we are running in the editor
        #if UNITY_EDITOR
            // Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

