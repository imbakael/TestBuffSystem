using System.Collections.Generic;
using UnityEngine;

public class BuffOnHitCallbacks {
    public static readonly Dictionary<string, BuffOnHit> OnHit = new Dictionary<string, BuffOnHit> {
        { "�����ƿ�", FireMaster },
        { "�¹�", MoonLight },
        { "����", PulloutHeart },
        { "ǹ����ʦ", SpearMaster },
        { "���糡", StaticElectricField },
        { "�ڰ�����", DarkPower },
        { "��͸����", PentrationFire },
        { "�ѻ�", Snipe },
        { "����һ��", ChaosAttack },
        { "����", FireAttack }
    };

    // ��ͨ�����и��������˺�30%�Ļ������˺�
    private static void FireAttack(BuffObj buff, DamageInfo damageInfo) {
        if (!damageInfo.isCommonAttack) {
            return;
        }
        float physics = damageInfo.damage.physics;
        float fire = physics * 0.3f;
        damageInfo.damage.fire += fire;
    }

    // ������ͨ����-5~15�������˺�
    private static void ChaosAttack(BuffObj buff, DamageInfo damageInfo) {
        if (!damageInfo.isCommonAttack) {
            return;
        }
        int delta = Random.Range(-5, 16);
        damageInfo.damage.physics += delta;
        damageInfo.damage.physics = Mathf.Max(0, damageInfo.damage.physics);
    }

    // �����ѻ�ʱ�˺�����
    private static void Snipe(BuffObj buff, DamageInfo damageInfo) {
        bool isTrigger = Random.Range(0f, 1f) <= 0.2f;
        if (isTrigger && damageInfo.damageSource == DamageSource.Bow) {
            damageInfo.damage.physics *= 2f;
        }
    }

    private static void PentrationFire(BuffObj buff, DamageInfo damageInfo) {
        // ����Ŀ��������޵���
        ChaState target = null;
        var damage = Damage.Copy(damageInfo.damage);
        var newDamageInfo = new DamageInfo(damageInfo.attacker, target, damage, null, false, DamageSource.None, true);
        DamageManager.AddDamageInfo(newDamageInfo);
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
            ignoreElemDefencePercent = 100
        };
        DamageManager.AddDamageInfo(newDamgeInfo);
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
            var newDamgeInfo = new DamageInfo(damageInfo.attacker, damageInfo.defender, damage, new DamageInfoTag[] { DamageInfoTag.Direct }, false);
            DamageManager.AddDamageInfo(newDamgeInfo);
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
