using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo {

    public ChaState attacker;

    public ChaState defender;

    public Damage damage; // 伤害值，物理、火、毒...

    private DamageInfoTag[] tags; // 伤害类型，直接、间接、反弹...

    private float critRate;

    private float hitRate;

    // 某些伤害会无视目标百分比的防御，比如攻击附加5点雷属性伤害，此伤害无视元素防御，所以此damageInfo的 ignoreThunderDefencePercent == 100
    public int ignoreDefencePercent = 0;
    public int ignoreFireDefencePercent = 0;
    public int ignoreIceDefencePercent = 0;
    public int ignoreThunderDefencePercent = 0;
    public int ignorePoisonDefencePercent = 0;
    public int ignoreLightDefencePercent = 0;
    public int ignoreDarkDefencePercent = 0;

    public List<AddBuffInfo> addBuffs = new List<AddBuffInfo>(); // 伤害后给角色添加的buff

    public DamageInfo(ChaState attacker, ChaState defender, Damage damage, DamageInfoTag[] tags, float critRate, float hitRate) {
        this.attacker = attacker;
        this.defender = defender;
        this.damage = damage;
        this.tags = tags;
        this.critRate = critRate;
        this.hitRate = hitRate;
    }

}
