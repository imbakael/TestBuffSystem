
public class TimelineObj {

    public TimelineModel model;
    public ChaState caster;
    public float timeScale = 1f;
    public float timeElapsed = 0f; // �������ۼ�timeline�ĳ���ʱ��

    public TimelineObj(TimelineModel model, ChaState caster) {
        this.model = model;
        this.caster = caster;
        timeScale = 1f;
    }
}
