using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuffInfo {

    public BuffModel buffModel;
    public ChaState caster;
    public ChaState target;
    ///<summary>
    ///����ʱ�䣬�Ǹı仹������Ϊ, true��������Ϊ��false����ı�
    ///</summary>
    public bool durationSetTo;
    public bool permanent; // �Ƿ�����
    public int addStack;
    public int duration;

    public AddBuffInfo(BuffModel buffModel, ChaState caster, ChaState target, bool durationSetTo, bool permanent, int addStack, int duration) {
        this.buffModel = buffModel;
        this.caster = caster;
        this.target = target;
        this.durationSetTo = durationSetTo;
        this.permanent = permanent;
        this.addStack = addStack;
        this.duration = duration;
    }
}
