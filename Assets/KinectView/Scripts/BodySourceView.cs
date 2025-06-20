using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using UnityEngine.SceneManagement;

public class BodySourceView : MonoBehaviour 
{
    public Material BoneMaterial;
    public GameObject BodySourceManager;

    public bool invert = false;
    public bool fireball = false;
    public bool vegSamuri = false;
    
    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    private List<Kinect.JointType> _joints = new List<Kinect.JointType>()
    {
        Kinect.JointType.HandLeft,
        Kinect.JointType.HandRight,
    };
    
    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };

    public Dictionary<ulong, GameObject> Bodies { get => _Bodies; }

    void Awake()
    {
        // Register the callback to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unregister the callback to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Set the invert variable to true only if the loaded scene is "MainScene"
        if (scene.name == "MainScene"){
            invert = true;
            fireball = false;
            vegSamuri = false;
        } else if (scene.name == "FireballBeatSaber"){
            fireball = true;
            invert = false;
            vegSamuri = false;
        } else if (scene.name == "Game_Scene"){
            vegSamuri = true;
            invert = false;
            fireball = false;
        }
        else {
            vegSamuri = false;
            invert = false;
            fireball = false;
        }
    }

    void Update () 
    {
        //Debug.Log("Invert: " + invert);
        //Debug.Log("vegSamuri: " + vegSamuri);
        //Debug.Log("fireball: " + fireball);

        if (BodySourceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }
        
        List<ulong> knownIds = new List<ulong>(Bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(Bodies[trackingId]);
                Bodies.Remove(trackingId);
            }
        }

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                if(!Bodies.ContainsKey(body.TrackingId))
                {
                    Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                    if(vegSamuri) {
                        DisableVisuals(_Bodies[body.TrackingId]);
                    }
                }
                
                RefreshBodyObject(body, Bodies[body.TrackingId]);
            }
        }
    }

    
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            if (fireball || vegSamuri){
                GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                jointObj.GetComponent<Collider>().enabled = false;
                
                LineRenderer lr = jointObj.AddComponent<LineRenderer>();
                lr.SetVertexCount(2);
                lr.material = BoneMaterial;
                lr.SetWidth(0.05f, 0.05f);
                
                jointObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                jointObj.name = jt.ToString();
                jointObj.transform.parent = body.transform;
            } else {
                GameObject jointObj = new GameObject();//GameObject.CreatePrimitive(PrimitiveType.Cube);
                //jointObj.GetComponent<Collider>().enabled = false;
                
                /*LineRenderer lr = jointObj.AddComponent<LineRenderer>();
                lr.SetVertexCount(2);
                lr.material = BoneMaterial;
                lr.SetWidth(0.05f, 0.05f);*/
                
                jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                jointObj.name = jt.ToString();
                jointObj.transform.parent = body.transform;
            }
            
        }
        
        return body;

        
    }
    
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        Vector3 spineBasePosition = GetVector3FromJoint(body.Joints[Kinect.JointType.SpineBase]);

        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            if(fireball || vegSamuri){
                Kinect.Joint sourceJoint = body.Joints[jt];
                Kinect.Joint? targetJoint = null;

                if(_BoneMap.ContainsKey(jt))
                {
                    targetJoint = body.Joints[_BoneMap[jt]];
                }

                Transform jointObj = bodyObject.transform.Find(jt.ToString());

                Vector3 jointPositionOffset = GetVector3FromJoint(sourceJoint) - spineBasePosition;

                Vector3 finalJointPosition;
                if(vegSamuri){
                    finalJointPosition = new Vector3(jointPositionOffset.x, jointPositionOffset.y, 0);
                } else {
                    finalJointPosition = new Vector3(jointPositionOffset.x, jointPositionOffset.y, -1);
                }
                
                jointObj.localPosition = finalJointPosition;

                LineRenderer lr = jointObj.GetComponent<LineRenderer>();
                if(targetJoint.HasValue)
                {
                    Vector3 targetPositionOffset = GetVector3FromJoint(targetJoint.Value) - spineBasePosition;
                    Vector3 finalTargetPosition = new Vector3(targetPositionOffset.x, targetPositionOffset.y, -1);

                    lr.SetPosition(0, finalJointPosition);
                    lr.SetPosition(1, finalTargetPosition);
                    lr.SetColors(GetColorForState (sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
                }
                else
                {
                    lr.enabled = false;
                }
            } else {
                Kinect.Joint sourceJoint = body.Joints[jt];
                Kinect.Joint? targetJoint = null;
                
                if(_BoneMap.ContainsKey(jt))
                {
                    targetJoint = body.Joints[_BoneMap[jt]];
                }
                
                Transform jointObj = bodyObject.transform.Find(jt.ToString());
                jointObj.localPosition = GetVector3FromJoint(sourceJoint);
                
                /*LineRenderer lr = jointObj.GetComponent<LineRenderer>();
                if(targetJoint.HasValue)
                {
                    lr.SetPosition(0, jointObj.localPosition);
                    lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                    lr.SetColors(GetColorForState (sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
                }
                else
                {
                    lr.enabled = false;
                }*/
            }
            
        }
    }
    
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        if (fireball) {
            return new Vector3(joint.Position.X *0.25f , joint.Position.Y *0.25f, joint.Position.Z *0.25f);
        } else {
            return new Vector3((invert? -1.0f : 1.0f) * joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
        }
        
    }

    private void DisableVisuals(GameObject body)
    {
        foreach (Transform joint in body.transform)
        {
            // Disable the MeshRenderer
            MeshRenderer renderer = joint.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
            
            // Disable the LineRenderer
            LineRenderer lr = joint.GetComponent<LineRenderer>();
            if (lr != null)
            {
                lr.enabled = false;
            }
            
            // Disable the Collider
            Collider collider = joint.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }
}
