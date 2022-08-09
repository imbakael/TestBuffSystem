using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo {

    private ChaState attacker;

    private ChaState defender;

    private Damage damage; // 伤害值，物理、火、毒...

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
        if (defender == null) {
            return;
        }
        // 1.攻击者自身buff强化伤害
        if (attacker != null) {
            for (int i = 0; i < attacker.buffs.Count; i++) {
                BuffObj buff = attacker.buffs[i];
                buff.buffModel.onHit?.Invoke(buff, ref damageInfo);
            }
        }
        // 2.受击者自身buff减免伤害
        for (int i = 0; i < defender.buffs.Count; i++) {
            BuffObj buff = defender.buffs[i];
            buff.buffModel.onBeHurt?.Invoke(buff, ref damageInfo);
        }
        if (true) { // 如果会被杀死
            // 3.攻击者击杀敌人后触发击杀效果，如影魔击杀敌人后吸取灵魂
            if (attacker != null) {
                for (int i = 0; i < attacker.buffs.Count; i++) {
                    BuffObj buff = attacker.buffs[i];
                    buff.buffModel.onKill?.Invoke(buff, damageInfo);
                }
            }
            // 4.受击者死亡后触发效果，如死亡后沉默敌人
            for (int i = 0; i < defender.buffs.Count; i++) {
                BuffObj buff = defender.buffs[i];
                buff.buffModel.onBeKilled(buff, damageInfo);
            }
        }
        // 5.根据最后的damageInfo进行扣血操作

        // 6.伤害流程走完后对双方添加buff
        for (int i = 0; i < damageInfo.addBuffs.Count; i++) {
            AddBuffInfo addBuffInfo = damageInfo.addBuffs[i];
            addBuffInfo.target.AddBuff(addBuffInfo);
        }
    }
}
