using UnityEngine;
using UnityEngine.UI;

public class FighterStats : MonoBehaviour
{
    public string fighterName;
    public int attackPower;
    public int maxHP;
    public int currentHP;

    [Header("UI Reference")]
    [Tooltip("Drag the health slider corresponding to this character here.")]
    public Slider hpSlider;

    [Header("Combat Text Config")]
    [Tooltip("Drag your floating text canvas prefab here.")]
    public GameObject floatingTextPrefab;

    [HideInInspector] public bool isDefending = false;
    [HideInInspector] public bool isDodging = false;

    void Start()
    {
        currentHP = maxHP;
        UpdateHPBar();
    }

    public bool TakeDamage(int incomingDamage)
    {
        // 1. Process Dodge Stance
        if (isDodging && Random.value < 0.50f)
        {
            SpawnFloatingText("DODGED!", Color.cyan);
            return false;
        }

        // 2. Process Defend Stance
        if (isDefending)
        {
            incomingDamage /= 2;
            SpawnFloatingText($"-{incomingDamage} (Blocked)", Color.yellow);
        }
        else
        {
            SpawnFloatingText($"-{incomingDamage}", Color.red);
        }

        currentHP -= incomingDamage;
        if (currentHP <= 0) currentHP = 0;

        UpdateHPBar();
        return currentHP <= 0;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;

        UpdateHPBar();
        SpawnFloatingText($"+{amount} HP", Color.green);
    }

    public void UpdateHPBar()
    {
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }
    }

    public void SpawnFloatingText(string text, Color color)
    {
        if (floatingTextPrefab != null)
        {
            GameObject txtObj = Instantiate(floatingTextPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            FloatingText script = txtObj.GetComponentInChildren<FloatingText>();
            if (script != null)
            {
                script.Setup(text, color);
            }
        }
    }
}

