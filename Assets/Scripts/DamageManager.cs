using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour {

    private List<DamageInfo> damageInfos = new List<DamageInfo>();

    private void Update() {
        while(damageInfos.Count > 0) {
            DealWithDamge(damageInfos[0]);
            damageInfos.RemoveAt(0);
        }
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
                buff.model.onHit?.Invoke(buff, ref damageInfo);
            }
        }
        // 2.�ܻ�������buff�����˺������罵��5�����ܵ������˺������ߴ�ܴ���ֱ�Ӱ�damage����
        for (int i = 0; i < defender.buffs.Count; i++) {
            BuffObj buff = defender.buffs[i];
            buff.model.onBeHurt?.Invoke(buff, ref damageInfo);
        }
        if (defender.CanBeKilled(damageInfo)) {
            // 3.�����߻�ɱ���˺󴥷���ɱЧ�������ɱ���˺�������һ������+2��buff
            if (attacker != null) {
                for (int i = 0; i < attacker.buffs.Count; i++) {
                    BuffObj buff = attacker.buffs[i];
                    buff.model.onKill?.Invoke(buff, damageInfo);
                }
            }
            // 4.�ܻ��������󴥷�Ч�������������Ĭ����(��������һ��debuff)
            for (int i = 0; i < defender.buffs.Count; i++) {
                BuffObj buff = defender.buffs[i];
                buff.model.onBeKilled?.Invoke(buff, damageInfo);
            }
        }
        // 5.��������damageInfo���п�Ѫ��������ʱdamageInfo�е�damagֵ�Ż�ȷ�����Ż��õ��ܻ��ߵķ����Ϳ��Խ��������˺�����
        int damage = GetDamageValue(damageInfo, defender.currentProp);
        defender.ModifyResource(new ChaResource(-damage));
        // 6.�˺�����������˫������buff
        for (int i = 0; i < damageInfo.addBuffs.Count; i++) {
            AddBuffInfo addBuffInfo = damageInfo.addBuffs[i];
            addBuffInfo.target.AddBuff(addBuffInfo);
        }
    }

    /// <summary>
    /// ���ָ��������damageInfo��ɵ��˺�ֵ
    /// </summary>
    /// <param name="damageInfo">�˺�info</param>
    /// <param name="chaProperty">��ɫ����</param>
    /// <returns></returns>
    public static int GetDamageValue(DamageInfo damageInfo, ChaProperty chaProperty) {
        Damage damage = damageInfo.damage;
        float realDefence = GetPercent(damageInfo.ignoreDefencePercent) * chaProperty.defence;
        float physics = GetPercent(chaProperty.physicsResist) * Mathf.Max(0, damage.physics - realDefence);

        float realFireDefence = GetPercent(damageInfo.ignoreFireDefencePercent) * chaProperty.elemDefence;
        float fire = GetPercent(chaProperty.fireResist) * Mathf.Max(0, damage.fire - realFireDefence);

        float realIceDefence = GetPercent(damageInfo.ignoreIceDefencePercent) * chaProperty.elemDefence;
        float ice = GetPercent(chaProperty.iceResist) * Mathf.Max(0, damage.ice - realIceDefence);

        float realThunderDefence = GetPercent(damageInfo.ignoreThunderDefencePercent) * chaProperty.elemDefence;
        float thunder = GetPercent(chaProperty.thunderResist) * Mathf.Max(0, damage.thunder - realThunderDefence);

        float realPoisonDefence = GetPercent(damageInfo.ignorePoisonDefencePercent) * chaProperty.elemDefence;
        float poison = GetPercent(chaProperty.poisonResist) * Mathf.Max(0, damage.poison - realPoisonDefence);

        float realLightDefence = GetPercent(damageInfo.ignoreLightDefencePercent) * chaProperty.elemDefence;
        float light = GetPercent(chaProperty.lightResist) * Mathf.Max(0, damage.light - realLightDefence);

        float realDarkDefence = GetPercent(damageInfo.ignoreDarkDefencePercent) * chaProperty.elemDefence;
        float dark = GetPercent(chaProperty.darkResist) * Mathf.Max(0, damage.dark - realDarkDefence);

        return Mathf.RoundToInt(physics + fire + ice + thunder + poison + light + dark + damage.real);
    }

    private static float GetPercent(int value) {
        return (100 - value) / 100f;
    }
}