using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DesignFormula {

    // 速度差会产生躲避率，上限不超过30%
    public static float DodgeRate(int attackerSpeed, int defenderSpeed) {
        int delta = defenderSpeed - attackerSpeed;
        if (delta < 5) {
            return 0f;
        }
        float rate = delta * 1f / defenderSpeed;
        return Mathf.Min(rate, 0.3f);
    }

    // 计算必杀
    public static bool IsCrit(ChaProperty attacker, ChaProperty defender) {
        return Random.Range(1, 101) <= (attacker.CritRate - defender.CritDodgeRate);
    }

    // 必杀倍率：3
    public static float GetCritValue(float value, bool isCrit) {
        return isCrit ? value * 3f : value;
    }

}
