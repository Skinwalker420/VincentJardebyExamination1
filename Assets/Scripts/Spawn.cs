using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] Transform[] a;
    [SerializeField] GameObject[] s;
    [SerializeField] float timeBetweenSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCell", 1, timeBetweenSpawn);
    }

    void SpawnCell()
    {
        Instantiate(s[Random.Range(0, s.Length)], a[Random.Range(0, a.Length)].position, Quaternion.identity);
    }
}
