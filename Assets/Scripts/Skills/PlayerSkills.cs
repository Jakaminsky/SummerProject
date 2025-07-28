using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [Header("Active Skills")]
    public SkillData primaryClickSkill;
    public SkillData secondaryClickSkill;
    public SkillData qSkill;
    public SkillData eSkill;
    public SkillData rSkill;

    [Header("Passive Skill")]
    public PassiveSkill currentPassiveSkill;

    private float[] cooldownTimers = new float[5];

    private bool passiveApplied = false;
    private PassiveSkill lastAppliedPassive;

    void Update()
    {
        if (!passiveApplied && currentPassiveSkill != null)
        {
            currentPassiveSkill.Apply(gameObject);
            lastAppliedPassive = currentPassiveSkill;
            passiveApplied = true;
        }

        if (passiveApplied && currentPassiveSkill == null)
        {
            lastAppliedPassive?.Remove(gameObject);
            lastAppliedPassive = null;
            passiveApplied = false;
        }

        Debug.Log(currentPassiveSkill);

        for (int i = 0; i < cooldownTimers.Length; i++)
        {
            cooldownTimers[i] -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Mouse0)) TryUseSkill(primaryClickSkill, 0);
        if (Input.GetMouseButtonDown(1)) TryUseSkill(secondaryClickSkill, 1);
        if (Input.GetKeyDown(KeyCode.Q)) TryUseSkill(qSkill, 2);
        if (Input.GetKeyDown(KeyCode.E)) TryUseSkill(eSkill, 3);
        if (Input.GetKeyDown(KeyCode.R)) TryUseSkill(rSkill, 4);
    }

    void TryUseSkill(SkillData skill, int index)
    {
        if (skill == null || cooldownTimers[index] > 0) return;

        skill.Activate(gameObject);
        cooldownTimers[index] = skill.cooldown;
    }

}
