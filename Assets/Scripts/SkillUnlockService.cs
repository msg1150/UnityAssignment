using UnityEngine;

public class SkillUnlockService : MonoBehaviour
{
    public void UnlockLinkedSkills(SkillNode learnedSkill)
    {
        foreach (SkillNode linkedSkill in learnedSkill.LinkedSkills)
        {
            if (linkedSkill == null) continue;

            linkedSkill.SetAvailable();

            if (linkedSkill.SkillData != null)
            {
                Debug.Log($"{linkedSkill.SkillData.SkillName} 스킬이 열렸습니다.");
            }
        }
    }
}