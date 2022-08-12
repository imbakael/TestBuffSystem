using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour {
    // ÿ��buff��Ӧ�Ĵ���Ȩ��
    private List<int> test = new List<int> { 20, 33, 5, 22, 10, 40, 8 };

    private void Start() {
        
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("index = " + GetRandomIndex(test));
        }
    }

    private int GetRandomIndex(List<int> target) {
        int sum = target.Sum();
        int random = Random.Range(1, sum);
        int curSum = 0;
        for (int i = 0; i < target.Count; i++) {
            curSum += target[i];
            if (random <= curSum) {
                return i;
            }
        }
        Debug.LogError("ȡ���������");
        return -1;
    }
}
