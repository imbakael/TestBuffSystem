
public class ChaProperty {

    public int hp; // ���hp
    public int strength;
    public int defence;
    public int elemStrength;
    public int elemDefence;
    public int speed;
    public int skill;
    public int luck;
    public int con; // ���

    // ���࿹�ԣ�ȡֵ��Χ��-100 ~ 90��Ҳ����˵����ܵ�200%�˺���������90%�˺�
    // ����ֻ���üӷ��������ʼ��������0��װ������20%���ԣ�buff����50%���ԣ������տ�����0 + 20 - 50 = -30
    public int physicsResist;
    public int fireResist;
    public int iceResist;
    public int thunderResist;
    public int poisonResist;
    public int lightResist;
    public int darkResist;

    // װ����buff����
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

    // ս��ʱ���������
    public int PhysicsAttack => strength + attack; // ��������
    public int PhysicsDefence => defence; // ���������
    public int ElemAttack => elemStrength + attack; // Ԫ�ع�����
    public int ElemDefence => elemDefence; // Ԫ�ط�����
    public int AttackSpeed => speed; // ����
    public int HitRate => skill * 2 + luck / 2 + hitRate; // ������
    public int DodgeRate => AttackSpeed * 2 + luck; // �����
    public int CritRate => skill / 2 + critRate; // ��ɱ��
    public int CritDodgeRate => luck; // ��ɱ�ر���

}
