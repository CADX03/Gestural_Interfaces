using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FireballController : MonoBehaviour
{


    class HandFrameInfo {
        public DateTime t;
        public Vector3 p;

    }
    public BodySourceView bsv;
    public GameObject rightPrefab;
    public GameObject leftPrefab;

    public float throwVelocity = 250;

    private List<HandFrameInfo> rightHandFrames = new List<HandFrameInfo>();
    private List<HandFrameInfo> leftHandFrames = new List<HandFrameInfo>();

    private List<HandFrameInfo> leftHandFramesToRemove = new List<HandFrameInfo>();

    private List<HandFrameInfo> rightHandFramesToRemove = new List<HandFrameInfo>();


    private Vector3 previousRightVelocity = Vector3.zero;
    private Vector3 previousLeftVelocity = Vector3.zero;

    private DateTime lastThrow;

    private float timeStart = 0f;
    private float requiredTime = 3f;


    // Start is called before the first frame update
    void Start()
    {
        lastThrow = DateTime.MinValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(bsv.Bodies.Count == 1)
        {
            Transform body = bsv.Bodies.First().Value.transform;
            Transform rightHand = body.Find("HandRight");
            Transform leftHand = body.Find("HandLeft");

            /*if(rightHand.Find("BlueFireball") == null)
            {
                GameObject blueFireball = Instantiate(rightPrefab, rightHand);
                blueFireball.name = "BlueFireball";
                blueFireball.transform.localScale = new Vector3(5.5f, 5.5f, 0.1f);
                blueFireball.transform.localPosition = new Vector3(rightHand.position.x, rightHand.position.y,-1);
            }
            
            if(leftHand.Find("RedFireball") == null)
            {
                GameObject redFireball = Instantiate(leftPrefab, leftHand);
                redFireball.name = "RedFireball";
                redFireball.transform.localScale = new Vector3(5.5f, 5.5f, 0.1f);
                redFireball.transform.localPosition = new Vector3(leftHand.position.x, leftHand.position.y,-1);
            }
            if (rightHand.Find("BlueFireball") != null)
            {
                Transform blueFireballTransform = rightHand.Find("BlueFireball");
                if(blueFireballTransform.position.x > 1.4f || blueFireballTransform.position.y > 0.7f || blueFireballTransform.position.x < -1.4f || blueFireballTransform.position.y < -0.7f)
                {
                    Destroy(blueFireballTransform.gameObject);
                }
            }
            
            if (leftHand.Find("RedFireball") != null)
            {
                Transform redFireballTransform = leftHand.Find("RedFireball");
                if(redFireballTransform.position.x > 1.4f || redFireballTransform.position.y > 0.7f || redFireballTransform.position.x < -1.4f || redFireballTransform.position.y < -0.7f)
                {
                    Destroy(redFireballTransform.gameObject);
                }
            }*/
            
            trackMovement(rightHand, rightHandFrames, rightHandFramesToRemove, ref previousRightVelocity);
            trackMovement(leftHand, leftHandFrames, leftHandFramesToRemove, ref previousLeftVelocity);

            Transform head = body.Find("Head");
            Vector3 vector3 = leftHand.position - head.position;
            Debug.Log("Vector " + vector3);
            if (vector3.y > 0) {
                timeStart += Time.deltaTime;
                if (timeStart > requiredTime) {
                    LoadSceneMenu();
                }
            } else {
                timeStart = 0f;
            }

          
        }
    }

    void trackMovement(Transform hand, List<HandFrameInfo> handFrames, List<HandFrameInfo> framesToRemove, ref Vector3 previousVelocity){
    HandFrameInfo hfi = new HandFrameInfo();
    hfi.p = hand.position;
    hfi.t = DateTime.Now;

    handFrames.Add(hfi);

    HandFrameInfo past = null;

    float elapsedTime = 1.0f;

    foreach (HandFrameInfo frame in handFrames)
    {
        elapsedTime = (float)(hfi.t - frame.t).TotalMilliseconds;

        if (elapsedTime > throwVelocity)
        {
            past = frame;
            framesToRemove.Add(frame);
        }
        else
        {
            break;
        }
    }

    foreach(HandFrameInfo frame in framesToRemove){
        handFrames.Remove(frame);
    }
    framesToRemove.Clear();

    if(past != null){
        Vector3 currentVelocity = (hfi.p - past.p) / elapsedTime;
        //if(previousVelocity.magnitude > 0.0001f){
        //    Debug.Log("" + previousVelocity.magnitude);}
        if(previousVelocity.magnitude > 0.0005f && currentVelocity.magnitude < 0.0005f) 
        {
           // Debug.Log("throwing ");

DateTime now = DateTime.Now;
float elapsedThrowTime = (float)(now - lastThrow).TotalMilliseconds;
//print(previousVelocity.x + ";" + previousVelocity.y + ";" + previousVelocity.z);
if(elapsedThrowTime > 500)
{
            ThrowFireball(hand, hand.position, previousVelocity);
            lastThrow = now;
}
        }

        previousVelocity = currentVelocity;
    }
}


void ThrowFireball(Transform hand, Vector3 throwPosition, Vector3 throwVelocity)
{

    Vector3 throwDirection = throwVelocity.normalized;
    //GameObject fireball = hand.Find(hand.name == "HandRight" ? "BlueFireball" : "RedFireball")?.gameObject;

                GameObject fireball;
                if(hand.name == "HandRight") fireball = Instantiate(rightPrefab, hand);
                else fireball = Instantiate(leftPrefab, hand);
                //fireball.name = "BlueFireball";
                fireball.transform.localScale = new Vector3(5.5f, 5.5f, 0.1f);
                fireball.transform.parent = null;
                fireball.transform.position = new Vector3(hand.position.x, hand.position.y,-1);

    if (fireball != null)
    {
        //fireball.transform.parent = null;

        //fireball.transform.position = new Vector3(fireball.transform.position.x, fireball.transform.position.y, -1);

        float speed = 1f;
    //
        Vector3 adjustedVelocity = throwDirection * speed;
            

        Rigidbody rb = fireball.GetComponent<Rigidbody>();


        rb.isKinematic = false;
        rb.velocity = adjustedVelocity;
       // Debug.Log("FOI LANÇADO ");

        //Destroy(fireball, 2f);
    }
}



    public void LoadSceneMenu()
    {
        SceneManager.LoadScene("MainSceneMenu"); //Assets/Scenes/SampleScene.unity
    }


    }