using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Controller : MonoBehaviour
{
    class HandFrameInfo {
        public DateTime t;
        public Vector3 p;
    }
    public BodySourceView bsv;
    public GameObject prefab;
    public float deltaTime = 250;
    private Vector3 pastVelocity = Vector3.zero;
    private BallShooter ballShooter;
    private List<HandFrameInfo> arrayFrames = new List<HandFrameInfo>();
    private List<HandFrameInfo> framesToRemove = new List<HandFrameInfo>();
    private float requiredTime = 3f;
    private float timeStart = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bsv.Bodies.Count == 1)
        {
            Transform body = bsv.Bodies.First().Value.transform;
            Transform rightHand = body.Find("HandRight");
            if(rightHand.Find("Olaf") == null)
            {
                GameObject olaf = Instantiate(prefab, rightHand);
                olaf.name = "Olaf";
                olaf.transform.localPosition = Vector3.zero;
                ballShooter = olaf.GetComponent<BallShooter>();
            }
            HandFrameInfo hfi = new HandFrameInfo();
            hfi.p = rightHand.position;
            hfi.t = DateTime.Now;
            arrayFrames.Add(hfi);
            HandFrameInfo past = null;
            float elapsedTime = 1.0f;
            
            foreach(HandFrameInfo frame in arrayFrames){
                elapsedTime = (float)(hfi.t - frame.t).TotalMilliseconds;
                if (elapsedTime > deltaTime){
                    past = frame;
                    framesToRemove.Add(frame);
                } else {
                    break;
                }
            }
            foreach(HandFrameInfo frame in framesToRemove){
                arrayFrames.Remove(frame);
            }
            framesToRemove.Clear();
            if (past != null) {
         
                Vector3 currentVelocity = (hfi.p - past.p) / elapsedTime;
                    if(pastVelocity != Vector3.zero){
                        float deltaMagnitude = currentVelocity.magnitude - pastVelocity.magnitude;
                        if(currentVelocity.magnitude < 0.01 && pastVelocity.magnitude > 0.01)
                        {
                            if(pastVelocity.z < 0)
                            {
                                ballShooter.Shoot(pastVelocity);
                            }
                        }
                    }
                    pastVelocity = currentVelocity;
    
            }

            Transform leftHand = body.Find("HandLeft");
            Transform head = body.Find("Head");
            Vector3 vector3 = leftHand.position - head.position ;
            //Debug.Log("Position Y: " + vector3.y);
            //Debug.Log("Position X: " + vector3.x);

            if (vector3.x > 0  && vector3.y > 0) {
                timeStart += Time.deltaTime;

                if (timeStart > requiredTime) {
                    LoadSceneMenu();
                }
            } else {
                timeStart = 0f;
            }


            
            

                    
        }
    }

    public void LoadSceneMenu()
    {
        SceneManager.LoadScene("MainSceneMenu"); //Assets/Scenes/SampleScene.unity
    }

}
