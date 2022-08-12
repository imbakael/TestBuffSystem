using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo {

    private ChaState attacker;

    private ChaState defender;

    public Damage damage; // 伤害值，物理、火、毒...

    private DamageInfoTag[] tags; // 伤害类型，直接、间接、反弹...

    private float critRate;

    private float hitRate;

    private int ignoreDefence = 0; // 无视防守者固定值的防御，如无视5点防御

    private int ignoreDefencePercent = 0; // 无视防守者百分比的防御

    public List<AddBuffInfo> addBuffs = new List<AddBuffInfo>(); // 伤害后给角色添加的buff

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
        // 1.攻击者自身buff强化伤害，比如持弓时伤害+30%
        if (attacker != null) {
            for (int i = 0; i < attacker.buffs.Count; i++) {
                BuffObj buff = attacker.buffs[i];
                buff.model.onHit?.Invoke(buff, ref damageInfo);
            }
        }
        // 2.受击者自身buff减免伤害，比如降低5点所受的物理伤害、或者大盾触发直接把damage清零
        for (int i = 0; i < defender.buffs.Count; i++) {
            BuffObj buff = defender.buffs[i];
            buff.model.onBeHurt?.Invoke(buff, ref damageInfo);
        }
        if (defender.CanBeKilled(damageInfo)) {
            // 3.攻击者击杀敌人后触发击杀效果，如击杀敌人后自身加一个攻击+2的buff
            if (attacker != null) {
                for (int i = 0; i < attacker.buffs.Count; i++) {
                    BuffObj buff = attacker.buffs[i];
                    buff.model.onKill?.Invoke(buff, damageInfo);
                }
            }
            // 4.受击者死亡后触发效果，如死亡后沉默敌人(给敌人上一个debuff)
            for (int i = 0; i < defender.buffs.Count; i++) {
                BuffObj buff = defender.buffs[i];
                buff.model.onBeKilled?.Invoke(buff, damageInfo);
            }
        }
        // 5.根据最后的damageInfo进行扣血操作，此时damageInfo中的damag值才会确定，才会用到受击者的防御和抗性进行最终伤害计算
        int damage = GetDamageValue(damageInfo, defender.currentProp);
        defender.ModifyResource(new ChaResource(-damage));
        // 6.伤害流程走完后对双方添加buff
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
        float realDefence = Mathf.Max(0, GetPercent(damageInfo.ignoreDefencePercent) * chaProperty.defence - damageInfo.ignoreDefence);
        float physics = GetPercent(chaProperty.physicsResist) * Mathf.Max(0, damage.physics - realDefence);
        float fire = GetPercent(chaProperty.fireResist) * Mathf.Max(0, damage.fire - chaProperty.elemDefence);
        float ice = GetPercent(chaProperty.iceResist) * Mathf.Max(0, damage.ice - chaProperty.elemDefence);
        float thunder = GetPercent(chaProperty.thunderResist) * Mathf.Max(0, damage.thunder - chaProperty.elemDefence);
        float poison = GetPercent(chaProperty.poisonResist) * Mathf.Max(0, damage.poison - chaProperty.elemDefence);
        float light = GetPercent(chaProperty.lightResist) * Mathf.Max(0, damage.light - chaProperty.elemDefence);
        float dark = GetPercent(chaProperty.darkResist) * Mathf.Max(0, damage.dark - chaProperty.elemDefence);
        return Mathf.CeilToInt(physics + fire + ice + thunder + poison + light + dark);
    }

    private static float GetPercent(int value) {
        return (100 - value) / 100f;
    }

}
