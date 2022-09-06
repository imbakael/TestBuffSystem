using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffOnTickCallbacks {
    public static readonly Dictionary<string, BuffOnTick> OnTick = new Dictionary<string, BuffOnTick> {
        { "中毒", Poison },
        { "净除", Cleanse },
        { "再生", Regenerate },
        { "暗焰", DarkFire }
    };

    // 对自身周围4格内的随机敌人早晨暗属性伤害，计算元素防御和暗抗
    private static void DarkFire(BuffObj buff) {
        // 搜素自身周围4格的所有敌人，然后随机选一个
        ChaState target = null;
        float factor = buff.carrier.currentProp.darkResist / 100f * (1 + buff.carrier.currentProp.darkResist / 100f);
        float damageValue = buff.carrier.currentProp.elemStrength * Mathf.Max(0, factor);
        var damage = new Damage(dark: damageValue);
        var damageInfo = new DamageInfo(buff.carrier, target, damage, null, false);
        DamageManager.AddDamageInfo(damageInfo);
    }

    // 回复最大hp / 4 * 物理抗性的生命
    private static void Regenerate(BuffObj buff) {
        int heal = Mathf.CeilToInt(Mathf.Max(0, buff.carrier.currentProp.hp / 4f * Mathf.Pow(buff.carrier.currentProp.physicsResist / 100f, 2)));
        buff.carrier.ModifyResource(new ChaResource(heal));
    }

    // 去除自身所有debuff
    private static void Cleanse(BuffObj buff) {
        ChaState carrier = buff.carrier;
        carrier.RemoveBuff((buff) => buff.model.isDebuff);
    }

    private static void Poison(BuffObj buff) {
        var damage = new Damage(poison:3);
        var damageInfo = new DamageInfo(null, buff.carrier, damage, new DamageInfoTag[] { DamageInfoTag.Direct }, false) {
            ignorePoisonDefencePercent = 100 // 毒伤害无视元素防御
        };
        DamageManager.AddDamageInfo(damageInfo);
    }
}
