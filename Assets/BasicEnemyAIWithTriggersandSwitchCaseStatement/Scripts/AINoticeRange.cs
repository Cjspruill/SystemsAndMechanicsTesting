using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINoticeRange : MonoBehaviour
{
    [SerializeField] AISwitchCase aISwitchCase; //Reference to aiswitch case which should be at parent level
    [SerializeField] AIShieldHolder aIShieldHolder; //Reference to aishield holder which should be at parent level
    [SerializeField] Turret turret; //Reference to turret which is at parent level
    [SerializeField] public bool isActive; 

    //On trigger enter check for player and pass it to the ai switch case if not null, switch enemy state to notice
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;

            if (aISwitchCase != null)
            {
                if (aISwitchCase.isConfused) return;
                aISwitchCase.player = other.gameObject;
                    aISwitchCase.enemyState = AISwitchCase.EnemyState.Notice;               
            }
           else if (aIShieldHolder != null)
            {               
                    aIShieldHolder.player = other.gameObject;
                    aIShieldHolder.enemyState = AIShieldHolder.EnemyState.Notice;              
            }
            else if (turret != null)
            {
                if (turret.enemyState == Turret.EnemyState.Down) return;
                turret.Player = other.gameObject;
                turret.enemyState = Turret.EnemyState.Notice;
            }
        }
    }

    //On trigger exit, check for player, if true, set state back to idle
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = false;

            if (aISwitchCase != null)
            {
                if (aISwitchCase.isConfused) return;
                aISwitchCase.enemyState = AISwitchCase.EnemyState.Idle;
            }
          else  if (aIShieldHolder != null)
            {
                aIShieldHolder.enemyState = AIShieldHolder.EnemyState.Idle;
            }
            else if (turret != null)
            {
                if (turret.enemyState == Turret.EnemyState.Down) return;
                turret.enemyState = Turret.EnemyState.Idle;
            }
        }
    }
}
