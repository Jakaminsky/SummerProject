using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    [System.Serializable]
    public struct SkillSlot
    {
        public Image cooldownOverlay;
        public int skillIndex;
    }

    public PlayerSkills playerSkills;
    public SkillSlot[] skillSlots;

    private void Update()
    {
        for (int i = 0; i < skillSlots.Length; i++)
        {
            float cooldownTime = playerSkills.cooldownTimers[skillSlots[i].skillIndex];
            SkillData skillData = GetSkillData(skillSlots[i].skillIndex);
            if(skillData != null && skillData.currentCooldown > 0)
            {
                float remaining = Mathf.Max(0, cooldownTime);
                if (skillSlots[i].cooldownOverlay != null)
                {
                    skillSlots[i].cooldownOverlay.fillAmount = remaining / skillData.currentCooldown;
                }
            }
        }
    }

    private SkillData GetSkillData(int index)
    {
        switch (index)
        {
            case 0: return playerSkills.primaryClickSkill;
            case 1: return playerSkills.secondaryClickSkill;
            case 2: return playerSkills.qSkill;
            case 3: return playerSkills.eSkill;
            case 4: return playerSkills.rSkill;
            default: return null;
        }
    }

}
