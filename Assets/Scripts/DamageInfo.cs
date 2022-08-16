using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �չ������ܡ�����buff����newһ���˺���Ϣ���˺���Ϣ����DamageManager�����Ż�������տ�Ѫ������˺����������ô�����Ͳ�������damageInfo
public class DamageInfo {

    public ChaState attacker;

    public ChaState defender;

    public Damage damage; // �˺�ֵ�������𡢶�...

    private DamageInfoTag[] tags; // �˺����ͣ�ֱ�ӡ���ӡ�����...

    public bool isCrit = false;

    // ĳЩ�˺�������Ŀ��ٷֱȵķ��������繥������5���������˺������˺�����Ԫ�ط��������Դ�damageInfo�� ignoreThunderDefencePercent == 100
    public int ignoreDefencePercent = 0;
    public int ignoreFireDefencePercent = 0;
    public int ignoreIceDefencePercent = 0;
    public int ignoreThunderDefencePercent = 0;
    public int ignorePoisonDefencePercent = 0;
    public int ignoreLightDefencePercent = 0;
    public int ignoreDarkDefencePercent = 0;

    public List<AddBuffInfo> addBuffs = new List<AddBuffInfo>(); // �˺������ɫ��ӵ�buff

    public DamageInfo(ChaState attacker, ChaState defender, Damage damage, DamageInfoTag[] tags, bool isCrit) {
        this.attacker = attacker;
        this.defender = defender;
        this.damage = damage;
        this.tags = tags;
        this.isCrit = isCrit;
    }

}
