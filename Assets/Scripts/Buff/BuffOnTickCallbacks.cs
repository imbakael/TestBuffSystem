using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffOnTickCallbacks : MonoBehaviour {
    public static readonly Dictionary<string, BuffOnTick> OnTick = new Dictionary<string, BuffOnTick> {
        { "÷–∂æ", Poison },
        { "æª≥˝", Cleanse }
    };

    private static void Cleanse(BuffObj buff) {
        ChaState carrier = buff.carrier;
        carrier.RemoveBuff((buff) => buff.model.isDebuff);
    }

    private static void Poison(BuffObj buff) {
        var damage = new Damage(poison:3);
        var damageInfo = new DamageInfo(null, buff.carrier, damage, new DamageInfoTag[] { DamageInfoTag.Direct }, false) {
            ignorePoisonDefencePercent = 100 // ∂æ…À∫¶Œﬁ ”‘™Àÿ∑¿”˘
        };
        DamageManager.AddDamageInfo(damageInfo);
    }
}
