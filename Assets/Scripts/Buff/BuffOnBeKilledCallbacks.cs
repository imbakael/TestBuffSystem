using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffOnBeKilledCallbacks {
    public static readonly Dictionary<string, BuffOnBeKilled> OnBeKilled = new Dictionary<string, BuffOnBeKilled> {
        { "自爆", SelfDestruct }
    };

    private static void SelfDestruct(BuffObj buff, DamageInfo damageInfo) {
        // 本质是产生一个AOE
        // 自身死亡时对周围所有敌人造成25点火属性伤害
        var damage = new Damage(fire: 25);
        var newDamageInfo = new DamageInfo(null, null, damage, new DamageInfoTag[] { DamageInfoTag.Direct }, false);
    }
}
