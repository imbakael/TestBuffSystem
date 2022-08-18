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
        // 1.�غϿ�ʼʱҪ�����£�ִ�лغϿ�ʼʱ������Ч�������ж��Ŀ�Ѫ
        for (int i = 0; i < target.Count; i++) {
            ChaState cs = target[i];
            cs.TickBuff();
            yield return new WaitForSeconds(0.5f);
        }

        // 2.�غ��У������ȴ�����ж�������AI�������ж�
        int actionCount = 0;
        for (int i = 0; i < target.Count; i++) {
            if (target[i] != null) {
                // ��ǰ�˽�ɫ�����ж�
            } else {
                actionCount++;
            }
            yield return new WaitForSeconds(0.5f);
        }

        // 3.�غϽ���ʱҪ�����£����г���ʱ���buff����ʱ��-1
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
