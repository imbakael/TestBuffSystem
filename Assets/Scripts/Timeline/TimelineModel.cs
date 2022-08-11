
public class TimelineModel {

    public int id;
    public TimelineNode[] nodes;
    public float duration; // 固定值，timeline的持续时间

    public TimelineModel(int id, TimelineNode[] nodes, float duration) {
        this.id = id;
        this.nodes = nodes;
        this.duration = duration;
    }
}
