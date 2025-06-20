using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ControllerMenu : MonoBehaviour
{
    public BodySourceView bodySourceView;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (bodySourceView.Bodies.Count != 0){

            Transform body = bodySourceView.Bodies.First().Value.transform;
            //Transform body = GameObject.Find("body").transform;

            Transform Head = body.Find("Head");

            Transform Neck = body.Find("Neck");

            Transform SpineShoulder = body.Find("SpineShoulder");

            Transform ShoulderLeft = body.Find("ShoulderLeft");
            Transform ShoulderRight = body.Find("ShoulderRight");

            Transform RightHand = body.Find("HandRight");
            Transform ElbowRight = body.Find("ElbowRight");
            

            Transform LeftHand = body.Find("HandLeft");
            Transform ElbowLeft = body.Find("ElbowLeft");

            Transform HipLeft = body.Find("HipLeft");
            Transform HipRight = body.Find("HipRight");

            Transform SpineBase = body.Find("SpineBase");

            Transform KneeLeft = body.Find("KneeLeft");
            Transform KneeRight = body.Find("KneeRight");

            Transform FootLeft = body.Find("FootLeft");
            Transform FootRight = body.Find("FootRight");

            /* Model */

            Transform shoulderModelR = GameObject.Find("upperarm_r").transform;
            Transform elbowModelR = GameObject.Find("lowerarm_r").transform;
            Transform handModelR = GameObject.Find("hand_r").transform;

            Transform shoulderModelL = GameObject.Find("upperarm_l").transform;
            Transform elbowModelL = GameObject.Find("lowerarm_l").transform;
            Transform handModelL = GameObject.Find("hand_l").transform;

            Transform hipModelR = GameObject.Find("upperleg_r").transform;//upperleg_r
            Transform kneeModelR = GameObject.Find("lowerleg_r").transform;//lowerleg_r
            Transform footModelR = GameObject.Find("foot_r").transform;

            Transform hipModelL = GameObject.Find("upperleg_l").transform;//upperleg_l
            Transform kneeModelL = GameObject.Find("lowerleg_l").transform;//lowerleg_l
            Transform footModelL = GameObject.Find("foot_l").transform;

            Transform neckModel = GameObject.Find("neck_01").transform; //neck_01
            Transform headModel = GameObject.Find("head").transform;
            
            Transform pelvisModel = GameObject.Find("pelvis").transform;
            Transform shoulderModel = GameObject.Find("spine_03").transform;

            if (ShoulderRight != null)
            {
                
                RotateJoint(ShoulderRight, ElbowRight, shoulderModelL, elbowModelL);
                RotateJoint(ShoulderLeft, ElbowLeft, shoulderModelR, elbowModelR);

                RotateJoint(ElbowLeft, LeftHand, elbowModelR, handModelR);
                RotateJoint(ElbowRight, RightHand, elbowModelL, handModelL);

                //RotateJoint(HipRight, KneeRight, hipModelL, kneeModelL);
                //RotateJoint(HipLeft, KneeLeft, hipModelR, kneeModelR);

                //RotateJoint(KneeRight, FootRight, kneeModelL, footModelL);
                //RotateJoint(KneeLeft, FootLeft, kneeModelR, footModelR);

                RotateJoint(Neck, Head, neckModel, headModel);
                //RotateJoint(SpineBase, SpineShoulder, pelvisModel, shoulderModel);
               
                /*
                
                RotateJoint(ShoulderRight, ElbowRight, shoulderModelR, elbowModelR);
                RotateJoint(ShoulderLeft, ElbowLeft, shoulderModelL, elbowModelL);

                RotateJoint(ElbowLeft, LeftHand, elbowModelL, handModelL);
                RotateJoint(ElbowRight, RightHand, elbowModelR, handModelR);

                RotateJoint(HipRight, KneeRight, hipModelR, kneeModelR);
                RotateJoint(HipLeft, KneeLeft, hipModelL, kneeModelL);

                RotateJoint(KneeRight, FootRight, kneeModelR, footModelR);
                RotateJoint(KneeLeft, FootLeft, kneeModelL, footModelL);

                RotateJoint(Neck, Head, neckModel, headModel);
                RotateJoint(SpineBase, SpineShoulder, pelvisModel, shoulderModel);
                */
                
                
            }
        }
        



    }

    private static void RotateJoint(Transform sourceOrigin, Transform sourceDestination, Transform targetOrigin, Transform targetDestination)
    {
        Vector3 bodyShoulderElbow = sourceDestination.position - sourceOrigin.position;
        Vector3 modelShoulderElbow = targetDestination.position - targetOrigin.position;

        Quaternion q = Quaternion.FromToRotation(modelShoulderElbow, bodyShoulderElbow);
        targetOrigin.rotation = q * targetOrigin.rotation;
    }
}