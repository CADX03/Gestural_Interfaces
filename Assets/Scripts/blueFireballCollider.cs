using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueFireballCollider : MonoBehaviour
{
        public static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("blueRect") && gameObject.CompareTag("blueFireball"))
        {
            Destroy(other.gameObject); 
            Destroy(gameObject);
            score += 10;
        }
    }
}