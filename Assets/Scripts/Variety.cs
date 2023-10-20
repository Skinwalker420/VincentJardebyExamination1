using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Variety : MonoBehaviour
{
    [SerializeField] private Vector2 wobbleParameter = new(-.002f, .002f); 
    [SerializeField] private Vector2 randomMovementParameterX = new(-.05f, .05f);
    [SerializeField] private Vector2 randomMovmentParameterY = new(-.05f, .05f);
    private GameObject player;
    private bool aggressive;
    
    // Start is called before the first frame update
    enum Microbes
    {
        RedMicrobe,
        BlueMicrobe,
        GreenMicrobe,
        
    }
    void Start()
    {
        if(aggressive){player = GameObject.FindWithTag("Player");}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (aggressive)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.005f);
        }
        
        transform.position += new Vector3(Random.Range(randomMovementParameterX.x, randomMovementParameterX.y), Random.Range(randomMovmentParameterY.x, randomMovmentParameterY.y));
        float wobbleRng = Random.Range(wobbleParameter.x, wobbleParameter.y);
        transform.localScale += new Vector3(wobbleRng, wobbleRng);
    }
}
