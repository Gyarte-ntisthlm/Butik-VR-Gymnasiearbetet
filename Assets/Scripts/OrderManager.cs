using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.onDecideOrder += OnDecideOrder;
    }

    private void OnDecideOrder()
    {
        // Randomize the order between A-B and B-A
        int order = Random.Range(0, 2);

    }
}
