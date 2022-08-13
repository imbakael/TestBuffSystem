using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo {

    public ChaState attacker;

    public ChaState defender;

    public Damage damage; // �˺�ֵ�������𡢶�...

    private DamageInfoTag[] tags; // �˺����ͣ�ֱ�ӡ���ӡ�����...

    private float critRate;

    private float hitRate;

    // ĳЩ�˺�������Ŀ��ٷֱȵķ��������繥������5���������˺������˺�����Ԫ�ط��������Դ�damageInfo�� ignoreThunderDefencePercent == 100
    public int ignoreDefencePercent = 0;
    public int ignoreFireDefencePercent = 0;
    public int ignoreIceDefencePercent = 0;
    public int ignoreThunderDefencePercent = 0;
    public int ignorePoisonDefencePercent = 0;
    public int ignoreLightDefencePercent = 0;
    public int ignoreDarkDefencePercent = 0;

    public List<AddBuffInfo> addBuffs = new List<AddBuffInfo>(); // �˺������ɫ��ӵ�buff

    public DamageInfo(ChaState attacker, ChaState defender, Damage damage, DamageInfoTag[] tags, float critRate, float hitRate) {
        this.attacker = attacker;
        this.defender = defender;
        this.damage = damage;
        this.tags = tags;
        this.critRate = critRate;
        this.hitRate = hitRate;
    }

}
