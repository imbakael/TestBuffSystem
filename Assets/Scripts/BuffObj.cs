using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObj {
    public BuffModel model;
    public ChaState caster;
    public ChaState carrier;
    public bool permanent;
    public int stack;
    public int duration; // 持续多少回合
    public int timeElapsed = 0; // 已经存在多少回合

    public BuffObj(BuffModel model, ChaState caster, ChaState carrier, bool permanent, int stack, int duration) {
        this.model = model;
        this.caster = caster;
        this.carrier = carrier;
        this.permanent = permanent;
        this.stack = stack;
        this.duration = duration;
    }
}

