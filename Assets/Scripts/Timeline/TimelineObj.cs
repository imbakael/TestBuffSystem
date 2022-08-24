
public class TimelineObj {

    public TimelineModel model;
    public ChaState caster;
    public float timeScale = 1f;
    public float timeElapsed = 0f; // 变量，累加timeline的持续时间

    public TimelineObj(TimelineModel model, ChaState caster) {
        this.model = model;
        this.caster = caster;
        timeScale = 1f;
    }
}
