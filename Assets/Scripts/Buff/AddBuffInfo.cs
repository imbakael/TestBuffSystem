using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuffInfo {

    public BuffModel model;
    public ChaState caster;
    public ChaState target;
    ///<summary>
    ///关于时间，是改变还是设置为, true代表设置为，false代表改变
    ///</summary>
    public bool durationSetTo;
    public bool permanent; // 是否永久
    public int addStack;
    public int duration;

    public AddBuffInfo(BuffModel model, ChaState caster, ChaState target, bool durationSetTo, bool permanent, int addStack, int duration) {
        this.model = model;
        this.caster = caster;
        this.target = target;
        this.durationSetTo = durationSetTo;
        this.permanent = permanent;
        this.addStack = addStack;
        this.duration = duration;
    }
}
