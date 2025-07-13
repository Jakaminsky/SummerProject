using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public float cooldown;
    public float damage;
    public Sprite icon;

    public virtual void Activate (GameObject user)
    {
        Debug.Log($"{user.name} used {skillName}");
    }
}
