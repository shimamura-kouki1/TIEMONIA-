using System;
using System.Collections.Generic;

/// <summary>
/// スキルの実行を担当するクラス
/// </summary>
public class SkillExecutor
{
    private readonly DamageCalculator _damageCalculator;
    private readonly Random _random;

    /// SkillExecutorの初期化
    /// </summary>
    public SkillExecutor(DamageCalculator damageCalculator)
        : this(damageCalculator, new Random())
    {
    }

    /// <summary>
    /// 乱数を外部から設定できる初期化 
    /// </summary>
    /// <param name="damageCalculator"></param>
    /// <param name="random"></param>
    public SkillExecutor(DamageCalculator damageCalculator, Random random)
    {
        _damageCalculator = damageCalculator
                   ?? throw new ArgumentNullException(nameof(damageCalculator));

        _random = random
            ?? throw new ArgumentNullException(nameof(random));
    }

    /// <summary>
    /// 技の実行
    /// </summary>
    /// <param name="action">実行する行動情報</param>
    /// <param name="events">発生したイベントを追加するリスト</param>
    public void ExecutesSkill(BattleAction action,IList<BattleEvent>events)
    {
        if (action == null) throw new ArgumentException(nameof(action));
        if (events == null) throw new ArgumentException(nameof(events));

        //技が使用できるかの確認
        if (action.Type != ActionType.Attack) throw new ArgumentException("技を使用する行動ではありません", nameof(action));

        if(action.Skill == null) throw new ArgumentException("使用する技が設定されていません", nameof(action));

        BattleMonster attacker = action.User;

        //行動するモンスターが倒れている場合実行しない
        if (attacker.IsFainted) return;

        //技を使用した場合イベントを追加
        events.Add(new SkillUsedEvent(attacker, action.Skill));

        //対象ごとに命中判定とダメージ判定を行う
        foreach(BattleMonster target in action.Targets)
        {
            if (target == null || target.IsFainted) continue;

            //命中判定
            if(!CheckHit(action.Skill))
            {
                events.Add(new SkillMissedEvent(attacker, action.Skill, target));
                continue;
            }

            //ダメージ計算
            int calculatedDamage = _damageCalculator.CalculateDamage(attacker, target, action.Skill);

            //ダメージを受ける前の㏋を記録
            int beforeHp = target.CurrentHp;

            //ＨＰを減らす
            target.TakeDamage(calculatedDamage);

            //ダメージを受けた分を記録
            int afterHp = target.CurrentHp;

            //実際に減ったHPを記録
            int actualDamage = beforeHp - afterHp;

            //ダメージイベントの登録
            events.Add(new DamageEvent(target, actualDamage, beforeHp, afterHp));

            //HPがゼロの場合戦闘不能イベントを追加
            if(beforeHp > 0 && target.IsFainted)
            {
                events.Add(new MonsterFaintedEvent(target));
            }
        }
    }

    /// <summary>
    /// 技の命中率
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    private bool CheckHit(SkillData skill)
    {
        //ランダムな値を取得
        int randomValue = _random.Next(0, 100);

        //命中率よりも小さければ命中
        return randomValue < skill.Accuracy;
    }
}
