using System.Collections.Generic;
using UnityEngine;

public class BuffOnHitCallbacks : MonoBehaviour {
    public static readonly Dictionary<string, BuffOnHit> OnHit = new Dictionary<string, BuffOnHit> {
        { "�����ƿ�", FireMaster },
        { "�¹�", MoonLight },
        { "����", PulloutHeart },
        { "ǹ����ʦ", SpearMaster },
        { "���糡", StaticElectricField },
        { "�ڰ�����", DarkPower },
        { "��͸����", PentrationFire }
    };

    private static void PentrationFire(BuffObj buff, DamageInfo damageInfo) {
        // ����Ŀ��������޵���
        ChaState target = null;
        var damage = Damage.Copy(damageInfo.damage);
        var newDamageInfo = new DamageInfo(damageInfo.attacker, target, damage, null, false, DamageSource.None, true);
        DamageManager.DealWithDamge(newDamageInfo);
    }

    private static void DarkPower(BuffObj buff, DamageInfo damageInfo) {
        if (damageInfo.isCommonAttack && damageInfo.attacker.currentProp.darkResist / 2f > damageInfo.defender.currentProp.darkResist) {
            damageInfo.isCrit = true;
        }
    }

    // ���������ܻ��ߵ�ǰ����ֵ15%�����˺������˺�����Ԫ�ط���
    private static void StaticElectricField(BuffObj buff, DamageInfo damageInfo) {
        // ����Ϊʲô��ֱ���޸�ԭ����damage����Ҫ��������һ��damageinfo�����˺������أ�
        // ��Ϊ���ԭ�˺����������˺�������Ҫ����Ԫ�ط��������Ҽ����ɱ�����˸������˺�����Ԫ�ط�����û�б�ɱ�����������˺�����ֿ�����
        var damage = new Damage(thunder: damageInfo.defender.resource.hp * 0.15f);
        var newDamgeInfo = new DamageInfo(damageInfo.attacker, damageInfo.defender, damage, new DamageInfoTag[] { DamageInfoTag.Direct }, false) {
            ignoreThunderDefencePercent = 100
        };
        DamageManager.DealWithDamge(newDamgeInfo);
    }

    // ��ǹʱ�˺�+30%
    private static void SpearMaster(BuffObj buff, DamageInfo damageInfo) {
        if (damageInfo.damageSource == DamageSource.Spear) {
            damageInfo.damage.physics *= 1.3f;
        }
    }

    // �չ�25%����ֱ�ӿ۳�33%��ǰ����ֵ
    private static void PulloutHeart(BuffObj buff, DamageInfo damageInfo) {
        bool isTrigger = Random.Range(0f, 1f) <= 0.25f;
        if (isTrigger) {
            int currentHp = damageInfo.defender.resource.hp;
            var damage = new Damage(real: currentHp / 3f);
            var newDamgeInfo = new DamageInfo(damageInfo.attacker, damageInfo.defender, damage, new DamageInfoTag[] { DamageInfoTag.Direct }, false, DamageSource.None);
            DamageManager.DealWithDamge(newDamgeInfo);
        }
    }

    // �����˺����ӵ���50%�������������ʼ�%
    private static void MoonLight(BuffObj buff, DamageInfo damageInfo) {
        bool isTrigger = Random.Range(0, 1f) <= buff.carrier.currentProp.skill / 100f;
        if (isTrigger) {
            damageInfo.ignoreDefencePercent = 50;
        }
    }

    // ���˺�+50%
    private static void FireMaster(BuffObj buff, DamageInfo damageInfo) {
        damageInfo.damage.fire *= 1.5f;
    }
}
