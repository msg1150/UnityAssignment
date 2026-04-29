using UnityEngine;

public class SkillUnlockService : MonoBehaviour
{
    public void UnlockLinkedSkills(SkillNode learnedSkill, bool printLog = true)
    {
        foreach (SkillNode linkedSkill in learnedSkill.LinkedSkills)
        {
            if (linkedSkill == null) continue;

            linkedSkill.SetAvailable();

            if (printLog && linkedSkill.SkillData != null)
            {
                Debug.Log($"{linkedSkill.SkillData.SkillName} 스킬이 열렸습니다.");
            }
        }
    }
}