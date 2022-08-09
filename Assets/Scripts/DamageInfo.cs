using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo {

    private ChaState attacker;

    private ChaState defender;

    public Damage damage; // �˺�ֵ�������𡢶�...

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
        if (defender == null || defender.IsDead) {
            return;
        }
        // 1.����������buffǿ���˺�������ֹ�ʱ�˺�+30%
        if (attacker != null) {
            for (int i = 0; i < attacker.buffs.Count; i++) {
                BuffObj buff = attacker.buffs[i];
                buff.buffModel.onHit?.Invoke(buff, ref damageInfo);
            }
        }
        // 2.�ܻ�������buff�����˺������罵��5�����ܵ������˺�
        for (int i = 0; i < defender.buffs.Count; i++) {
            BuffObj buff = defender.buffs[i];
            buff.buffModel.onBeHurt?.Invoke(buff, ref damageInfo);
        }
        if (defender.CanBeKilled(damageInfo)) {
            // 3.�����߻�ɱ���˺󴥷���ɱЧ�������ɱ���˺������һ������+2��buff
            if (attacker != null) {
                for (int i = 0; i < attacker.buffs.Count; i++) {
                    BuffObj buff = attacker.buffs[i];
                    buff.buffModel.onKill?.Invoke(buff, damageInfo);
                }
            }
            // 4.�ܻ��������󴥷�Ч�������������Ĭ����(��������һ��debuff)
            for (int i = 0; i < defender.buffs.Count; i++) {
                BuffObj buff = defender.buffs[i];
                buff.buffModel.onBeKilled?.Invoke(buff, damageInfo);
            }
        }
        // 5.��������damageInfo���п�Ѫ������ֻ�д�ʱ�Ż��õ��ܻ��ߵķ����Ϳ��Խ����˺�����
        int damage = GetDamageValue(defender.currentProp, damageInfo);
        defender.ModifyResource(new ChaResource(-damage));
        // 6.�˺�����������˫�����buff
        for (int i = 0; i < damageInfo.addBuffs.Count; i++) {
            AddBuffInfo addBuffInfo = damageInfo.addBuffs[i];
            addBuffInfo.target.AddBuff(addBuffInfo);
        }
    }

    /// <summary>
    /// ���ָ��������damageInfo��ɵ��˺�ֵ
    /// </summary>
    /// <param name="chaProperty">��ɫ����</param>
    /// <param name="damageInfo">�˺�info</param>
    /// <returns></returns>
    public static int GetDamageValue(ChaProperty chaProperty, DamageInfo damageInfo) {
        Damage damage = damageInfo.damage;
        float physics = (100 - chaProperty.physicsResist) / 100f * GetPositiveValue(damage.physics, chaProperty.defence);
        float fire = (100 - chaProperty.fireResist) / 100f * GetPositiveValue(damage.fire, chaProperty.elemDefence);
        float ice = (100 - chaProperty.iceResist) / 100f * GetPositiveValue(damage.ice, chaProperty.elemDefence);
        float thunder = (100 - chaProperty.thunderResist) / 100f * GetPositiveValue(damage.thunder, chaProperty.elemDefence);
        float poison = (100 - chaProperty.poisonResist) / 100f * GetPositiveValue(damage.poison, chaProperty.elemDefence);
        float light = (100 - chaProperty.lightResist) / 100f * GetPositiveValue(damage.light, chaProperty.elemDefence);
        float dark = (100 - chaProperty.darkResist) / 100f * GetPositiveValue(damage.dark, chaProperty.elemDefence);
        return Mathf.CeilToInt(physics + fire + ice + thunder + poison + light + dark);
    }

    /// <summary>
    /// �������ֵ
    /// </summary>
    /// <param name="subtrahend">����</param>
    /// <param name="minuend">������</param>
    /// <returns></returns>
    private static int GetPositiveValue(int subtrahend, int minuend) {
        int delta = subtrahend - minuend;
        return delta > 0 ? delta : 0;
    }
}
