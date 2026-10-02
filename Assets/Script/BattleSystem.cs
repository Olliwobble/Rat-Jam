using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform enemyBattleStation;

    Unit enemyUnit;

    public Text dialogText;

    public BattleState state;

    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyGO.GetComponent<Unit>();

        dialogText.text = "A wild" + enemyUnit.unitName + "approaches...";
    }


}

 
