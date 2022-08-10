using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel {

    public int id;
    public string name;
    public int maxStack;

    public ItemModel(int id, string name, int maxStack) {
        this.id = id;
        this.name = name;
        this.maxStack = maxStack;
    }
}
