using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuffInfo {

    public BuffModel model;
    public ChaState caster;
    public ChaState target;
    ///<summary>
    ///����ʱ�䣬�Ǹı仹������Ϊ, true��������Ϊ��false����ı�
    ///</summary>
    public bool durationSetTo;
    public bool permanent; // �Ƿ�����
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
