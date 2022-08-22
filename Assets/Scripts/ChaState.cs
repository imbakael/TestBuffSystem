using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaState {

    public bool IsDead => resource.hp <= 0;

    public ChaProperty currentProp;
    public ChaResource resource;
    public List<BuffObj> buffs;
    private List<EquipmentObj> equipments;

    private ChaProperty baseProp; // 基础属性，可成长

    public ChaState(ChaProperty baseProp) {
        this.baseProp = baseProp;
        buffs = new List<BuffObj>();
        equipments = new List<EquipmentObj>();
        RecheckProperty();
        resource = new ChaResource(currentProp.hp);
    }

    public bool CanBeKilled(DamageInfo damageInfo) {
        int damage = DamageManager.GetDamageValue(damageInfo, currentProp);
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
            int realAfterStack = Mathf.Min(afterStack, addBuffInfo.buffModel.maxStack);
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
            int modifyStack = Mathf.Min(addBuffInfo.addStack, addBuffInfo.buffModel.maxStack);
            addBuffInfo.buffModel.onOccur?.Invoke(buff, modifyStack);
        }
        RecheckProperty();
    }

    public void TickBuff() {
        for (int i = 0; i < buffs.Count; i++) {
            BuffObj buff = buffs[i];
            buff.model.onTick?.Invoke(buff);
        }
    }

    public void RemoveBuff(Func<BuffObj, bool> filter) {
        int index = 0;
        int originalBuffCount = buffs.Count;
        while (index < buffs.Count) {
            BuffObj buff = buffs[index];
            if (filter(buff)) {
                buffs.RemoveAt(index);
                buff.model.onRemoved?.Invoke(buff);
                continue;
            }
            index++;
        }
        if (buffs.Count < originalBuffCount) {
            RecheckProperty();
        }
    }

    private BuffObj GetBuff(int buffId) {
        return buffs.Where(t => t.model.id == buffId).FirstOrDefault();
    }

    private void RecheckProperty() {
        currentProp.Zero();
        ChaProperty buffProp = ChaProperty.NewZero();
        for (int i = 0; i < buffs.Count; i++) {
            BuffObj buff = buffs[i];
            buffProp += buff.model.prop * buff.stack;
        }
        ChaProperty equipmentProp = ChaProperty.NewZero();
        for (int i = 0; i < equipments.Count; i++) {
            EquipmentObj eo = equipments[i];
            equipmentProp += eo.model.prop;
        }
        currentProp = baseProp + buffProp + equipmentProp;
    }
}
