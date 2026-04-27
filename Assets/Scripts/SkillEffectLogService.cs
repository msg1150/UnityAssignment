using UnityEngine;

public class SkillEffectLogService : MonoBehaviour
{
    public void PrintSkillLearnedLog(SkillNode skillNode)
    {
        if (skillNode.SkillData == null) return;

        Debug.Log($"[{skillNode.SkillData.SkillName}] 스킬을 찍었습니다.");

        foreach (SkillEffect effect in skillNode.SkillData.Effects)
        {
            PrintEffectLog(effect);
        }
    }

    private void PrintEffectLog(SkillEffect effect)
    {
        if (effect.Amount > 0)
        {
            Debug.Log($"{effect.StatName}이 {effect.Amount} 올랐습니다.");
        }
        else if (effect.Amount < 0)
        {
            Debug.Log($"{effect.StatName}이 {Mathf.Abs(effect.Amount)} 줄었습니다.");
        }
        else
        {
            Debug.Log($"{effect.StatName}은 변화가 없습니다.");
        }
    }
}