using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillModel {

    public int id;
    public ChaResource condition;
    public ChaResource cost;
    public AddBuffInfo[] buffInfos;

    public SkillModel(int id, ChaResource condition, ChaResource cost, AddBuffInfo[] buffInfos) {
        this.id = id;
        this.condition = condition;
        this.cost = cost;
        this.buffInfos = buffInfos;
    }
}
