using System.Collections.Generic;
using UnityEngine;

public class BuffOnBeHurtCallbacks : MonoBehaviour {
    public static readonly Dictionary<string, BuffOnBeHurt> OnBeHurt = new Dictionary<string, BuffOnBeHurt> {
        { "Ů���ػ�", GodnessGuard },
        { "���", LargeShield },
        { "����", Reflect },
        { "ʯ��Ƥ��", StoneSkin },
        { "���¹���", AfraidBow }
    };

    // �ܵ�2���Ĺ����˺�
    private static void AfraidBow(BuffObj buff, ref DamageInfo damageInfo) {
        if (damageInfo.damageSource == DamageSource.Bow) {
            damageInfo.damage.physics *= 2;
        }
    }

    // �ܵ��������˺�-3
    private static void StoneSkin(BuffObj buff, ref DamageInfo damageInfo) {
        damageInfo.damage -= 3f;
    }

    // �ܵ��Ƿ������˺�ʱ������30%�˺�
    private static void Reflect(BuffObj buff, ref DamageInfo damageInfo) {
        if (!damageInfo.IsReflectDamge()) {
            var damage = damageInfo.damage * 0.3f;
            var newDamageInfo = new DamageInfo(damageInfo.defender, damageInfo.attacker, damage, new DamageInfoTag[] { DamageInfoTag.Reflect }, false);
            DamageManager.DealWithDamge(newDamageInfo);
        }
    }

    // ����%�������˺���0
    private static void LargeShield(BuffObj buff, ref DamageInfo damageInfo) {
        bool isTrigger = Random.Range(0, 1f) <= buff.carrier.currentProp.defence / 100f;
        if (isTrigger) {
            damageInfo.damage = Damage.Zero();
        }
    }

    // ����20%�ܵ��������˺�
    private static void GodnessGuard(BuffObj buff, ref DamageInfo damageInfo) {
        damageInfo.damage *= 0.8f;
    }
}
