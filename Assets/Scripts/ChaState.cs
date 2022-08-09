using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaState {

    private ChaProperty baseProp;
    private ChaProperty buffProp;
    private ChaProperty equipmentProp;

    public List<BuffObj> buffs = new List<BuffObj>();

    public void AddBuff(AddBuffInfo addBuffInfo) {
        BuffObj theBuff = GetBuff(addBuffInfo.buffModel.id);
        if (theBuff != null) {
            theBuff.duration = addBuffInfo.durationSetTo ? addBuffInfo.duration : addBuffInfo.duration + theBuff.duration;
            theBuff.permanent = addBuffInfo.permanent;
            int afterStack = addBuffInfo.addStack + theBuff.stack;
            int realAfterStack = Mathf.Clamp(afterStack, 0, addBuffInfo.buffModel.maxStack);
            int modifyStack = realAfterStack - theBuff.stack;
            theBuff.stack += modifyStack;
            if (theBuff.stack > 0 && modifyStack != 0) {
                addBuffInfo.buffModel.onOccur?.Invoke(theBuff, modifyStack);
            }
        } else {
            var buff = new BuffObj(addBuffInfo.buffModel, addBuffInfo.caster, addBuffInfo.caster, 
                addBuffInfo.permanent, addBuffInfo.addStack, addBuffInfo.duration);
            buffs.Add(buff);
            buffs.Sort((a, b) => -a.buffModel.priority.CompareTo(b.buffModel.priority)); // 降序，优先执行优先级高的buff
            int modifyStack = Mathf.Clamp(addBuffInfo.addStack, 0, addBuffInfo.buffModel.maxStack);
            addBuffInfo.buffModel.onOccur?.Invoke(buff, modifyStack);
        }
    }

    private BuffObj GetBuff(int buffId) {
        return buffs.Where(t => t.buffModel.id == buffId).FirstOrDefault();
    }
}
