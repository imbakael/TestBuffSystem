using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffModel {
    public int id;
    public string name;
    public int priority;
    public int maxStack; // 最大层数
    public string[] tags; // 比如poison，标记此buff是一个中毒buff
    public int tickTime; // 如2代表每2回合执行一次
    public ChaProperty prop; // buff附带的属性
    public bool isDebuff; // 是否是负面buff

    public BuffOnOccur onOccur;
    public BuffOnTick onTick;
    public BuffOnRemoved onRemoved;
    public BuffOnHit onHit;
    public BuffOnBeHurt onBeHurt;
    public BuffOnKill onKill;
    public BuffOnBeKilled onBeKilled;

    public BuffModel(int id, string name, int priority, int maxStack, string[] tags, int tickTime, ChaProperty prop, bool isDebuff,
        BuffOnOccur onOccur, BuffOnTick onTick, BuffOnRemoved onRemoved, BuffOnHit onHit, BuffOnBeHurt onBeHurt, BuffOnKill onKill, BuffOnBeKilled onBeKilled) {
        this.id = id;
        this.name = name;
        this.priority = priority;
        this.maxStack = maxStack;
        this.tags = tags;
        this.tickTime = tickTime;
        this.prop = prop;
        this.isDebuff = isDebuff;
        this.onOccur = onOccur;
        this.onTick = onTick;
        this.onRemoved = onRemoved;
        this.onHit = onHit;
        this.onBeHurt = onBeHurt;
        this.onKill = onKill;
        this.onBeKilled = onBeKilled;
    }
}

public delegate void BuffOnOccur(BuffObj buff, int modifyStack); // 添加buff或buff层数变化时触发
public delegate void BuffOnRemoved(BuffObj buff); // 移除buff时触发
public delegate void BuffOnTick(BuffObj buff); // 有持续时间的buff会触发，比如中毒扣血
public delegate void BuffOnHit(BuffObj buff, ref DamageInfo damageInfo); // 攻击者造成伤害时触发，比如持剑时物理伤害+30%
public delegate void BuffOnBeHurt(BuffObj buff, ref DamageInfo damageInfo); // 受击者受伤害时触发，比如降低50%弓箭伤害
public delegate void BuffOnKill(BuffObj buff, DamageInfo damageInfo); // 击杀单位时触发，如影魔收集灵魂
public delegate void BuffOnBeKilled(BuffObj buff, DamageInfo damageInfo); // 被击杀时触发，如死亡后爆炸

