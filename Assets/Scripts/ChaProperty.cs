
public class ChaProperty {

    public int hp; // ���hp
    public int strength; // ������������������װ���ӵ�Ҳ��������û�й�����һ˵
    public int defence;
    public int elemStrength;
    public int elemDefence;
    public int speed; // ����׷��
    public int skill; // Ӱ���ɱ��
    public int luck; // ��ɱ�ر���
    public int con; // ���Ӱ���ٶ�

    // ���࿹�ԣ�ȡֵ��Χ��-100 ~ 90��Ҳ����˵����ܵ�200%�˺���������90%�˺�
    // ����ֻ���üӷ��������ʼ��������0��װ������20%���ԣ�buff����50%���ԣ������տ�����0 + 20 - 50 = -30
    public int physicsResist;
    public int fireResist;
    public int iceResist;
    public int thunderResist;
    public int poisonResist;
    public int lightResist;
    public int darkResist;

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
}
