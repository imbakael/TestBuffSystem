using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    private void Start() {
        ChaState a = new ChaState(new ChaProperty(30, 6, 4, 2, 0, 3, 4, 9, 10, 0, 0, 0, 0, 0, 0));
        ChaState b = new ChaState(new ChaProperty(20, 5, 2, 1, 0, 8, 2, 5, 5, 0, 0, 0, 0, 0, 0));

        // 产生一次普通攻击
        //Debug.Log("1 b.hp = " + b.resource.hp);
        //Attack(a, b);
        //Debug.Log("一个月坐地铁花费：" + GetSubwayPrice(44, 7f));
    }

    private void Attack(ChaState attacker, ChaState defender) {
        float dodgeRate = DesignFormula.DodgeRate(attacker.currentProp.speed, defender.currentProp.speed);
        bool isDodge = Random.Range(0f, 1f) <= dodgeRate;
        if (isDodge) {
            Debug.Log("躲避");
            return;
        }
        bool isCrit = DesignFormula.IsCrit(attacker.currentProp, defender.currentProp);
        // 普攻攻击产生的damageInfo
        var damageInfo = new DamageInfo(attacker, defender, new Damage(physics: attacker.currentProp.PhysicsAttack), new DamageInfoTag[] { DamageInfoTag.Direct }, isCrit);
        DamageManager.AddDamageInfo(damageInfo);
    }

    private float GetSubwayPrice(int useCount, float singlePrice) {
        float sum = 0;
        for (int i = 0; i < useCount; i++) {
            float factSinglePrice =
                sum < 100f ? singlePrice * 0.9f :
                sum >= 100f && sum < 200f ? singlePrice * 0.8f :
                sum >= 200f && sum < 300f ? singlePrice * 0.7f :
                singlePrice * 0.9f;
            sum += factSinglePrice;
        }
        return sum;
    }
}

