using UnityEngine;

public class BattleDataManager : MonoBehaviour
{
    public static BattleDataManager Instance { get; private set; }

    [Header("Persistent Overworld Data")]
    public int globalPotionCount = 5; // Starting items

    [HideInInspector] public string StoredName;
    [HideInInspector] public int StoredMaxHP;
    [HideInInspector] public int StoredAttack;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveEnemyData(string name, int hp, int attack)
    {
        StoredName = name;
        StoredMaxHP = hp;
        StoredAttack = attack;
    }
}
