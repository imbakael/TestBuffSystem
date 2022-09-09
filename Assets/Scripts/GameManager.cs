using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum Camp {
        Player,
        Ally,
        Enemy,
        Neutrality
    }

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
        // 1.回合开始时要做的事：执行回合开始时触发的效果，如中毒的扣血、回血类天赋
        for (int i = 0; i < target.Count; i++) {
            ChaState cs = target[i];
            yield return SwitchCameraTo(cs);
            cs.TickBuff();
            yield return new WaitForSeconds(0.5f);
        }

        // 2.回合中，玩家则等待玩家行动结束，AI则依次行动（移动、攻击、释放技能、待机）
        for (int i = 0; i < target.Count; i++) {
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

    private IEnumerator SwitchCameraTo(ChaState target) {
        yield return new WaitForSeconds(0.5f);
    }

}
