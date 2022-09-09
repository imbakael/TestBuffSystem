using System.Collections.Generic;
using UnityEngine;

public class BuffOnHitCallbacks {
    public static readonly Dictionary<string, BuffOnHit> OnHit = new Dictionary<string, BuffOnHit> {
        { "火焰掌控", FireMaster },
        { "月光", MoonLight },
        { "掏心", PulloutHeart },
        { "枪术大师", SpearMaster },
        { "静电场", StaticElectricField },
        { "黑暗力量", DarkPower },
        { "穿透火焰", PentrationFire },
        { "狙击", Snipe },
        { "混沌一击", ChaosAttack },
        { "附火", FireAttack }
    };

    // 普通攻击中附加物理伤害30%的火属性伤害
    private static void FireAttack(BuffObj buff, DamageInfo damageInfo) {
        if (!damageInfo.isCommonAttack) {
            return;
        }
        float physics = damageInfo.damage.physics;
        float fire = physics * 0.3f;
        damageInfo.damage.fire += fire;
    }

    // 增加普通攻击-5~15点物理伤害
    private static void ChaosAttack(BuffObj buff, DamageInfo damageInfo) {
        if (!damageInfo.isCommonAttack) {
            return;
        }
        int delta = Random.Range(-5, 16);
        damageInfo.damage.physics += delta;
        damageInfo.damage.physics = Mathf.Max(0, damageInfo.damage.physics);
    }

    // 触发狙击时伤害翻倍
    private static void Snipe(BuffObj buff, DamageInfo damageInfo) {
        bool isTrigger = Random.Range(0f, 1f) <= 0.2f;
        if (isTrigger && damageInfo.damageSource == DamageSource.Bow) {
            damageInfo.damage.physics *= 2f;
        }
    }

    private static void PentrationFire(BuffObj buff, DamageInfo damageInfo) {
        // 查找目标身后有无敌人
        ChaState target = null;
        var damage = Damage.Copy(damageInfo.damage);
        var newDamageInfo = new DamageInfo(damageInfo.attacker, target, damage, null, false, DamageSource.None, true);
        DamageManager.AddDamageInfo(newDamageInfo);
    }

    private static void DarkPower(BuffObj buff, DamageInfo damageInfo) {
        if (damageInfo.isCommonAttack && damageInfo.attacker.currentProp.darkResist / 2f > damageInfo.defender.currentProp.darkResist) {
            damageInfo.isCrit = true;
        }
    }

    // 攻击附带受击者当前生命值15%的雷伤害，此伤害无视元素防御
    private static void StaticElectricField(BuffObj buff, DamageInfo damageInfo) {
        // ！！为什么不直接修改原来的damage，而要重新生成一个damageinfo重走伤害流程呢？
        // 因为如果原伤害是雷属性伤害，但是要计算元素防御，而且计算必杀；而此附加雷伤害无视元素防御且没有必杀，所以两个伤害必须分开计算
        var damage = new Damage(thunder: damageInfo.defender.resource.hp * 0.15f);
        var newDamgeInfo = new DamageInfo(damageInfo.attacker, damageInfo.defender, damage, new DamageInfoTag[] { DamageInfoTag.Direct }, false) {
            ignoreElemDefencePercent = 100
        };
        DamageManager.AddDamageInfo(newDamgeInfo);
    }

    // 持枪时伤害+30%
    private static void SpearMaster(BuffObj buff, DamageInfo damageInfo) {
        if (damageInfo.damageSource == DamageSource.Spear) {
            damageInfo.damage.physics *= 1.3f;
        }
    }

    // 普攻25%几率直接扣除33%当前生命值
    private static void PulloutHeart(BuffObj buff, DamageInfo damageInfo) {
        bool isTrigger = Random.Range(0f, 1f) <= 0.25f;
        if (isTrigger) {
            int currentHp = damageInfo.defender.resource.hp;
            var damage = new Damage(real: currentHp / 3f);
            var newDamgeInfo = new DamageInfo(damageInfo.attacker, damageInfo.defender, damage, new DamageInfoTag[] { DamageInfoTag.Direct }, false);
            DamageManager.AddDamageInfo(newDamgeInfo);
        }
    }

    // 物理伤害无视敌人50%防御，触发几率技%
    private static void MoonLight(BuffObj buff, DamageInfo damageInfo) {
        bool isTrigger = Random.Range(0, 1f) <= buff.carrier.currentProp.skill / 100f;
        if (isTrigger) {
            damageInfo.ignoreDefencePercent = 50;
        }
    }

    // 火伤害+50%
    private static void FireMaster(BuffObj buff, DamageInfo damageInfo) {
        damageInfo.damage.fire *= 1.5f;
    }
}
