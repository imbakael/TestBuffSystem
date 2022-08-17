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

    // ���뵽�˷����ڲ���˵����damageInfo�ض�û�б����ܣ����������������ж�
    public static void DealWithDamge(DamageInfo damageInfo) {
        ChaState attacker = damageInfo.attacker;
        ChaState defender = damageInfo.defender;
        if (defender == null || defender.IsDead) {
            return;
        }

        // 1.����buff�������޸�damageInfo
        if (attacker != null) {
            for (int i = 0; i < attacker.buffs.Count; i++) {
                BuffObj buff = attacker.buffs[i];
                buff.model.onHit?.Invoke(buff, ref damageInfo);
            }
        }
        for (int i = 0; i < defender.buffs.Count; i++) {
            BuffObj buff = defender.buffs[i];
            buff.model.onBeHurt?.Invoke(buff, ref damageInfo);
        }
        if (defender.CanBeKilled(damageInfo)) {
            if (attacker != null) {
                for (int i = 0; i < attacker.buffs.Count; i++) {
                    BuffObj buff = attacker.buffs[i];
                    buff.model.onKill?.Invoke(buff, damageInfo);
                }
            }
            for (int i = 0; i < defender.buffs.Count; i++) {
                BuffObj buff = defender.buffs[i];
                buff.model.onBeKilled?.Invoke(buff, damageInfo);
            }
        }

        // 2.��������damageInfo���п�Ѫ��������ʱdamageInfo�е�damagֵ�Ż�ȷ�����Ż��õ��ܻ��ߵķ����Ϳ��Խ��������˺�����
        int damage = GetDamageValue(damageInfo, defender.currentProp);
        defender.ModifyResource(new ChaResource(-damage));

        // 3.�˺�����������˫�����buff
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
        bool isCrit = damageInfo.isCrit;
        float realDefence = chaProperty.defence * GetPercent(damageInfo.ignoreDefencePercent);
        float physics = Mathf.Max(0, GetCritValue(damage.physics - realDefence, isCrit)) * GetPercent(chaProperty.physicsResist);

        float realFireDefence = chaProperty.elemDefence * GetPercent(damageInfo.ignoreFireDefencePercent);
        float fire = Mathf.Max(0, GetCritValue(damage.fire - realFireDefence, isCrit)) * GetPercent(chaProperty.fireResist);

        float realIceDefence = chaProperty.elemDefence * GetPercent(damageInfo.ignoreIceDefencePercent);
        float ice =  Mathf.Max(0, GetCritValue(damage.ice - realIceDefence, isCrit)) * GetPercent(chaProperty.iceResist);

        float realThunderDefence = chaProperty.elemDefence * GetPercent(damageInfo.ignoreThunderDefencePercent);
        float thunder = Mathf.Max(0, GetCritValue(damage.thunder - realThunderDefence, isCrit)) * GetPercent(chaProperty.thunderResist);

        float realPoisonDefence = chaProperty.elemDefence * GetPercent(damageInfo.ignorePoisonDefencePercent);
        float poison = Mathf.Max(0, GetCritValue(damage.poison - realPoisonDefence, isCrit)) * GetPercent(chaProperty.poisonResist);

        float realLightDefence = chaProperty.elemDefence * GetPercent(damageInfo.ignoreLightDefencePercent);
        float light = Mathf.Max(0, GetCritValue(damage.light - realLightDefence, isCrit)) * GetPercent(chaProperty.lightResist);

        float realDarkDefence = chaProperty.elemDefence * GetPercent(damageInfo.ignoreDarkDefencePercent);
        float dark = Mathf.Max(0, GetCritValue(damage.dark - realDarkDefence, isCrit)) * GetPercent(chaProperty.darkResist);

        return Mathf.RoundToInt(physics + fire + ice + thunder + poison + light + dark + GetCritValue(damage.real, isCrit));
    }

    private static float GetPercent(int value) {
        return (100 - value) / 100f;
    }

    private static float GetCritValue(float value, bool isCrit) {
        return isCrit ? value * 3f : value;
    }
}
