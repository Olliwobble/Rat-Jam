using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
      

        dialogText.text = "A wild" + enemyUnit.unitName + "approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        yield return new WaitForSeconds(2f);
        
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
         
        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        } else
        {
            state = BattleState.ENEMYTURN;
            
        }

    }
    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogText.text = "You won the battle!";
        } else if (state == BattleState.LOST)
        {
            dialogText.text = "You were defeated.";
        }

    }
    void PlayerTurn()
    {
        dialogText.text = "Choose an action";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

}

 
