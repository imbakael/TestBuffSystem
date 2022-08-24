
/// <summary>
/// ��Ҫͨ��DamageInfo���������޸�damage��ͨ������޸�damage�ﵽbuffЧ��
/// ���磺ĳ���˺����ڻ�+���Ļ���˺���{��100������50}���ܻ�������buff�ֵ�30%���˺�����ͨ����buff��damage�޸�Ϊ {��70������50}
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
    /// ��ʵ�˺������ӷ����Ϳ��ԣ�����۳���ǰ30%����ֵ�����˺���Ϊ��ʵ�˺�������ظ�10hp���ظ����������10%����ֵ��Ҳ������ʵ�˺�
    /// ��ʵ�˺�Ҳ������ּӼ����ͳ˷��ļ��㣬����۳���ǰ30%����ֵ�����˺������buffӰ��
    /// ��ʵ�˺�Ҳ���ܱ�ɱ�˺��ӳ�
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

    // todo: ���Ǽ������Ϊ��ֵ
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

    // todo: ���Ǽ������Ϊ��ֵ
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
