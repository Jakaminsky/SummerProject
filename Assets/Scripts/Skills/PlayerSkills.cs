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
    public PassiveSkill passiveSkill;

    private float[] cooldownTimers = new float[5];

    void Start()
    {
        passiveSkill?.Apply(gameObject);
    }

    void Update()
    {
        for (int i = 0; i < cooldownTimers.Length; i++)
            cooldownTimers[i] -= Time.deltaTime;

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
