using System.Collections.Generic;
using UnityEngine;

public class BuffOnBeHurtCallbacks : MonoBehaviour {
    public static readonly Dictionary<string, BuffOnBeHurt> OnBeHurt = new Dictionary<string, BuffOnBeHurt> {
        { "Ů���ػ�", GodnessGuard },
        { "���", LargeShield },
        { "����", Reflect },
        { "ʯ��Ƥ��", StoneSkin },
        { "���¹���", AfraidBow },
        { "������",  DarkReflect }
    };

    private static void DarkReflect(BuffObj buff, DamageInfo damageInfo) {
        if (!damageInfo.IsReflectDamge() && damageInfo.defender.currentProp.physicsResist > damageInfo.attacker.currentProp.physicsResist) {
            var damage = new Damage(dark: damageInfo.damage.GetValue() * 0.3f);
            var newDamageInfo = new DamageInfo(damageInfo.defender, damageInfo.attacker, damage, new DamageInfoTag[] { DamageInfoTag.Reflect }, false);
            DamageManager.DealWithDamge(newDamageInfo);
        }
    }

    // �ܵ�2���Ĺ����˺�
    private static void AfraidBow(BuffObj buff, DamageInfo damageInfo) {
        if (damageInfo.damageSource == DamageSource.Bow) {
            damageInfo.damage.physics *= 2;
        }
    }

    // �ܵ��������˺�-3
    private static void StoneSkin(BuffObj buff, DamageInfo damageInfo) {
        damageInfo.damage -= 3f;
    }

    // �ܵ��Ƿ������˺�ʱ������20%�˺��������Ƴ���
    private static void Reflect(BuffObj buff, DamageInfo damageInfo) {
        if (!damageInfo.IsReflectDamge()) {
            var damage = new Damage(real: damageInfo.damage.GetValue() * 0.2f);
            var newDamageInfo = new DamageInfo(damageInfo.defender, damageInfo.attacker, damage, new DamageInfoTag[] { DamageInfoTag.Reflect }, false);
            DamageManager.DealWithDamge(newDamageInfo);
        }
    }

    // ����%�������˺���0
    private static void LargeShield(BuffObj buff, DamageInfo damageInfo) {
        bool isTrigger = Random.Range(0, 1f) <= buff.carrier.currentProp.defence / 100f;
        if (isTrigger) {
            damageInfo.damage = new Damage();
        }
    }

    // ����20%�ܵ��������˺�
    private static void GodnessGuard(BuffObj buff, DamageInfo damageInfo) {
        damageInfo.damage *= 0.8f;
    }
}
