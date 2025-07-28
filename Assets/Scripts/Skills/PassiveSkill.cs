using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive Skill")]
public class PassiveSkill : SkillData
{
    public virtual void Apply(GameObject user) { }
    public virtual void Remove(GameObject user) { }
}
