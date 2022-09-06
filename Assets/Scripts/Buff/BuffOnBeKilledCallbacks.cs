using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffOnBeKilledCallbacks {
    public static readonly Dictionary<string, BuffOnBeKilled> OnBeKilled = new Dictionary<string, BuffOnBeKilled> {
        { "�Ա�", SelfDestruct }
    };

    private static void SelfDestruct(BuffObj buff, DamageInfo damageInfo) {
        // �����ǲ���һ��AOE
        // ��������ʱ����Χ���е������25��������˺�
        var damage = new Damage(fire: 25);
        var newDamageInfo = new DamageInfo(null, null, damage, new DamageInfoTag[] { DamageInfoTag.Direct }, false);
    }
}
