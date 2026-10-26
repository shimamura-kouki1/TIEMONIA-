using System;
/// <summary>
/// 仲間にした固体の、育成や戦闘をまたいで残るデータ
/// </summary>
public class Monster
{
   public MonsterData Data { get;}
   public int Level { get; private set; }
    public int Experience { get; private set; }
    public int CurrentHp {  get; private set; }

    public MajorStatus Status { get; private set; }

    public int MaxHp => Data.MaxHp;      // 今は種族値そのまま。後でレベル補正をここに入れる
    public int Attack => Data.Attack;

    public Monster(MonsterData data,int level = 1)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
        Level = Math.Max(1,level);
        CurrentHp = MaxHp;
        Status = MajorStatus.None;
    }
    public void SetHp(int hp)
    {
        CurrentHp = Math.Clamp(hp, 0, Data.MaxHp);
    }

    public void AddExperience(int amount)
    {
        if (amount > 0)
            Experience += amount;
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
