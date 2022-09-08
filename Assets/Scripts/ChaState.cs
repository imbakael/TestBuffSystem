using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaState {

    public bool IsDead => resource.hp <= 0;

    public ChaProperty currentProp; // 由基础属性+装备属性+buff属性组成
    public ChaResource resource;
    public List<BuffObj> buffs;

    private ChaProperty baseProp; // 基础属性，可成长
    private List<EquipmentObj> equipments;
    private List<EquipmentObj> notEquipments;
    private List<ItemObj> items;

    public ChaState(ChaProperty baseProp) {
        this.baseProp = baseProp;
        buffs = new List<BuffObj>();
        equipments = new List<EquipmentObj>();
        notEquipments = new List<EquipmentObj>();
        items = new List<ItemObj>();
        currentProp = ChaProperty.NewZero();
        RecheckProperty();
        resource = new ChaResource(currentProp.hp);
    }

    public void Equip(EquipmentObj eo) {
        EquipmentObj beforeEo = equipments.Where(t => t.model.slot == eo.model.slot).FirstOrDefault();
        if (beforeEo != null) {
            DisarmEquipment(beforeEo);
        }
        equipments.Add(eo);
        notEquipments.Remove(eo);
        if (eo.model.addBuffInfos != null) {
            for (int i = 0; i < eo.model.addBuffInfos.Length; i++) {
                AddBuff(eo.model.addBuffInfos[i]);
            }
        }
        RecheckProperty();
    }

    public void DisarmEquipment(EquipmentObj eo) {
        equipments.Remove(eo);
        notEquipments.Add(eo);
        if (eo.model.addBuffInfos != null) {
            RemoveBuff((t) => IsContainBuffId(eo.model.addBuffInfos, t.model.id));
        }
        RecheckProperty();
    }

    private bool IsContainBuffId(AddBuffInfo[] infos, int targetId) {
        return infos.Any(t => t.model.id == targetId);
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

    public void AddBuff(AddBuffInfo buffInfo) {
        BuffObj theBuff = GetBuff(buffInfo.model.id);
        if (theBuff != null) {
            theBuff.duration = buffInfo.durationSetTo ? buffInfo.duration : buffInfo.duration + theBuff.duration;
            theBuff.permanent = buffInfo.permanent;
            int afterStack = buffInfo.addStack + theBuff.stack;
            int realAfterStack = Mathf.Min(afterStack, buffInfo.model.maxStack);
            int deltaStack = realAfterStack - theBuff.stack;
            theBuff.stack += deltaStack;
            if (theBuff.stack > 0 && deltaStack != 0) {
                buffInfo.model.onOccur?.Invoke(theBuff, deltaStack);
            }
        } else {
            var buff = new BuffObj(buffInfo.model, buffInfo.caster, buffInfo.caster, 
                buffInfo.permanent, buffInfo.addStack, buffInfo.duration);
            buffs.Add(buff);
            buffs.Sort((a, b) => -a.model.priority.CompareTo(b.model.priority));
            int modifyStack = Mathf.Min(buffInfo.addStack, buffInfo.model.maxStack);
            buffInfo.model.onOccur?.Invoke(buff, modifyStack);
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
        bool isBuffRemoved = false;
        while (index < buffs.Count) {
            BuffObj buff = buffs[index];
            if (filter(buff)) {
                buffs.RemoveAt(index);
                buff.model.onRemoved?.Invoke(buff);
                isBuffRemoved = true;
                continue;
            }
            index++;
        }
        if (isBuffRemoved) {
            RecheckProperty();
        }
    }

    private BuffObj GetBuff(int buffId) {
        return buffs.Where(t => t.model.id == buffId).FirstOrDefault();
    }

    // buff变化或者装备变化都会影响自身的属性
    private void RecheckProperty() {
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
        currentProp.Zero();
        currentProp = baseProp + buffProp + equipmentProp;
    }
}
