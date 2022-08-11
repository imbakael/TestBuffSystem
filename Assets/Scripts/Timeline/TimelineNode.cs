
public class TimelineNode {

    public float triggerTime;
    public TimelineEvent doEvent;

    public TimelineNode(float triggerTime, TimelineEvent doEvent) {
        this.triggerTime = triggerTime;
        this.doEvent = doEvent;
    }
}
