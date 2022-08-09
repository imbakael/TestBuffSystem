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
                buff.buffModel.onHit?.Invoke(buff, ref damageInfo);
            }
        }
        // 2.受击者自身buff减免伤害，比如降低5点所受的物理伤害
        for (int i = 0; i < defender.buffs.Count; i++) {
            BuffObj buff = defender.buffs[i];
            buff.buffModel.onBeHurt?.Invoke(buff, ref damageInfo);
        }
        if (defender.CanBeKilled(damageInfo)) {
            // 3.攻击者击杀敌人后触发击杀效果，如击杀敌人后自身加一个攻击+2的buff
            if (attacker != null) {
                for (int i = 0; i < attacker.buffs.Count; i++) {
                    BuffObj buff = attacker.buffs[i];
                    buff.buffModel.onKill?.Invoke(buff, damageInfo);
                }
            }
            // 4.受击者死亡后触发效果，如死亡后沉默敌人(给敌人上一个debuff)
            for (int i = 0; i < defender.buffs.Count; i++) {
                BuffObj buff = defender.buffs[i];
                buff.buffModel.onBeKilled?.Invoke(buff, damageInfo);
            }
        }
        // 5.根据最后的damageInfo进行扣血操作，只有此时才会用到受击者的防御和抗性进行伤害减免
        int damage = GetDamageValue(defender.currentProp, damageInfo);
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
    /// <param name="chaProperty">角色属性</param>
    /// <param name="damageInfo">伤害info</param>
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
    /// 获得正数值
    /// </summary>
    /// <param name="subtrahend">减数</param>
    /// <param name="minuend">被减数</param>
    /// <returns></returns>
    private static int GetPositiveValue(int subtrahend, int minuend) {
        int delta = subtrahend - minuend;
        return delta > 0 ? delta : 0;
    }
}
