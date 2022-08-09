using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo {

    private ChaState attacker;

    private ChaState defender;

    private Damage damage; // �˺�ֵ�������𡢶�...

    private DamageInfoTag[] tags; // �˺����ͣ�ֱ�ӡ���ӡ�����...

    private float critRate;

    private float hitRate;

    public List<AddBuffInfo> addBuffs = new List<AddBuffInfo>(); // �˺������ɫ��ӵ�buff

    public DamageInfo(ChaState attacker, ChaState defender, Damage damage, DamageInfoTag[] tags, float critRate, float hitRate) {
        this.attacker = attacker;
        this.defender = defender;
        this.damage = damage;
        this.tags = tags;
        this.critRate = critRate;
        this.hitRate = hitRate;
    }

    public static void DealWithDamge(DamageInfo damageInfo) {
        ChaState attacker = damageInfo.attacker;
        ChaState defender = damageInfo.defender;
        if (defender == null) {
            return;
        }
        // 1.����������buffǿ���˺�
        if (attacker != null) {
            for (int i = 0; i < attacker.buffs.Count; i++) {
                BuffObj buff = attacker.buffs[i];
                buff.buffModel.onHit?.Invoke(buff, ref damageInfo);
            }
        }
        // 2.�ܻ�������buff�����˺�
        for (int i = 0; i < defender.buffs.Count; i++) {
            BuffObj buff = defender.buffs[i];
            buff.buffModel.onBeHurt?.Invoke(buff, ref damageInfo);
        }
        if (true) { // ����ᱻɱ��
            // 3.�����߻�ɱ���˺󴥷���ɱЧ������Ӱħ��ɱ���˺���ȡ���
            if (attacker != null) {
                for (int i = 0; i < attacker.buffs.Count; i++) {
                    BuffObj buff = attacker.buffs[i];
                    buff.buffModel.onKill?.Invoke(buff, damageInfo);
                }
            }
            // 4.�ܻ��������󴥷�Ч�������������Ĭ����
            for (int i = 0; i < defender.buffs.Count; i++) {
                BuffObj buff = defender.buffs[i];
                buff.buffModel.onBeKilled(buff, damageInfo);
            }
        }
        // 5.��������damageInfo���п�Ѫ����

        // 6.�˺�����������˫�����buff
        for (int i = 0; i < damageInfo.addBuffs.Count; i++) {
            AddBuffInfo addBuffInfo = damageInfo.addBuffs[i];
            addBuffInfo.target.AddBuff(addBuffInfo);
        }
    }
}
