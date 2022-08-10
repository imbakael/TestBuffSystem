using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObj {
    public BuffModel model;
    public ChaState caster;
    public ChaState carrier;
    public bool permanent;
    public int stack;
    public int duration; // �������ٻغ�
    public int timeElapsed = 0; // �Ѿ����ڶ��ٻغ�

    public BuffObj(BuffModel model, ChaState caster, ChaState carrier, bool permanent, int stack, int duration) {
        this.model = model;
        this.caster = caster;
        this.carrier = carrier;
        this.permanent = permanent;
        this.stack = stack;
        this.duration = duration;
    }
}

