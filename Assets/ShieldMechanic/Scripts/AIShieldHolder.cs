using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShieldHolder : MonoBehaviour
{
    [SerializeField]public enum EnemyState  //List of all possible enemy states
    {
        Idle,
        Notice,
        Chase,
    }
    [SerializeField] public EnemyState enemyState;  //Current enemy state
    [SerializeField]public GameObject player; //Used to track and move around player
    [SerializeField] MeshRenderer meshRenderer; //Used to change color of the gamobject
    [SerializeField] Color origColor;   //Used to set the original color in idle state

    [SerializeField] float speed;   //Movement speed of this object
    [SerializeField] float rotationSpeed;   //Rotation speed of this object

    [SerializeField] Shield shield; //Reference to held shield

    [SerializeField] AIChaseRange aIChaseRange; //Reference for sphere collider size needed
    [SerializeField] int moveDirection; //Used to randomize a move direction while close to target
    [SerializeField] float moveTimer;   //Timer that increments in chase state
    [SerializeField] float moveTimerMin;   //Min move timer value
    [SerializeField] float moveTimerMax;   //Max move timer value
    [SerializeField] float moveTime;    //How much time needed before changing move direction

    [SerializeField] float shieldTimer; //Counts time for when shield should be raised and lowered
    [SerializeField] float shieldTimerMin;  //Min shield timer
    [SerializeField] float shieldTimerMax;  //Max shield timer
    [SerializeField] float shieldTime; //How much time needed before chaning shield raised or lowered
    [SerializeField] bool shieldRaised; //Bool for if the shield is currently raised


    // Start is called before the first frame update
    void Start()
    {
        origColor = meshRenderer.material.color;    //Setting orig color on start
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
                break;
            //In notice, set material to yellow color
            case EnemyState.Notice:
                meshRenderer.material.color = Color.yellow;
                //Set object rotation to look towards player
                Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
                break;
            //In chase, set material to red color, and chase player
            case EnemyState.Chase:
                meshRenderer.material.color = Color.red;
                //Set object rotation to look towards player
                Quaternion lookRotation2 = Quaternion.LookRotation(player.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation2, rotationSpeed * Time.deltaTime);

                //Increment move timer by time.deltatime
                moveTimer += Time.deltaTime;

                //If move timer is greater or equal to move time
                if (moveTimer >= moveTime)
                {
                    //Randomize move time and randomize move direction
                    moveTimer = 0;
                    moveTime = Random.Range(moveTimerMin, moveTimerMax);
                    moveDirection = Random.Range(0, 2);
                }


                //If move direction is 0 move left
                if (moveDirection == 0)
                {
                    MoveLeft();
                }
                //If move direction is 1 move right
                else if (moveDirection == 1)
                {
                    MoveRight();
                }


                //Increment shield timer
                shieldTimer += Time.deltaTime;

                //If shield timer is greater or equal to shield time, reset shield timer, randomize shield timer and flip shield raised
                if (shieldTimer >= shieldTime)
                {
                    shieldTimer = 0;
                    shieldTime = Random.Range(shieldTimerMin, shieldTimerMax);
                    shieldRaised = !shieldRaised;
                }

                //Is shield raised is true
                if (shieldRaised)
                {
                    //Raise Shield
                    shield.RaiseShield();
                }
                //If shield raised is false
                else if (!shieldRaised)
                {
                    //Lower shield
                    shield.LowerShield();
                }

                break;
            default:
                break;
        }
    }

    //Move transform to the left
    void MoveLeft()
    {
        Vector3 movePosition = -transform.right;
        transform.position += movePosition * speed * Time.deltaTime;
    }

    //Move tranform to the right
    void MoveRight()
    {
        Vector3 movePosition = transform.right;
        transform.position += movePosition * speed * Time.deltaTime;
    }
}
