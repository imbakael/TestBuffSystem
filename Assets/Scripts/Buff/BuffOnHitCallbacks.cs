using System.Collections.Generic;
using UnityEngine;

public class BuffOnHitCallbacks : MonoBehaviour {
    public static readonly Dictionary<string, BuffOnHit> OnHit = new Dictionary<string, BuffOnHit> {
        { "火焰掌控", FireMaster },
        { "月光", MoonLight },
        { "掏心", PulloutHeart }
    };

    // 普攻25%几率直接扣除33%当前生命值
    private static void PulloutHeart(BuffObj buff, ref DamageInfo damageInfo) {
        bool isTrigger = Random.Range(0f, 1f) <= 0.25f;
        if (isTrigger) {
            int currentHp = damageInfo.defender.resource.hp;
            var damage = new Damage(0, 0, 0, 0, 0, 0, 0, currentHp / 3f);
            var newDamgeInfo = new DamageInfo(damageInfo.attacker, damageInfo.defender, damage, null, false);
            DamageManager.DealWithDamge(newDamgeInfo);
        }
    }

    // 物理伤害无视敌人50%防御，触发几率技%
    private static void MoonLight(BuffObj buff, ref DamageInfo damageInfo) {
        bool isTrigger = Random.Range(0, 1f) <= buff.carrier.currentProp.skill / 100f;
        if (isTrigger) {
            damageInfo.ignoreDefencePercent = 50;
        }
    }

    // 火伤害+50%
    private static void FireMaster(BuffObj buff, ref DamageInfo damageInfo) {
        damageInfo.damage.fire *= 1.5f;
    }
}
