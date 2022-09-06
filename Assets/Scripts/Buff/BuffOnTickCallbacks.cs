using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffOnTickCallbacks {
    public static readonly Dictionary<string, BuffOnTick> OnTick = new Dictionary<string, BuffOnTick> {
        { "�ж�", Poison },
        { "����", Cleanse },
        { "����", Regenerate },
        { "����", DarkFire }
    };

    // ��������Χ4���ڵ���������糿�������˺�������Ԫ�ط����Ͱ���
    private static void DarkFire(BuffObj buff) {
        // ����������Χ4������е��ˣ�Ȼ�����ѡһ��
        ChaState target = null;
        float factor = buff.carrier.currentProp.darkResist / 100f * (1 + buff.carrier.currentProp.darkResist / 100f);
        float damageValue = buff.carrier.currentProp.elemStrength * Mathf.Max(0, factor);
        var damage = new Damage(dark: damageValue);
        var damageInfo = new DamageInfo(buff.carrier, target, damage, null, false);
        DamageManager.AddDamageInfo(damageInfo);
    }

    // �ظ����hp / 4 * �����Ե�����
    private static void Regenerate(BuffObj buff) {
        int heal = Mathf.CeilToInt(Mathf.Max(0, buff.carrier.currentProp.hp / 4f * Mathf.Pow(buff.carrier.currentProp.physicsResist / 100f, 2)));
        buff.carrier.ModifyResource(new ChaResource(heal));
    }

    // ȥ����������debuff
    private static void Cleanse(BuffObj buff) {
        ChaState carrier = buff.carrier;
        carrier.RemoveBuff((buff) => buff.model.isDebuff);
    }

    private static void Poison(BuffObj buff) {
        var damage = new Damage(poison:3);
        var damageInfo = new DamageInfo(null, buff.carrier, damage, new DamageInfoTag[] { DamageInfoTag.Direct }, false) {
            ignorePoisonDefencePercent = 100 // ���˺�����Ԫ�ط���
        };
        DamageManager.AddDamageInfo(damageInfo);
    }
}
