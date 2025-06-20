using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Spawner : MonoBehaviour
{
   private Collider spawnArea;

   public GameObject[] veggiePrefabs;
   public GameObject bomb;
   public float bombChance = 0.05f;
   private int startTime = 5;
   public Text timer;


   [SerializeField] public float minSpawnDelay = 1f;
   [SerializeField] public float maxSpawnDelay = 3.5f;

   [SerializeField] public float minAngle = -12f;
   [SerializeField] public float maxAngle = 13f;

   [SerializeField] public float minForce = 18f;
   [SerializeField] public float maxForce = 22f;

   [SerializeField] public float maxLifeTime = 5f;

   private void Awake() {
        spawnArea = GetComponent<Collider>();
   }

   private void OnEnable() {
        StartCoroutine(Spawn());
   }

    private void OnDisable() {
        StopAllCoroutines();
   }

    private IEnumerator Spawn(){

      yield return new WaitForSeconds(2f);

      while(startTime > 0){

         timer.text = startTime.ToString();
         startTime--;
         yield return new WaitForSeconds(1f);
      }
      timer.text = "";

      while(enabled){
         GameObject prefab;

         if(Random.value < bombChance){
            prefab = bomb;
         }
         else{
            prefab = veggiePrefabs[Random.Range(0,veggiePrefabs.Length)];
         }


         Vector3 position = new Vector3();
         position.x= Random.Range(spawnArea.bounds.min.x,spawnArea.bounds.max.x);
         position.y= Random.Range(spawnArea.bounds.min.y,spawnArea.bounds.max.y);
         position.z= Random.Range(spawnArea.bounds.min.z,spawnArea.bounds.max.z);

         Quaternion rotation = Quaternion.Euler(0f,0f,Random.Range(minAngle,maxAngle));

         GameObject fruit = Instantiate(prefab, position, rotation);
         Destroy(fruit, maxLifeTime);

         float force = Random.Range(minForce,maxForce);
         fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

         yield return new WaitForSeconds(Random.Range(minSpawnDelay,maxSpawnDelay));
      }
   }
}
