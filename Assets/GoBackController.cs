using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackController : MonoBehaviour
{
    public void GoBackToMenu()
    {
        // Assuming your main menu scene is called "MainMenu"
        SceneManager.LoadScene("MainSceneMenu");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
