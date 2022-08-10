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
        int actionCount = 0;
        for (int i = 0; i < target.Count; i++) {
            if (!target[i]) {
                // 当前此角色正在行动
            } else {
                actionCount++;
            }
        }
        if (actionCount == target.Count) {
            ChangeCamp(next);
        }
    }

    private void ChangeCamp(Camp target) {
        current = target;
    }
}
