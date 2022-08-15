using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主要通过DamageInfo来创建和修改damage，通过层层修改damage达到buff效果
/// 例如：某次伤害属于火+暗的混合伤害，{火：100，暗：50}，受击者身上buff抵挡30%火伤害，则通过此buff后damage修改为 {火：70，暗：50}
/// </summary>
public class Damage {

    public int physics;
    public int fire;
    public int ice;
    public int thunder;
    public int poison;
    public int light;
    public int dark;

    public int real; // 真实伤害，无视防御和抗性，比如扣除当前30%生命值，此伤害即为真实伤害；又如回复10hp、回复最大生命的10%生命值，也属于真实伤害

    public Damage(int physics = 0, int fire = 0, int ice = 0, int thunder = 0, int poison = 0, int light = 0, int dark = 0, int real = 0) {
        this.physics = physics;
        this.fire = fire;
        this.ice = ice;
        this.thunder = thunder;
        this.poison = poison;
        this.light = light;
        this.dark = dark;
        this.real = real;
    }
}
