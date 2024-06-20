using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINoticeRange : MonoBehaviour
{
    [SerializeField] AISwitchCase aISwitchCase; //Reference to aiswitch case which should be at parent level
    [SerializeField] AIShieldHolder aIShieldHolder; //Reference to aishield holder which should be at parent level

    //On trigger enter check for player and pass it to the ai switch case if not null, switch enemy state to notice
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (aISwitchCase != null)
            {            
                    aISwitchCase.player = other.gameObject;
                    aISwitchCase.enemyState = AISwitchCase.EnemyState.Notice;               
            }
           else if (aIShieldHolder != null)
            {               
                    aIShieldHolder.player = other.gameObject;
                    aIShieldHolder.enemyState = AIShieldHolder.EnemyState.Notice;              
            }
        }
    }

    //On trigger exit, check for player, if true, set state back to idle
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (aISwitchCase != null)
            {
                aISwitchCase.enemyState = AISwitchCase.EnemyState.Idle;
            }
          else  if (aIShieldHolder != null)
            {
                aIShieldHolder.enemyState = AIShieldHolder.EnemyState.Idle;
            }
        }
    }
}
