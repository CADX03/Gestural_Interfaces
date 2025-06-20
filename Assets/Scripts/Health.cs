
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{   

    public TextMesh scoreText;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      scoreText.text = "Lives: " + RectController.health;
    }

}

