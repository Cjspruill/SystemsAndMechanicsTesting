using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISwitchCase : MonoBehaviour
{

    [SerializeField] public enum EnemyState //Enemy state enum that holds all possible states
    {
        Idle,
        Notice,
        Chase,
    }
    [SerializeField] float speed;   //Movement speed of this object
    [SerializeField] public EnemyState enemyState; //Enemy state variable that holds current state
    [SerializeField] public GameObject player;  //Player reference to have player follow
    [SerializeField] MeshRenderer meshRenderer; //Mesh renderer reference to change material color
    [SerializeField] Color origColor;   //Original color reference set on start

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;   //Set enemy state to idle
        origColor = meshRenderer.material.color;    //Set original color to meshrenderers color on start
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
                break;
            //In chase, set material to red color, and chase player
            case EnemyState.Chase:
                meshRenderer.material.color = Color.red;
                //Sets a move towards vector, Then cancels out the y value before moving the object
               Vector3 modifiedPos = new Vector3(transform.position.x, 0.5f, transform.position.z);
                transform.position = Vector3.Lerp(modifiedPos, player.transform.position, speed * Time.deltaTime);
                break;
            default:
                break;
        }
    }
}
