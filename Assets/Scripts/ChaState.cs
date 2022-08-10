using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaState {

    public bool IsDead => resource.hp <= 0;

    public ChaProperty currentProp;

    private ChaProperty baseProp; // 基础属性，可成长
    private ChaProperty buffProp; // buff带来的属性
    private ChaProperty equipmentProp; // 装备属性
    private ChaResource resource;

    public List<BuffObj> buffs = new List<BuffObj>();

    public bool CanBeKilled(DamageInfo damageInfo) {
        int damage = DamageInfo.GetDamageValue(currentProp, damageInfo);
        return resource.hp <= damage;
    }

    public void ModifyResource(ChaResource chaResource) {
        resource += chaResource;
        resource.hp = Mathf.Clamp(resource.hp, 0, currentProp.hp);
        if (resource.hp <= 0) {
            // 自身死亡
        }
    }

    public void AddBuff(AddBuffInfo addBuffInfo) {
        BuffObj theBuff = GetBuff(addBuffInfo.buffModel.id);
        if (theBuff != null) {
            theBuff.duration = addBuffInfo.durationSetTo ? addBuffInfo.duration : addBuffInfo.duration + theBuff.duration;
            theBuff.permanent = addBuffInfo.permanent;
            int afterStack = addBuffInfo.addStack + theBuff.stack;
            int realAfterStack = Mathf.Clamp(afterStack, 0, addBuffInfo.buffModel.maxStack);
            int deltaStack = realAfterStack - theBuff.stack;
            theBuff.stack += deltaStack;
            if (theBuff.stack > 0 && deltaStack != 0) {
                addBuffInfo.buffModel.onOccur?.Invoke(theBuff, deltaStack);
            }
        } else {
            var buff = new BuffObj(addBuffInfo.buffModel, addBuffInfo.caster, addBuffInfo.caster, 
                addBuffInfo.permanent, addBuffInfo.addStack, addBuffInfo.duration);
            buffs.Add(buff);
            buffs.Sort((a, b) => -a.model.priority.CompareTo(b.model.priority)); // 降序，先执行优先级高的buff
            int modifyStack = Mathf.Clamp(addBuffInfo.addStack, 0, addBuffInfo.buffModel.maxStack);
            addBuffInfo.buffModel.onOccur?.Invoke(buff, modifyStack);
        }
    }

    private BuffObj GetBuff(int buffId) {
        return buffs.Where(t => t.model.id == buffId).FirstOrDefault();
    }
}
