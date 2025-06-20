using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BladeController : MonoBehaviour
{

    public BodySourceView bsv;
    public GameObject prefab;
    public GameObject prefab2;
    public float sliceForce = 5.0f;
   // private bool rightSlicing;
   // private bool leftSlicing;
   // private Collider rightBladeCollider;
   // private Collider leftBladeCollider;

   public UnityEngine.Vector3 directionRight {get; private set;}
   public UnityEngine.Vector3 directionLeft {get; private set;}
   private UnityEngine.Vector3 oldLeftPos;
   private UnityEngine.Vector3 oldRightPos;

   private UnityEngine.Vector3 rightPos;
   private UnityEngine.Vector3 leftPos;

    private float requiredTime = 3f;
    private float timeStart = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        directionLeft = UnityEngine.Vector3.zero;
        directionRight = UnityEngine.Vector3.zero;
        oldLeftPos = UnityEngine.Vector3.zero;
        oldRightPos = UnityEngine.Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {   
       // rightSlicing = true;
       // leftSlicing = true;

        if(bsv.Bodies.Count > 0)
        {
            Transform body = bsv.Bodies.First().Value.transform;
            Transform rightHand = body.Find("HandRight");
            Transform leftHand = body.Find("HandLeft");

            rightPos = rightHand.position;
            leftPos = leftHand.position;

            if(rightHand.Find("RightBlade") == null)
            {
                GameObject Blade = Instantiate(prefab, rightHand);
                Blade.name = "RightBlade";
                Blade.transform.localPosition = UnityEngine.Vector3.zero;
            }
            if(leftHand.Find("LeftBlade") == null){
                GameObject Blade = Instantiate(prefab2, leftHand);
                Blade.name = "LeftBlade";
                Blade.transform.localPosition = UnityEngine.Vector3.zero;
            }

            if(leftPos != oldLeftPos){
                directionLeft = leftPos - oldLeftPos;
            }
            if(rightPos != oldRightPos){
                directionRight = rightPos - oldRightPos;
            }
                
            oldLeftPos = leftHand.position;
            oldRightPos = rightHand.position;

            Transform head = body.Find("Head");
            UnityEngine.Vector3 vector3 = leftHand.position - head.position ;
            //Debug.Log("Position Y: " + vector3.y);
            //Debug.Log("Position X: " + vector3.x);
            //Debug.Log("Vector: " + vector3);

            if (vector3.x < 0  && vector3.y > 3) {
                timeStart += Time.deltaTime;

                if (timeStart > requiredTime) {
                    LoadSceneMenu();
                }
            } else {
                timeStart = 0f;
            }
                   
        }
    }

    public UnityEngine.Vector3 getRightPos(){
        return rightPos;
    }

    public UnityEngine.Vector3 getLeftPos(){
        return leftPos;
    }

    public void LoadSceneMenu()
    {
        SceneManager.LoadScene("MainSceneMenu"); //Assets/Scenes/SampleScene.unity
    }

}