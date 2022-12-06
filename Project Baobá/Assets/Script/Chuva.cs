using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chuva : MonoBehaviour
{

    [SerializeField] GameObject[] PreFabs;
    [SerializeField] float cd = 0.5f;

    [SerializeField] float minTras;
    [SerializeField] float maxTras;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnChuva());
    }

    IEnumerator SpawnChuva(){
        while(true){
            var wanted = Random.Range(minTras,maxTras);
            var position = new Vector3(wanted,transform.position.y);
            GameObject gameObject = Instantiate(PreFabs[Random.Range(0,PreFabs.Length)],position,Quaternion.identity);
            yield return new WaitForSeconds(cd);
            Destroy(gameObject,5f);
        }
    }

}