
public class BuffObj {
    public BuffModel model;
    public ChaState caster; // buff释放者
    public ChaState carrier; // buff携带者
    public bool permanent;
    public int stack; // 层数，至少1层
    public int duration; // 持续多少回合
    public int timeElapsed = 0; // 已经存在多少回合

    public BuffObj(BuffModel model, ChaState caster, ChaState carrier, bool permanent, int stack, int duration) {
        this.model = model;
        this.caster = caster;
        this.carrier = carrier;
        this.permanent = permanent;
        this.stack = stack;
        this.duration = duration;
    }
}

