
public class ChaProperty {

    public int hp; // 最大hp
    public int strength;
    public int defence;
    public int elemStrength;
    public int elemDefence;
    public int speed;
    public int skill;
    public int luck;
    public int con; // 体格

    // 各类抗性，取值范围是-100 ~ 90，也就是说最多受到200%伤害，最多减免90%伤害
    // 抗性只能用加法，比如初始物理抗性是0，装备增加20%抗性，buff减少50%抗性，则最终抗性是0 + 20 - 50 = -30
    public int physicsResist;
    public int fireResist;
    public int iceResist;
    public int thunderResist;
    public int poisonResist;
    public int lightResist;
    public int darkResist;

    // 装备或buff属性
    private int attack = 0;
    private int hitRate = 0;
    private int critRate = 0;

    public ChaProperty(int hp, int strength, int defence, int elemStrength, int elemDefence, int speed, int skill, int luck, int con,
        int physicsResist, int fireResist, int iceResist, int thunderResist, int poisonResist, int lightResist, int darkResist) {
        this.hp = hp;
        this.strength = strength;
        this.defence = defence;
        this.elemStrength = elemStrength;
        this.elemDefence = elemDefence;
        this.speed = speed;
        this.skill = skill;
        this.luck = luck;
        this.con = con;
        this.physicsResist = physicsResist;
        this.fireResist = fireResist;
        this.iceResist = iceResist;
        this.thunderResist = thunderResist;
        this.poisonResist = poisonResist;
        this.lightResist = lightResist;
        this.darkResist = darkResist;
    }

    public static ChaProperty operator +(ChaProperty a, ChaProperty b) {
        return 
            new ChaProperty(
                a.hp + b.hp,
                a.strength + b.strength,
                a.defence + b.defence,
                a.elemStrength + b.elemStrength,
                a.elemDefence + b.elemDefence,
                a.speed + b.speed,
                a.skill + b.skill,
                a.luck + b.luck,
                a.con + b.con,
                a.physicsResist + b.physicsResist,
                a.fireResist + b.fireResist,
                a.iceResist + b.iceResist,
                a.thunderResist + b.thunderResist,
                a.poisonResist + b.poisonResist,
                a.lightResist + b.lightResist,
                a.darkResist + b.darkResist
            );
    }

    // 战斗时的相关属性
    public int PhysicsAttack => strength + attack; // 物理攻击力
    public int PhysicsDefence => defence; // 物理防御力
    public int ElemAttack => elemStrength + attack; // 元素攻击力
    public int ElemDefence => elemDefence; // 元素防御力
    public int AttackSpeed => speed; // 攻速
    public int HitRate => skill * 2 + luck / 2 + hitRate; // 命中率
    public int DodgeRate => AttackSpeed * 2 + luck; // 躲避率
    public int CritRate => skill / 2 + critRate; // 必杀率
    public int CritDodgeRate => luck; // 必杀回避率

}
