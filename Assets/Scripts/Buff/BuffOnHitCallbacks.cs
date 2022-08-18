using System.Collections.Generic;
using UnityEngine;

public class BuffOnHitCallbacks : MonoBehaviour {
    public static readonly Dictionary<string, BuffOnHit> OnHit = new Dictionary<string, BuffOnHit> {
        { "�����ƿ�", FireMaster },
        { "�¹�", MoonLight },
        { "����", PulloutHeart }
    };

    // �չ�25%����ֱ�ӿ۳�33%��ǰ����ֵ
    private static void PulloutHeart(BuffObj buff, ref DamageInfo damageInfo) {
        bool isTrigger = Random.Range(0f, 1f) <= 0.25f;
        if (isTrigger) {
            int currentHp = damageInfo.defender.resource.hp;
            var damage = new Damage(0, 0, 0, 0, 0, 0, 0, currentHp / 3f);
            var newDamgeInfo = new DamageInfo(damageInfo.attacker, damageInfo.defender, damage, null, false);
            DamageManager.DealWithDamge(newDamgeInfo);
        }
    }

    // �����˺����ӵ���50%�������������ʼ�%
    private static void MoonLight(BuffObj buff, ref DamageInfo damageInfo) {
        bool isTrigger = Random.Range(0, 1f) <= buff.carrier.currentProp.skill / 100f;
        if (isTrigger) {
            damageInfo.ignoreDefencePercent = 50;
        }
    }

    // ���˺�+50%
    private static void FireMaster(BuffObj buff, ref DamageInfo damageInfo) {
        damageInfo.damage.fire *= 1.5f;
    }
}
