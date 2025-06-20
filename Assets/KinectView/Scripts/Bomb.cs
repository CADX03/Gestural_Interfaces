using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{   
    private GameObject controllerObject;
    private List<GameObject> bombParts = new List<GameObject>();
    private GameManager gameManager;
    private ParticleSystem explosionEffect;
    private Collider collisor;
    
    private void Awake(){
        gameManager = FindObjectOfType<GameManager>();
        explosionEffect = GetComponentInChildren<ParticleSystem>();

        foreach (Transform child in transform)
        {
            bombParts.Add(child.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(controllerObject == null){
            controllerObject = GameObject.Find("Controller");
            if(other.CompareTag("Right")){
                gameManager.Explode();
                explosionEffect.Play();
                Destroy();
            }
            if(other.CompareTag("Left")){
                gameManager.Explode();
                explosionEffect.Play();
                Destroy();
            }
        }
        else{
            if(other.CompareTag("Right")){
                gameManager.Explode();
                explosionEffect.Play();
                Destroy();
            }    
            if(other.CompareTag("Left")){
                gameManager.Explode();
                explosionEffect.Play();
                Destroy();
            }
        }
    }


    private void Destroy(){

        if (collisor != null)
        {
        collisor.enabled = false;
        }
        
        foreach(GameObject part in bombParts){
            MeshRenderer renderer = part.GetComponent<MeshRenderer>();

            if (renderer != null){
                renderer.enabled = false;
            }
        }
    }
}
