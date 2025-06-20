using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSSpawner : MonoBehaviour
{
    [SerializeField] private GameObject blueRect;
    [SerializeField] private GameObject redRect;

    [SerializeField] private float blueRectInterval = 3.5f;
    [SerializeField] private float redRectInterval = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn(blueRectInterval,blueRect));
        StartCoroutine(spawn(redRectInterval,redRect));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawn(float interval, GameObject target){
        if(RectController.health > 0){
            yield return new WaitForSeconds(interval);
        
            GameObject newTarget = Instantiate(target, new Vector3(Random.Range(1.6f,2f),Random.Range(-0.75f,0.75f),-1), Quaternion.identity);
            var scaleChange = new Vector3(0.1f,1f,1f);
            newTarget.transform.localScale = scaleChange;
            
            float scaleY = UnityEngine.Random.Range(0.2f,0.5f);
            scaleChange.y = scaleY;
            newTarget.transform.localScale = scaleChange;
            StartCoroutine(spawn(interval,target));
        }
        
    }
}
