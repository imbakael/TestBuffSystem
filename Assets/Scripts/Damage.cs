using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ҫͨ��DamageInfo���������޸�damage��ͨ������޸�damage�ﵽbuffЧ��
/// ���磺ĳ���˺����ڻ�+���Ļ���˺���{��100������50}���ܻ�������buff�ֵ�30%���˺�����ͨ����buff��damage�޸�Ϊ {��70������50}
/// </summary>
public class Damage {

    public int physics;
    public int fire;
    public int ice;
    public int thunder;
    public int poison;
    public int light;
    public int dark;

    public Damage(int physics = 0, int fire = 0, int ice = 0, int thunder = 0, int poison = 0, int light = 0, int dark = 0) {
        this.physics = physics;
        this.fire = fire;
        this.ice = ice;
        this.thunder = thunder;
        this.poison = poison;
        this.light = light;
        this.dark = dark;
    }

    public int OverAll() {
        return physics + fire + ice + thunder + poison + light + dark;
    }
}
