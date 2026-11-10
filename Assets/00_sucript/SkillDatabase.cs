using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

/// <summary>
/// CSVの読み込み・変換・検索を行う
/// </summary>
public class SkillDatabase : MonoBehaviour
{
    [Header("技データ")]
    [SerializeField] private TextAsset _skillCsv;

    //SkillIDをキーにして技データの管理をする。
    private readonly Dictionary<int, SkillData> _skills = new();

    private void Awake()
    {
        LoadSkill();
    }

    /// <summary>
    /// スキルＣＳＶを読み込む
    /// </summary>
    private void LoadSkill()
    {
        _skills.Clear();

        if (_skillCsv == null)
        {
            Debug.Log("Skillデータがありません。", this);
            return;
        }

        string[] lines = _skillCsv.text.TrimStart('\uFEFF').Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length < 2)
        {
            Debug.LogError("Skill.csvに技データがありません", this);
            return;
        }

        //CSVのヘッダーを確認する
        string expectedHeader = "SkillId,Name,Power,Accuracy,Priority,Element,TargetType";

        if (lines[0].Trim() != expectedHeader)
        {
            Debug.LogError("Skills.csvのヘッダーが正しくありません。", this);
            return;
        }

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            // 列数が正しいか確認する。
            if (values.Length != 7)
            {
                Debug.LogError(
                    $"Skills.csvの{i + 1}行目の列数が正しくありません。", this);
                continue;
            }

            // CSVの文字列を各データ型に変換する。
            if (!int.TryParse(values[0], NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out int skillId) ||
                !int.TryParse(values[2], NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out int power) ||
                !int.TryParse(values[3], NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out int accuracy) ||
                !int.TryParse(values[4], NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out int priority))
            {
                Debug.LogError(
                    $"Skills.csvの{i + 1}行目に数値の不正があります。", this);
                continue;
            }

            // Enumの文字列を対応する値に変換する。
            if (!Enum.TryParse(values[5], true, out ElementType element) ||
                !Enum.IsDefined(typeof(ElementType), element))
            {
                Debug.LogError(
                    $"Skills.csvの{i + 1}行目の属性が正しくありません。", this);
                continue;
            }

            if (!Enum.TryParse(values[6], true, out SkillTargetType targetType) ||
                !Enum.IsDefined(typeof(SkillTargetType), targetType))
            {
                Debug.LogError(
                    $"Skills.csvの{i + 1}行目の対象が正しくありません。", this);
                continue;
            }

            // 技データを生成する。
            try
            {
                SkillData skill = new SkillData(
                    skillId,
                    values[1].Trim(),
                    power,
                    accuracy,
                    priority,
                    element,
                    targetType
                );


                // IDの重複を確認して登録する。
                if (_skills.ContainsKey(skillId))
                {
                    Debug.LogError(
                        $"SkillId {skillId} が重複しています。", this);
                    continue;
                }

                _skills.Add(skillId, skill);
            }
            catch (ArgumentException exception)
            {
                Debug.LogError(
                    $"Skills.csvの{i + 1}行目が不正です。\n{exception.Message}", this);
            }
        }
        Debug.Log($"技データを {_skills.Count} 件読み込みました。", this);
    }

    /// <summary>
    /// SkillIdから技のデータを取得する
    /// </summary>
    /// <param name="skillId"></param>
    /// <returns></returns>
    public SkillData GetSkill(int skillId)
    {
        if (_skills.TryGetValue(skillId, out SkillData skill))
        {
            return skill;
        }
        Debug.LogWarning($"SkillId{skillId}の技が見つかりません", this);
        return null;
    }

    /// <summary>
    /// skillIdから技のデータを取得できたかを判定する。
    /// <returns></returns>
    public bool TryGetSkill(int skillId,out SkillData skill)
    {
        return _skills.TryGetValue(skillId,out skill);
    }
}

