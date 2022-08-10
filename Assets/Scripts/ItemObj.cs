using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj {

    public ItemModel model;
    public int count;

    public ItemObj(ItemModel model, int count) {
        this.model = model;
        this.count = count;
    }
}
