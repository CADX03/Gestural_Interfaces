
using UnityEngine;
using UnityEngine.UI;

public class ScoreFireball : MonoBehaviour
{

    public TextMesh scoreText;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      scoreText.text = "Score: " + redFireballCollider.score + blueFireballCollider.score;
    }

}