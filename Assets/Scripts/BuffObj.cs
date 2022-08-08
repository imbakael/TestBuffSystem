using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObj {
    public BuffModel buffModel;
    public ChaState caster;
    public ChaState carrier;
    public bool permanent;
    public int stack;
    public int duration; // �������ٻغ�
    public int timeElapsed; // �Ѿ����ڶ��ٻغ�

    public BuffObj(BuffModel buffModel, ChaState caster, ChaState carrier, bool permanent, int stack, int duration, int timeElapsed) {
        this.buffModel = buffModel;
        this.caster = caster;
        this.carrier = carrier;
        this.permanent = permanent;
        this.stack = stack;
        this.duration = duration;
        this.timeElapsed = timeElapsed;
    }
}

