using UnityEngine;

public class Test : MonoBehaviour {

    private void Start() {
        ChaState a = new ChaState(new ChaProperty(30, 6, 4, 2, 0, 3, 4, 9, 10, 0, 0, 0, 0, 0, 0));
        ChaState b = new ChaState(new ChaProperty(20, 5, 2, 1, 0, 8, 2, 5, 25, 0, 0, 0, 0, 0, 0));

        // 产生一次普通攻击
        Attack(a, b);
    }

    private void Attack(ChaState attacker, ChaState defender) {
        float dodgeRate = ChaProperty.DodgeRate(attacker.currentProp.speed, defender.currentProp.speed);
        bool isHit = Random.Range(0f, 1f) <= dodgeRate;
        if (isHit) {
            // 判断defender中是否有躲避buff
        }
        float critRate = attacker.currentProp.CritRate - defender.currentProp.CritDodgeRate;
        bool isCrit = Random.Range(0f, 1f) <= critRate;
        // 判断
        DamageInfo damageInfo = new DamageInfo(attacker, defender, new Damage(attacker.currentProp.PhysicsAttack), null, isCrit);
    }
}
