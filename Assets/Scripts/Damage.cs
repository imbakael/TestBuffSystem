
/// <summary>
/// 主要通过DamageInfo来创建和修改damage，通过层层修改damage达到buff效果
/// 例如：某次伤害属于火+暗的混合伤害，{火：100，暗：50}，受击者身上buff抵挡30%火伤害，则通过此buff后damage修改为 {火：70，暗：50}
/// </summary>
public class Damage {

    public float physics;
    public float fire;
    public float ice;
    public float thunder;
    public float poison;
    public float light;
    public float dark;

    /// <summary>
    /// 真实伤害，无视防御和抗性，比如扣除当前30%生命值，此伤害即为真实伤害；又如回复10hp、回复最大生命的10%生命值，也属于真实伤害
    /// 真实伤害也参与各种加减法和乘法的计算，比如扣除当前30%生命值不受伤害减免等buff影响
    /// 真实伤害也不受必杀伤害加成
    /// </summary>
    public float real;

    public Damage(float physics = 0, float fire = 0, float ice = 0, float thunder = 0, float poison = 0,
        float light = 0, float dark = 0, float real = 0) {

        this.physics = physics;
        this.fire = fire;
        this.ice = ice;
        this.thunder = thunder;
        this.poison = poison;
        this.light = light;
        this.dark = dark;
        this.real = real;
    }

    public static Damage Zero() => new Damage();

    public static Damage operator *(Damage a, float b) {
        return
            new Damage(
                a.physics * b,
                a.fire * b,
                a.ice * b,
                a.thunder * b,
                a.poison * b,
                a.light * b,
                a.dark * b,
                a.real
            );
    }

    // todo: 考虑计算后结果为负值
    public static Damage operator +(Damage a, float b) {
        return
            new Damage(
                a.physics + b,
                a.fire + b,
                a.ice + b,
                a.thunder + b,
                a.poison + b,
                a.light + b,
                a.dark + b,
                a.real
            );
    }

    // todo: 考虑计算后结果为负值
    public static Damage operator -(Damage a, float b) {
        return
            new Damage(
                a.physics - b,
                a.fire - b,
                a.ice - b,
                a.thunder - b,
                a.poison - b,
                a.light - b,
                a.dark - b,
                a.real
            );
    }
}
