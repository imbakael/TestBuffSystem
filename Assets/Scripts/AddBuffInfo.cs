using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuffInfo {

    public BuffModel buffModel;
    public ChaState caster;
    public ChaState target;
    public int addStack;
    ///<summary>
    ///����ʱ�䣬�Ǹı仹������Ϊ, true��������Ϊ��false����ı�
    ///</summary>
    public bool durationSetTo;
    public bool permanent;
    public int duration;

    ///<summary>
    ///buff��һЩ��������Щ�������߼�ʹ�õģ�����wow����ʦ�Ķܻ������ն����˺����Ϳ��Լ�¼��buffParam����
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
