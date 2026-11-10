
using UnityEngine;

/// <summary>
/// SkillDatabaseの動作確認用クラス。
/// </summary>
public class SkillDatabaseTester : MonoBehaviour
{
    [SerializeField] private SkillDatabase _skillDatabase;

    private void Start()
    {
        for (int i = 1; i < 5; i++)
        {
            // SkillIdが1の技を取得する。
            SkillData skill = _skillDatabase.GetSkill(i);

            if (skill == null) return;

            Debug.Log($"技名：{skill.Name}");
            Debug.Log($"威力：{skill.Power}");
            Debug.Log($"命中率：{skill.Accuracy}");
            Debug.Log($"属性：{skill.Element}");
        }
    }
}
