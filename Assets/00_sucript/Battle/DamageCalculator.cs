using System;

/// <summary>
/// ダメージ計算を担当するクラス
/// </summary>
public class DamageCalculator
{
    public int CalculateDamage(BattleMonster attacker, BattleMonster target, SkillData skill)
    {
        if (attacker == null)
            throw new ArgumentNullException(nameof(attacker));

        if (target == null)
            throw new ArgumentNullException(nameof(target));

        if (skill == null)
            throw new ArgumentNullException(nameof(skill));

        //威力が０の技はダメージを与えない
        if(skill.Power <= 0)
            return 0;

        //防御力が0以下の場合、0城砦を防ぐため1として扱う
        int defencse = Math.Max(1, target.Defense);

        //ダメージ計算
        long damage = (long)skill.Power * attacker.Attack / defencse;

        //威力のある攻撃は最低1ダメージを与える
        //intの最大値を超えないように調整
        return (int)Math.Min(int.MaxValue, Math.Max(1L, damage));
    }
}
