using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffOnKillCallbacks {
    public static readonly Dictionary<string, BuffOnKill> Onkill = new Dictionary<string, BuffOnKill> {
        { "收集灵魂", CollectSouls },
        { "天赋偷取", TalentSteal }
    };

    private static void TalentSteal(BuffObj buff, DamageInfo damageInfo) {
        // 击杀后偷取1个天赋，上限2个
        // 需要弹出UI，在UI中选择敌人的可偷取天赋，然后给自身加对应的天赋buff，刷新天赋栏UI
    }

    private static void CollectSouls(BuffObj buff, DamageInfo damageInfo) {
        // 给buff.carrier加一层buff，此buff加1点攻击
    }
}
