using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomModelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Models;
    void Start()
    {
        SelectRandomModel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectRandomModel()
    {
        // Disable all models
        foreach (GameObject model in Models)
        {
            model.SetActive(false);
        }

        // Select a random model to enable
        int randomIndex = Random.Range(0, Models.Length);
        Models[randomIndex].SetActive(true);
    }
}
