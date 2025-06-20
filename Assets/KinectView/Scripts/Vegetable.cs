using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Content;
using UnityEngine;

public class Vegetable : MonoBehaviour
{   
    public GameObject Sliced;
    public GameObject Whole;

    private Rigidbody vegRigidBody;
    private Collider vegCollider;
    private GameObject controllerObject;
    private ParticleSystem juiceEffect;

    public int points = 1;


    private void Awake(){
        vegRigidBody = GetComponent<Rigidbody>();
        vegCollider = GetComponent<Collider>();
        juiceEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force){

        FindAnyObjectByType<GameManager>().IncreaseScore(points);
        
        Whole.SetActive(false);
        Sliced.SetActive(true);
        vegCollider.enabled = false;

        juiceEffect.Play();

        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        Sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = Sliced.GetComponentsInChildren<Rigidbody>();
        Collider[] parts = Sliced.GetComponentsInChildren<Collider>();

            


        foreach(Rigidbody slice in slices){
            slice.velocity = vegRigidBody.velocity;
            slice.AddForceAtPosition(direction * force, position,ForceMode.Impulse);
        }
        
        /*
        foreach(Collider part in parts){
            part.enabled = false;
        }*/


        return;
    }

    private void OnTriggerEnter(Collider other) {
        if(controllerObject == null){
            controllerObject = GameObject.Find("Controller");
            BladeController blades = controllerObject.GetComponent<BladeController>() ;
            if(other.CompareTag("Right")){
                Slice(blades.directionRight, blades.getRightPos() , blades.sliceForce);
            }
            if(other.CompareTag("Left")){
                Slice(blades.directionLeft, blades.getLeftPos() , blades.sliceForce);
            }
        }
        else{
            BladeController blades = controllerObject.GetComponent<BladeController>() ;
            if(other.CompareTag("Right")){
                Slice(blades.directionRight, blades.getRightPos() , blades.sliceForce);
            }    
            if(other.CompareTag("Left")){
                Slice(blades.directionLeft, blades.getLeftPos() , blades.sliceForce);
            }
        }
    }
}