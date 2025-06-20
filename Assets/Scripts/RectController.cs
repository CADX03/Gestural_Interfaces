using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RectController : MonoBehaviour
{

    public Transform target;
    public float speed;

    public static int score = 0;
    public static int health = 5;

    private float timeStart = 0f;
    private float requiredTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.deltaTime + "TEMPO");
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position,step);

        if(health <= 0){
            //Time.timeScale = 0f;
            timeStart += Time.deltaTime;
            if (timeStart > requiredTime) {
                    LoadSceneMenu();
                }
        } else {
            timeStart = 0f;
        }

        if(transform.position == target.position){
            Destroy(gameObject);
            health--;
        }
        //Debug.Log(score); ELE ESTA A DAR UPDATE AO SCORE POR ISSO POSSO DAR UPDATE AO SPEED FOR SURE!
    }
    public void LoadSceneMenu()
    {
        SceneManager.LoadScene("MainSceneMenu"); //Assets/Scenes/SampleScene.unity
    }

}