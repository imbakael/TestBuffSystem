using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj {
    public SkillModel model;
    public int level;
    public int cd;

    public SkillObj(SkillModel model, int level, int cd) {
        this.model = model;
        this.level = level;
        this.cd = cd;
    }
}
