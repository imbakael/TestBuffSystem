using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffModel {
    public int id;
    public string name;
    public int priority;
    public int maxStack; // ������
    public string[] tags; // ����poison����Ǵ�buff��һ���ж�buff
    public int tickTime; // ��2����ÿ2�غ�ִ��һ��
    public ChaProperty propMod;

    public BuffOnOccur onOccur;
    public BuffOnTick onTick;
    public BuffOnRemoved onRemoved;
    public BuffOnHit onHit;
    public BuffOnBeHurt onBeHurt;
    public BuffOnKill onKill;
    public BuffOnBeKilled onBeKilled;

    public BuffModel(int id, string name, int priority, int maxStack, string[] tags, int tickTime, ChaProperty propMod,
        BuffOnOccur onOccur, BuffOnTick onTick, BuffOnRemoved onRemoved, BuffOnHit onHit, BuffOnBeHurt onBeHurt, BuffOnKill onKill, BuffOnBeKilled onBeKilled) {
        this.id = id;
        this.name = name;
        this.priority = priority;
        this.maxStack = maxStack;
        this.tags = tags;
        this.tickTime = tickTime;
        this.propMod = propMod;
        this.onOccur = onOccur;
        this.onTick = onTick;
        this.onRemoved = onRemoved;
        this.onHit = onHit;
        this.onBeHurt = onBeHurt;
        this.onKill = onKill;
        this.onBeKilled = onBeKilled;
    }
}

public delegate void BuffOnOccur(BuffObj buff, int modifyStack);
public delegate void BuffOnRemoved(BuffObj buff);
public delegate void BuffOnTick(BuffObj buff);
public delegate void BuffOnHit(BuffObj buff, ref DamageInfo damageInfo);
public delegate void BuffOnBeHurt(BuffObj buff, ref DamageInfo damageInfo);
public delegate void BuffOnKill(BuffObj buff, DamageInfo damageInfo);
public delegate void BuffOnBeKilled(BuffObj buff, DamageInfo damageInfo);
