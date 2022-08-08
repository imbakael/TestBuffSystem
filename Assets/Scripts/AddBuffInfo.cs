using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuffInfo {

    public BuffModel buffModel;
    public ChaState caster;
    public ChaState target;
    public int addStack;
    ///<summary>
    ///关于时间，是改变还是设置为, true代表设置为，false代表改变
    ///</summary>
    public bool durationSetTo;
    public bool permanent;
    public int duration;

    ///<summary>
    ///buff的一些参数，这些参数是逻辑使用的，比如wow中牧师的盾还能吸收多少伤害，就可以记录在buffParam里面
    ///</summary>
    public Dictionary<string, object> buffParam;

    public AddBuffInfo(
        BuffModel buffModel, ChaState caster, ChaState target,
        int addStack, int duration, bool durationSetTo, bool permanent, Dictionary<string, object> buffParam
    ) {
        this.buffModel = buffModel;
        this.caster = caster;
        this.target = target;
        this.addStack = addStack;
        this.duration = duration;
        this.durationSetTo = durationSetTo;
        this.buffParam = buffParam;
        this.permanent = permanent;
    }
}
