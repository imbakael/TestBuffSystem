
public class TimelineModel {

    public int id;
    public TimelineNode[] nodes;
    public float duration; // �̶�ֵ��timeline�ĳ���ʱ��

    public TimelineModel(int id, TimelineNode[] nodes, float duration) {
        this.id = id;
        this.nodes = nodes;
        this.duration = duration;
    }
}
