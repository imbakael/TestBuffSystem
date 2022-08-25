using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentModel {

    public int id;
    public string name;
    public EquipmentSlot slot;
    public ChaProperty prop;
    public SkillModel[] skills;
    public AddBuffInfo[] addBuffInfos;

    public EquipmentModel(int id, string name, EquipmentSlot slot, ChaProperty prop, SkillModel[] skills, AddBuffInfo[] addBuffInfos) {
        this.id = id;
        this.name = name;
        this.slot = slot;
        this.prop = prop;
        this.skills = skills;
        this.addBuffInfos = addBuffInfos;
    }
}
