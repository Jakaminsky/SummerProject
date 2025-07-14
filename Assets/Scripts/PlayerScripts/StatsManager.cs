using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    [Header("Movement")]
    public int moveSpeed;
    public int dashSpeed;
    public int dashCooldown;
    public float dashDuration;
    public int totalDashes;
    public int numberOfDashes;

    [Header("Health")]
    public float maxHealth;
    public float currentHealth;
    public int armor; //reduce flat damage
    public int block; //% to negate damage
    public int revives;

    [Header("Combat")]
    public int baseDamage;
    public int cooldownReduction;
    public int projectileSize;
    public int critChance;
    public float critDamage;

    [Header("Energy")]
    public int stamina;

    [Header("Experience")]
    public int playerLevel;
    public float currentXp;
    public float neededXp;
    public int experienceGain;
    public int experienceRadius;

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
