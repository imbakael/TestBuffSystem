using System.Collections.Generic;
using UnityEngine;

public class BuffOnBeHurtCallbacks : MonoBehaviour {
    public static readonly Dictionary<string, BuffOnBeHurt> OnBeHurt = new Dictionary<string, BuffOnBeHurt> {
        { "女神守护", GodnessGuard },
        { "大盾", LargeShield },
        { "反弹", Reflect },
        { "石化皮肤", StoneSkin },
        { "惧怕弓箭", AfraidBow },
        { "暗反射",  DarkReflect }
    };

    private static void DarkReflect(BuffObj buff, DamageInfo damageInfo) {
        if (!damageInfo.IsReflectDamge() && damageInfo.defender.currentProp.physicsResist > damageInfo.attacker.currentProp.physicsResist) {
            var damage = new Damage(dark: damageInfo.damage.GetValue() * 0.3f);
            var newDamageInfo = new DamageInfo(damageInfo.defender, damageInfo.attacker, damage, new DamageInfoTag[] { DamageInfoTag.Reflect }, false);
            DamageManager.DealWithDamge(newDamageInfo);
        }
    }

    // 受到2倍的弓箭伤害
    private static void AfraidBow(BuffObj buff, DamageInfo damageInfo) {
        if (damageInfo.damageSource == DamageSource.Bow) {
            damageInfo.damage.physics *= 2;
        }
    }

    // 受到的所有伤害-3
    private static void StoneSkin(BuffObj buff, DamageInfo damageInfo) {
        damageInfo.damage -= 3f;
    }

    // 受到非反弹的伤害时，反弹20%伤害（生命移除）
    private static void Reflect(BuffObj buff, DamageInfo damageInfo) {
        if (!damageInfo.IsReflectDamge()) {
            var damage = new Damage(real: damageInfo.damage.GetValue() * 0.2f);
            var newDamageInfo = new DamageInfo(damageInfo.defender, damageInfo.attacker, damage, new DamageInfoTag[] { DamageInfoTag.Reflect }, false);
            DamageManager.DealWithDamge(newDamageInfo);
        }
    }

    // 防御%触发，伤害清0
    private static void LargeShield(BuffObj buff, DamageInfo damageInfo) {
        bool isTrigger = Random.Range(0, 1f) <= buff.carrier.currentProp.defence / 100f;
        if (isTrigger) {
            damageInfo.damage = new Damage();
        }
    }

    // 降低20%受到的任意伤害
    private static void GodnessGuard(BuffObj buff, DamageInfo damageInfo) {
        damageInfo.damage *= 0.8f;
    }
}
