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

        // 1.部分buff触发会修改damageInfo
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

        // 2.根据最后的damageInfo进行扣血操作，此时damageInfo中的damag值才会确定，才会用到受击者的防御和抗性进行最终伤害计算
        int damage = GetDamageValue(damageInfo, defender.currentProp);
        defender.ModifyResource(new ChaResource(-damage));

        // 3.伤害流程走完后对双方添加buff
        for (int i = 0; i < damageInfo.addBuffs.Count; i++) {
            AddBuffInfo addBuffInfo = damageInfo.addBuffs[i];
            addBuffInfo.target.AddBuff(addBuffInfo);
        }
    }

    /// <summary>
    /// 获得指定属性下damageInfo造成的伤害值
    /// </summary>
    /// <param name="damageInfo">伤害info</param>
    /// <param name="chaProperty">角色属性</param>
    /// <returns></returns>
    public static int GetDamageValue(DamageInfo damageInfo, ChaProperty chaProperty) {
        Damage damage = damageInfo.damage;
        bool isCrit = damageInfo.isCrit;
        float realDefence = GetPercent(damageInfo.ignoreDefencePercent) * chaProperty.defence;
        float physics = GetPercent(chaProperty.physicsResist) * Mathf.Max(0, GetCritValue(damage.physics - realDefence, isCrit));

        float realFireDefence = GetPercent(damageInfo.ignoreFireDefencePercent) * chaProperty.elemDefence;
        float fire = GetPercent(chaProperty.fireResist) * Mathf.Max(0, GetCritValue(damage.fire - realFireDefence, isCrit));

        float realIceDefence = GetPercent(damageInfo.ignoreIceDefencePercent) * chaProperty.elemDefence;
        float ice = GetPercent(chaProperty.iceResist) * Mathf.Max(0, GetCritValue(damage.ice - realIceDefence, isCrit));

        float realThunderDefence = GetPercent(damageInfo.ignoreThunderDefencePercent) * chaProperty.elemDefence;
        float thunder = GetPercent(chaProperty.thunderResist) * Mathf.Max(0, GetCritValue(damage.thunder - realThunderDefence, isCrit));

        float realPoisonDefence = GetPercent(damageInfo.ignorePoisonDefencePercent) * chaProperty.elemDefence;
        float poison = GetPercent(chaProperty.poisonResist) * Mathf.Max(0, GetCritValue(damage.poison - realPoisonDefence, isCrit));

        float realLightDefence = GetPercent(damageInfo.ignoreLightDefencePercent) * chaProperty.elemDefence;
        float light = GetPercent(chaProperty.lightResist) * Mathf.Max(0, GetCritValue(damage.light - realLightDefence, isCrit));

        float realDarkDefence = GetPercent(damageInfo.ignoreDarkDefencePercent) * chaProperty.elemDefence;
        float dark = GetPercent(chaProperty.darkResist) * Mathf.Max(0, GetCritValue(damage.dark - realDarkDefence, isCrit));

        return Mathf.RoundToInt(physics + fire + ice + thunder + poison + light + dark + GetCritValue(damage.real, isCrit));
    }

    private static float GetPercent(int value) {
        return (100 - value) / 100f;
    }

    private static float GetCritValue(float value, bool isCrit) {
        return isCrit ? value * 3f : value;
    }
}
