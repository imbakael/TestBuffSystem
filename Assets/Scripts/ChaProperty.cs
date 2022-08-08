using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaProperty {

    public int hp;
    public int strength;
    public int defence;
    public int elemStrength;
    public int elemDefence;
    public int speed;
    public int skill;
    public int luck;
    public int con; // Меёс

    public int physicsResist;
    public int fireResist;
    public int iceResist;
    public int thunderResist;
    public int poisonResist;
    public int lightResist;
    public int darkResist;

    public ChaProperty(int hp, int strength, int defence, int elemStrength, int elemDefence, int speed, int skill, int luck, int con,
        int physicsResist, int fireResist, int iceResist, int thunderResist, int poisonResist, int lightResist, int darkResist) {
        this.hp = hp;
        this.strength = strength;
        this.defence = defence;
        this.elemStrength = elemStrength;
        this.elemDefence = elemDefence;
        this.speed = speed;
        this.skill = skill;
        this.luck = luck;
        this.con = con;
        this.physicsResist = physicsResist;
        this.fireResist = fireResist;
        this.iceResist = iceResist;
        this.thunderResist = thunderResist;
        this.poisonResist = poisonResist;
        this.lightResist = lightResist;
        this.darkResist = darkResist;
    }
}
