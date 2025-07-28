using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    [Header("Movement")]
    public float moveSpeed;
    public int dashSpeed;
    public int dashCooldown;
    public float dashDuration;
    public int totalDashes;
    public int numberOfDashes;

    [Header("Health")]
    public float maxHealth;
    public float currentHealth;
    public float armor; //reduce flat damage
    public float block; //% to negate damage
    public int revives;

    [Header("Combat")]
    public float baseDamage;
    public float cooldownReduction;
    public float projectileSize;
    public float critChance;
    public float critDamage;

    [Header("Energy")]
    public int stamina;

    [Header("Experience")]
    public int playerLevel;
    public float currentXp;
    public float neededXp;
    public float experienceGain;
    public float experienceRadius;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
