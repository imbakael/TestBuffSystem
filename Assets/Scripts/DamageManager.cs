using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour {

    private static List<DamageInfo> damageInfos = new List<DamageInfo>();

    private void Update() {
        while(damageInfos.Count > 0) {
            DealWithDamge(damageInfos[0]);
            damageInfos.RemoveAt(0);
        }
    }

    public static void AddDamageInfo(DamageInfo damageInfo) {
        damageInfos.Add(damageInfo);
    }

    private static void DealWithDamge(DamageInfo damageInfo) {
        ChaState attacker = damageInfo.attacker;
        ChaState defender = damageInfo.defender;
        if (defender == null || defender.IsDead) {
            return;
        }

        // 1.部分buff触发会修改damageInfo
        if (attacker != null) {
            for (int i = 0; i < attacker.buffs.Count; i++) {
                BuffObj buff = attacker.buffs[i];
                buff.model.onHit?.Invoke(buff, damageInfo);
            }
        }
        for (int i = 0; i < defender.buffs.Count; i++) {
            BuffObj buff = defender.buffs[i];
            buff.model.onBeHurt?.Invoke(buff, damageInfo);
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
        Debug.Log("defender.hp = " + defender.resource.hp);

        // 3.伤害流程走完后对双方添加buff
        for (int i = 0; i < damageInfo.addBuffs.Count; i++) {
            AddBuffInfo addBuffInfo = damageInfo.addBuffs[i];
            addBuffInfo.target.AddBuff(addBuffInfo);
        }
    }

    /// <summary>
    /// 通过damageInfo和角色属性计算伤害，属于最终伤害n，如果有些特殊buff会降低50%最终伤害，则应该n * 0.5f
    /// </summary>
    /// <param name="damageInfo">伤害info</param>
    /// <param name="target">角色属性</param>
    /// <returns></returns>
    public static int GetDamageValue(DamageInfo damageInfo, ChaProperty target) {
        Damage damage = damageInfo.damage;
        bool isCrit = damageInfo.isCrit;
        float realDefence = target.defence * GetPercent(damageInfo.ignoreDefencePercent);
        float realElemDefence = target.elemDefence * GetPercent(damageInfo.ignoreElemDefencePercent);
        float physics = Mathf.Max(0, DesignFormula.GetCritValue(damage.physics - realDefence, isCrit)) * GetPercent(target.physicsResist);
        float fire = Mathf.Max(0, DesignFormula.GetCritValue(damage.fire - realElemDefence, isCrit)) * GetPercent(target.fireResist);
        float ice =  Mathf.Max(0, DesignFormula.GetCritValue(damage.ice - realElemDefence, isCrit)) * GetPercent(target.iceResist);
        float thunder = Mathf.Max(0, DesignFormula.GetCritValue(damage.thunder - realElemDefence, isCrit)) * GetPercent(target.thunderResist);
        float poison = Mathf.Max(0, DesignFormula.GetCritValue(damage.poison - realElemDefence, isCrit)) * GetPercent(target.poisonResist);
        float light = Mathf.Max(0, DesignFormula.GetCritValue(damage.light - realElemDefence, isCrit)) * GetPercent(target.lightResist);
        float dark = Mathf.Max(0, DesignFormula.GetCritValue(damage.dark - realElemDefence, isCrit)) * GetPercent(target.darkResist);
        return Mathf.RoundToInt(physics + fire + ice + thunder + poison + light + dark + damage.real);
    }

    private static float GetPercent(int value) {
        return (100 - value) / 100f;
    }

}
