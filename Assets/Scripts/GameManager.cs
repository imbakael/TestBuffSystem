using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //public enum Camp {
    //    None,
    //    Player,
    //    Ally,
    //    Enemy,
    //    Neutrality
    //}

    private List<ChaState> players = new List<ChaState>();
    private List<ChaState> allies = new List<ChaState>();
    private List<ChaState> enemies = new List<ChaState>();
    private List<ChaState> neutrality = new List<ChaState>();

    public void StartTurn() {
        StartCoroutine(TurnLoop());
    }

    private IEnumerator TurnLoop() {
        while (true) {
            yield return ActionCamp(players);
            yield return ActionCamp(allies);
            yield return ActionCamp(enemies);
            yield return ActionCamp(neutrality);
        }
    }

    private IEnumerator ActionCamp(List<ChaState> target) {
        // 1.回合开始时要做的事：执行回合开始时触发的效果，如中毒的扣血
        for (int i = 0; i < target.Count; i++) {
            ChaState cs = target[i];
            cs.TickBuff();
            yield return new WaitForSeconds(0.5f);
        }

        // 2.回合中，玩家则等待玩家行动结束，AI则依次行动
        int actionCount = 0;
        for (int i = 0; i < target.Count; i++) {
            if (target[i] != null) {
                // 当前此角色正在行动
            } else {
                actionCount++;
            }
            yield return new WaitForSeconds(0.5f);
        }

        // 3.回合结束时要做的事，如有持续时间的buff持续时间-1
        for (int i = 0; i < target.Count; i++) {
            ChaState cs = target[i];
            cs.RemoveBuff((buff) => {
                if (!buff.permanent) {
                    buff.timeElapsed += 1;
                    return buff.timeElapsed >= buff.duration;
                }
                return false;
            });
            yield return new WaitForSeconds(0.5f);
        }
    }

}
