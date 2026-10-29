using System;
/// <summary>
/// 仲間にした固体の、育成や戦闘をまたいで残るデータ
/// </summary>
public class Monster
{
    public MonsterData Data { get; }
    public int Level { get; private set; }
    public int Experience { get; private set; }
    public int CurrentHp { get; private set; }

    public MajorStatus Status { get; private set; }

    public int MaxHp => Data.CalculateMaxHP(Level);      // 今は種族値そのまま。後でレベル補正をここに入れる
    public int Attack => Data.CalculateAttack(Level);
    public int Defense => Data.CalculateDefense(Level);
    public int Speed => Data.CalculateSpeed(Level);
    public string Name => Data.MonsterName;

    public Monster(MonsterData data, int level = 1)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
        Level = Math.Max(1, level);
        CurrentHp = MaxHp;
        Status = MajorStatus.None;
    }

    public void SetHp(int hp)
    {
        CurrentHp = Math.Clamp(hp, 0, Data.CalculateMaxHP(Level));
    }

    /// <summary>最大HPの増加分だけ現在HPを増やす</summary>
    public void SetLevel(int level)
    {
        int oldMaxHp = MaxHp;

        level = Math.Max(1, level);

        int newMaxHp = MaxHp;
        int hpIncrease = newMaxHp - oldMaxHp;

        CurrentHp = Math.Clamp(CurrentHp + hpIncrease, 0, newMaxHp);
    }

    //public void AddExperience(int amount)
    //{
    //    if (amount > 0)
    //        Experience += amount;
    //}

    /// <summary>
    /// 経験値を獲得し、必要ならレベルアップする
    /// </summary>
    /// <param name="amount">レベルアップした回数</param>
    /// <returns></returns>
    public int AddExperience(int amount)
    {
        if (amount <= 0) return 0;

        Experience += amount;

        int levelUpCount = 0;
        const int maxLevel = 100;

        while(Level < maxLevel && Experience >= Level * 100)
        {
            Level++;
            levelUpCount++;
        }
        return levelUpCount;
    }


    public void SetStatus(MajorStatus status)
    {
        Status = status;
    }

    public void CureStatus()
    {
        Status = MajorStatus.None;
    }
}
