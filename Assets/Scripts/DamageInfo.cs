using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// �չ������ܡ�����buff����newһ���˺���Ϣ���˺���Ϣ����DamageManager�����Ż�������տ�Ѫ������˺����������ô�����Ͳ�������damageInfo
public class DamageInfo {

    public ChaState attacker;
    public ChaState defender;
    public Damage damage; // �˺�ֵ�������𡢶�...
    public bool isCrit = false;
    public DamageInfoTag[] tags; // �˺����ͣ���ֱ���˺��������˺�������
    public DamageSource damageSource; // �˺���Դ���罣��ì����������
    public bool isCommonAttack; // �Ƿ�Ϊ�չ�

    // ĳЩ�˺�������Ŀ��ٷֱȵ��������/Ԫ�ط��������繥������5���������˺������˺�����Ԫ�ط��������Դ�damageInfo�� ignoreElemDefencePercent == 100
    public int ignoreDefencePercent = 0;
    public int ignoreElemDefencePercent = 0;

    public List<AddBuffInfo> addBuffs = new List<AddBuffInfo>(); // �˺������ɫ��ӵ�buff

    public DamageInfo(ChaState attacker, ChaState defender, Damage damage, DamageInfoTag[] tags, bool isCrit = false, DamageSource damageSource = DamageSource.None,
        bool isCommonAttack = false) {
        this.attacker = attacker;
        this.defender = defender;
        this.damage = damage;
        this.tags = tags;
        this.isCrit = isCrit;
        this.damageSource = damageSource;
        this.isCommonAttack = isCommonAttack;
    }

    public bool IsReflectDamge() => tags.Any(t => t == DamageInfoTag.Reflect);

}
