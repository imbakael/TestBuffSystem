using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuffOnOccurCallbacks {
    public static readonly Dictionary<string, BuffOnOccur> OnOccur = new Dictionary<string, BuffOnOccur> {
        { "钢铁之躯", IronBody }
    };

    // 提升6点防御和20%物理抗性
    private static void IronBody(BuffObj buff, int modifyStack) {
        var addBuffInfo = new AddBuffInfo(buff.model, buff.carrier, buff.carrier, true, true, 1, -1);
        buff.carrier.AddBuff(addBuffInfo);
    }
}
