using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffOnKillCallbacks {
    public static readonly Dictionary<string, BuffOnKill> Onkill = new Dictionary<string, BuffOnKill> {
        { "�ռ����", CollectSouls },
        { "�츳͵ȡ", TalentSteal }
    };

    private static void TalentSteal(BuffObj buff, DamageInfo damageInfo) {
        // ��ɱ��͵ȡ1���츳������2��
        // ��Ҫ����UI����UI��ѡ����˵Ŀ�͵ȡ�츳��Ȼ�������Ӷ�Ӧ���츳buff��ˢ���츳��UI
    }

    private static void CollectSouls(BuffObj buff, DamageInfo damageInfo) {
        // ��buff.carrier��һ��buff����buff��1�㹥��
    }
}
