using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 普攻、技能、部分buff都会new一个伤害信息，伤害信息经过DamageManager处理后才会进行最终扣血，如果伤害被躲避了那么根本就不会生成damageInfo
public class DamageInfo {

    public ChaState attacker;
    public ChaState defender;
    public Damage damage; // 伤害值，物理、火、毒...
    public bool isCrit = false;
    public DamageInfoTag[] tags; // 伤害类型，如直接伤害、反弹伤害、治疗
    public DamageSource damageSource; // 伤害来源，如剑、矛、斧、弓箭
    public bool isCommonAttack; // 是否为普攻

    // 某些伤害会无视目标百分比的物理防御/元素防御，比如攻击附加5点雷属性伤害，此伤害无视元素防御，所以此damageInfo的 ignoreElemDefencePercent == 100
    public int ignoreDefencePercent = 0;
    public int ignoreElemDefencePercent = 0;

    public List<AddBuffInfo> addBuffs = new List<AddBuffInfo>(); // 伤害后给角色添加的buff

    public DamageInfo(ChaState attacker, ChaState defender, Damage damage, DamageInfoTag[] tags, bool isCrit = false, DamageSource damageSource = DamageSource.None,
        bool isCommonAttack = false) {
        this.attacker = attacker;
        this.defender = defender;
        this.damage = damage;
        this.tags = tags;
        this.isCrit = isCrit;
        this.damageSource = damageSource;
        this.isCommonAttack = isCommonAttack;
    }

    public bool IsReflectDamge() => tags.Any(t => t == DamageInfoTag.Reflect);

}
