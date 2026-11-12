using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 1ターンのバトル結果を保持するクラス
/// </summary>
public class BattleTurnResult
{
    /// <summary>ターン中に発生したイベント一覧</summary>
    public IReadOnlyList<BattleEvent> Events { get; }

    /// <summary>バトル終了後の状態</summary>
    public BattleState State { get; }

    /// <summary>
    /// バトル結果の生成
    /// </summary>
    /// <param name="events">発生したイベント一覧</param>
    /// <param name="state">現在のバトル状態</param>
    public BattleTurnResult(
        IReadOnlyList<BattleEvent> events,
        BattleState state)
    {
        // nullチェックを行い、イベント一覧をコピーして保持する
        Events = events?.ToList().AsReadOnly()
            ?? throw new ArgumentNullException(nameof(events));
        State = state;
    }


}
