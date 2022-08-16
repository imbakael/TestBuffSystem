using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum Camp {
        None,
        Player,
        Ally,
        Enemy,
        Neutrality
    }

    private Camp current = Camp.None;
    private List<bool> players = new List<bool> { false, false };
    private List<bool> allies = new List<bool> { false, false };
    private List<bool> enemies = new List<bool> { false, false };
    private List<bool> neutrality = new List<bool> { false, false };

    private void Update() {
        if (current == Camp.Player) {
            ActionCamp(players, Camp.Ally);
        } else if (current == Camp.Ally) {
            ActionCamp(allies, Camp.Enemy);
        } else if (current == Camp.Enemy) {
            ActionCamp(enemies, Camp.Neutrality);
        } else if (current == Camp.Neutrality) {
            ActionCamp(neutrality, Camp.Player);
        }
    }

    private void ActionCamp(List<bool> target, Camp next) {
        // 1.回合开始时要做的事：首先遍历全体buff列表，执行回合开始时触发的效果，如中毒的扣血、净化天赋的去除所有debuff效果

        // 2.回合中，玩家则等待玩家行动结束，AI则依次行动
        int actionCount = 0;
        for (int i = 0; i < target.Count; i++) {
            if (!target[i]) {
                // 当前此角色正在行动
            } else {
                actionCount++;
            }
        }
        // 3.回合结束时要做的事

        if (actionCount == target.Count) {
            ChangeCamp(next);
        }
    }

    private void ChangeCamp(Camp target) {
        current = target;
    }
}
