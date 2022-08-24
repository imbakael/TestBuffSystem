
public class ChaResource {
    public int hp;

    public ChaResource(int hp) {
        this.hp = hp;
    }

    public static ChaResource operator +(ChaResource a, ChaResource b) {
        return new ChaResource(
            a.hp + b.hp
        );
    }
}
