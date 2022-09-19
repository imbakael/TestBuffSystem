
public class ChaProperty {

    // 战斗时的相关属性
    public int PhysicsAttack => strength + attack; // 物理攻击力
    public int PhysicsDefence => defence; // 物理防御力
    public int ElemAttack => elemStrength + attack; // 元素攻击力
    public int ElemDefence => elemDefence; // 元素防御力
    public int CritRate => skill / 2 + critRate; // 必杀率
    public int CritDodgeRate => luck; // 必杀回避率

    public int hp; // 最大hp
    public int strength;
    public int defence;
    public int elemStrength;
    public int elemDefence;
    public int speed;
    public int skill;
    public int luck;

    // 各类抗性，取值范围是-200 ~ 95，也就是说最多受到300%伤害，最多减免95%伤害
    // 抗性只能用加法，比如初始物理抗性是0，装备增加20%抗性，buff减少50%抗性，则最终抗性是0 + 20 - 50 = -30
    public int physicsResist; // >=50%时免疫贯穿、流血
    public int fireResist; // >=50%时免疫灼烧、点燃
    public int iceResist; // >=50%时免疫寒冷、冰冻
    public int thunderResist; // >=50%时免疫麻痹
    public int poisonResist; // >=50%免疫中毒、剧毒、瘟疫
    public int lightResist; // >=50%时任意回血效果提升30%
    public int darkResist; // >=50%时最终伤害提升30%

    // 装备或buff属性
    private int attack = 0;
    private int critRate = 0;

    public ChaProperty(int hp, int strength, int defence, int elemStrength, int elemDefence, int speed, int skill, int luck,
        int physicsResist, int fireResist, int iceResist, int thunderResist, int poisonResist, int lightResist, int darkResist) {
        this.hp = hp;
        this.strength = strength;
        this.defence = defence;
        this.elemStrength = elemStrength;
        this.elemDefence = elemDefence;
        this.speed = speed;
        this.skill = skill;
        this.luck = luck;
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
                a.physicsResist + b.physicsResist,
                a.fireResist + b.fireResist,
                a.iceResist + b.iceResist,
                a.thunderResist + b.thunderResist,
                a.poisonResist + b.poisonResist,
                a.lightResist + b.lightResist,
                a.darkResist + b.darkResist
            );
    }

    public static ChaProperty operator *(ChaProperty a, int b) {
        return
            new ChaProperty(
                a.hp * b,
                a.strength * b,
                a.defence * b,
                a.elemStrength * b,
                a.elemDefence * b,
                a.speed * b,
                a.skill * b,
                a.luck * b,
                a.physicsResist * b,
                a.fireResist * b,
                a.iceResist * b,
                a.thunderResist * b,
                a.poisonResist * b,
                a.lightResist * b,
                a.darkResist * b
            );
    }

    public static ChaProperty NewZero() => new ChaProperty(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

    public void Zero() {
        hp = 0;
        strength = 0;
        defence = 0;
        elemStrength = 0;
        elemDefence = 0;
        speed = 0;
        skill = 0;
        luck = 0;
        physicsResist = 0;
        fireResist = 0;
        iceResist = 0;
        thunderResist = 0;
        poisonResist = 0;
        lightResist = 0;
        darkResist = 0;
    }
}
