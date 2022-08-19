using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    private void Start() {
        ChaState a = new ChaState(new ChaProperty(30, 6, 4, 2, 0, 3, 4, 9, 10, 0, 0, 0, 0, 0, 0));
        ChaState b = new ChaState(new ChaProperty(20, 5, 2, 1, 0, 8, 2, 5, 5, 0, 0, 0, 0, 0, 0));

        // 产生一次普通攻击
        Debug.Log("1 b.hp = " + b.resource.hp);
        Attack(a, b);
    }


    private void Attack(ChaState attacker, ChaState defender) {
        float dodgeRate = DesignFormula.DodgeRate(attacker.currentProp.speed, defender.currentProp.speed);
        bool isDodge = Random.Range(0f, 1f) <= dodgeRate;
        if (isDodge) {
            Debug.Log("躲避");
            return;
        } else {
            // 判断defender中是否有躲避buff，如果躲避则退出

        }
        bool isCrit = DesignFormula.IsCrit(attacker.currentProp, defender.currentProp);
        // 普攻攻击产生的damageInfo
        var damageInfo = new DamageInfo(attacker, defender, new Damage(physics: attacker.currentProp.PhysicsAttack), new DamageInfoTag[] { DamageInfoTag.Direct }, isCrit);
        DamageManager.AddDamageInfo(damageInfo);
    }
}
