using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public Sprite icon;

    public float cooldown;

    public virtual void Activate (GameObject user){ }
}
