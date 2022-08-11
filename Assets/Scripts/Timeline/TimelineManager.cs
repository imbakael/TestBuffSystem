using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineManager : MonoBehaviour {

    private List<TimelineObj> timelines = new List<TimelineObj>();

    private void Update() {
        int index = 0;
        while (index < timelines.Count) {
            TimelineObj to = timelines[index];
            float lastTime = to.timeElapsed;
            to.timeElapsed += Time.deltaTime * to.timeScale;
            for (int i = 0; i < to.model.nodes.Length; i++) {
                TimelineNode node = to.model.nodes[i];
                if (node.triggerTime >= lastTime && node.triggerTime < to.timeElapsed) {
                    node.doEvent(to);
                }
            }

            if (to.timeElapsed >= to.model.duration) {
                timelines.RemoveAt(index);
            } else {
                index++;
            }
        }
    }
}
