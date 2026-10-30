using System;

/// <summary>
/// 戦闘中に状態異常の処理を実行する
/// 状態異常分処理を追加する
/// </summary>
public sealed class StatusEffectProcessor
{
    /// <summary>
    /// 行動前に状態異常による行動制限を確認する
    /// </summary>
    public bool CanAct(BattleMonster monster)
    {
        // TODO: 麻痺・眠り・凍結などの判定
        return true;
    }

    /// <summary>ターン終了時に状態異常の処理をする </summary>
    public void ProcessEndTurn(BattleMonster monster)
    {
        if (monster == null || monster.IsFainted) return;

        switch (monster.MajorStatus)
        {
            case MajorStatus.Poison:
                Poison(monster);
                break;

            case MajorStatus.Burn:
                Burn(monster);
                break;
        }
    }

    /// <summary>
    /// 毒状態の処理
    /// </summary>
    /// <param name="monster"></param>
    private void Poison(BattleMonster monster)
    {
        //毎ターン最大体力の10/1を減らす
        int damage = Math.Max(1, monster.MaxHp / 10);
        monster.TakeDamage(damage);
    }

    /// <summary>やけど状態の処理</summary>
    private void Burn(BattleMonster monster)
    {
        // TODO: やけどのダメージ処理
    }
}
