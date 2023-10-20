using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Microbes germType;

    #region Standard Parameters
    [SerializeField] private Vector2 wobbleParameter = new(-.002f, .002f); 
    [SerializeField] private Vector2 randomMovementParameterX = new(-.05f, .05f);
    [SerializeField] private Vector2 randomMovementParameterY = new(-.05f, .05f);
    #endregion

    #region Red Parameters
    [SerializeField] private bool aggressive;
    [SerializeField] private float redMoveSpeed;
    [SerializeField, Tooltip("Decides size increase of cells that eat this cell.")] Vector2 germHypertrophy;
    #endregion

    #region Green Parameters
    [SerializeField] Vector2 greenMovement;
    [SerializeField] Vector2 scaleLimit;
    [SerializeField] float rotationSpeed;
    [SerializeField] float greenMoveSpeed = 1f;
    [SerializeField] float positionChangeInterval;
    #endregion

    #region Blue Parameters
    [SerializeField] float blueSpeed;
    [SerializeField, Tooltip("Decides size decrease of cells that eat this cell.")] Vector2 germHypotrophy;
    #endregion

    #region Private Variables
    private Vector2 currentGreenMovePosition;
    private UnityEngine.Quaternion currentRotation;
    #endregion

    #region Cached References
    private GameObject player;
    #endregion
    // Start is called before the first frame update
    public enum Microbes
    {
        RedMicrobe,
        GreenMicrobe,
        PassiveMicrobe,
        Antidote
    }
    void Start()
    {
        if(aggressive){player = GameObject.FindWithTag("Player");}
        if(germType == Microbes.GreenMicrobe) { InvokeRepeating("GreenMovement", 0, positionChangeInterval); }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(germType == Microbes.RedMicrobe)
        {
            if (aggressive)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, redMoveSpeed);
            }
            StandardMovement();
        }
        else if(germType == Microbes.GreenMicrobe) 
        {
            transform.position = Vector2.MoveTowards(transform.position, currentGreenMovePosition, greenMoveSpeed);
            Debug.Log(currentGreenMovePosition);
            GreenScaleLimit();
            GreenRotation();
            StandardMovement();
        }
        else if(germType == Microbes.Antidote)
        {
            transform.position = Vector2.MoveTowards(transform.position, AntidoteFollow().transform.position, blueSpeed);
            StandardMovement();
        }
        else
        {
            StandardMovement();
        }
    }

    void StandardMovement()
    {
        transform.position += new Vector3(Random.Range(randomMovementParameterX.x, randomMovementParameterX.y), Random.Range(randomMovementParameterY.x, randomMovementParameterY.y));
        float wobbleRng = Random.Range(wobbleParameter.x, wobbleParameter.y);
        transform.localScale += new Vector3(wobbleRng, wobbleRng); 
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(germType == Microbes.RedMicrobe)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (other.gameObject.CompareTag("Green Microbe"))
            {
                Destroy(gameObject);
                other.transform.localScale += (Vector3)germHypertrophy;
            }
        }
        else if(germType == Microbes.Antidote) 
        {
            if(other.gameObject.CompareTag("Green Microbe"))
            {
                Destroy(gameObject);
                other.transform.localScale -= (Vector3)germHypotrophy;
            }
        }
       
    }
    void GreenMovement()
    {
        currentGreenMovePosition = Random.insideUnitCircle * greenMovement;
    }

    void GreenRotation()
    {
        if (currentGreenMovePosition != (Vector2)transform.position)
        {
            currentRotation = UnityEngine.Quaternion.Euler(0, 0, Mathf.Atan2(currentGreenMovePosition.y, currentGreenMovePosition.x) * Mathf.Rad2Deg);
            Debug.Log(currentRotation.eulerAngles.z);
        }
        transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation, currentRotation, rotationSpeed);
    }
    void GreenScaleLimit()
    {
        if(transform.localScale.x <= scaleLimit.x || transform.localScale.y <= scaleLimit.x)
        {
            transform.localScale = new (scaleLimit.x, scaleLimit.x);
        }
        else if(transform.localScale.x >= scaleLimit.y || transform.localScale.y >= scaleLimit.y)
        {
            transform.localScale = new(scaleLimit.y, scaleLimit.y);
        }
    }

    GameObject AntidoteFollow()
    {
        GameObject[] greenMicrobes = GameObject.FindGameObjectsWithTag("Green Microbe");
        GameObject closestMicrobe = null;
        float bestDistance = Mathf.Infinity;
        foreach(GameObject microbe in greenMicrobes) 
        {
            if(Vector2.Distance(transform.position, microbe.transform.position) < bestDistance)
            {
                closestMicrobe = microbe;
                bestDistance = Vector2.Distance(transform.position, microbe.transform.position);
            }
        }
        if(closestMicrobe != null)
        {
            return closestMicrobe;
        }
        else
        {
            return null;
        }
    }
}