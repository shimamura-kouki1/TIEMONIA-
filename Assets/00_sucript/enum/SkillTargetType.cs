
/// <summary>
/// 技の対象範囲。
/// 将来的な複数体戦闘にも対応できるようにする。
/// </summary>
public enum SkillTargetType
{
    SingleEnemy,  // 敵1体
    SingleAlly,   // 味方1体
    AllEnemies,   // 敵全体
    AllAllies,    // 味方全体
    Self          // 自分自身
}
