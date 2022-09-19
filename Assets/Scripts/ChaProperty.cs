
public class ChaProperty {

    // ս��ʱ���������
    public int PhysicsAttack => strength + attack; // ��������
    public int PhysicsDefence => defence; // ���������
    public int ElemAttack => elemStrength + attack; // Ԫ�ع�����
    public int ElemDefence => elemDefence; // Ԫ�ط�����
    public int CritRate => skill / 2 + critRate; // ��ɱ��
    public int CritDodgeRate => luck; // ��ɱ�ر���

    public int hp; // ���hp
    public int strength;
    public int defence;
    public int elemStrength;
    public int elemDefence;
    public int speed;
    public int skill;
    public int luck;

    // ���࿹�ԣ�ȡֵ��Χ��-200 ~ 95��Ҳ����˵����ܵ�300%�˺���������95%�˺�
    // ����ֻ���üӷ��������ʼ��������0��װ������20%���ԣ�buff����50%���ԣ������տ�����0 + 20 - 50 = -30
    public int physicsResist; // >=50%ʱ���߹ᴩ����Ѫ
    public int fireResist; // >=50%ʱ�������ա���ȼ
    public int iceResist; // >=50%ʱ���ߺ��䡢����
    public int thunderResist; // >=50%ʱ�������
    public int poisonResist; // >=50%�����ж����綾������
    public int lightResist; // >=50%ʱ�����ѪЧ������30%
    public int darkResist; // >=50%ʱ�����˺�����30%

    // װ����buff����
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
