using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Base/Passive")]
public class BasePassive : PassiveSkill
{
    public override void Apply(GameObject user)
    {
        StatsManager.instance.experienceGain *= 1.25f;
    }

    public override void Remove(GameObject user)
    {
        StatsManager.instance.experienceGain /= 1.25f;
    }
}
