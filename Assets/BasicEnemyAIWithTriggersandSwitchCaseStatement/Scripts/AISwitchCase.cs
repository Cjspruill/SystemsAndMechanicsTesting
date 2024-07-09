using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AISwitchCase : MonoBehaviour
{

    [SerializeField] public enum EnemyState //Enemy state enum that holds all possible states
    {
        Idle,
        Notice,
        Chase,
        Confused,
        Patrol,
    }
    [SerializeField] float speed;   //Movement speed of this object
    [SerializeField] public EnemyState enemyState; //Enemy state variable that holds current state
    [SerializeField] public GameObject player;  //Player reference to have player follow
    [SerializeField] MeshRenderer meshRenderer; //Mesh renderer reference to change material color
    [SerializeField] Color origColor;   //Original color reference set on start
    [SerializeField] Transform[] navPoints;
    [SerializeField] int navIndex;
    [SerializeField] bool canNavigate;
    [SerializeField] float patrolSpeed;

    [SerializeField] Image uncertaintyImage;
    [SerializeField]public bool isConfused;
    [SerializeField] float confusedTime;
    [SerializeField] float confusedTimer;

    [SerializeField] AINoticeRange aINoticeRange;
    [SerializeField] AIChaseRange aIChaseRange;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;   //Set enemy state to idle
        origColor = meshRenderer.material.color;    //Set original color to meshrenderers color on start
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {   
            //In idle, set material to orig color
            case EnemyState.Idle:
                meshRenderer.material.color = origColor;
                if (canNavigate)
                    enemyState = EnemyState.Patrol;
                break;
            //In notice, set material to yellow color
            case EnemyState.Notice:
                meshRenderer.material.color = Color.yellow;
                break;
            //In chase, set material to red color, and chase player
            case EnemyState.Chase:
                meshRenderer.material.color = Color.red;
                //Sets a move towards vector, Then cancels out the y value before moving the object
               Vector3 modifiedPos = new Vector3(transform.position.x, 0.5f, transform.position.z);
                transform.position = Vector3.Lerp(modifiedPos, player.transform.position, speed * Time.deltaTime);
                break;
            case EnemyState.Confused:

                meshRenderer.material.color = Color.magenta;
                isConfused = true;

                uncertaintyImage.gameObject.SetActive(true);

                confusedTimer += Time.deltaTime;

                if(confusedTimer >= confusedTime)
                {
                    confusedTimer = 0;
                    isConfused = false;
                    uncertaintyImage.gameObject.SetActive(false);

                    if (aIChaseRange.isActive)
                        enemyState = EnemyState.Chase;
                    else if (aINoticeRange.isActive)
                        enemyState = EnemyState.Notice;
                    else
                        enemyState = EnemyState.Idle;

                }
                break;
            case EnemyState.Patrol:
                meshRenderer.material.color = Color.blue;
                //Sets a move towards vector, Then cancels out the y value before moving the object
                Vector3 navPos = new Vector3(transform.position.x, 0.5f, transform.position.z);
                transform.position = Vector3.MoveTowards(navPos, navPoints[navIndex].transform.position, patrolSpeed * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NavPoint"))
        {
            navIndex++;

            if (navIndex >= navPoints.Length)
                navIndex = 0;
        }

        if (other.CompareTag("Respawn"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = navPoints[Random.Range(0, navPoints.Length)].position;
        }
    }

    public void SetConfusedState()
    {
        enemyState = EnemyState.Confused;
    }
}
