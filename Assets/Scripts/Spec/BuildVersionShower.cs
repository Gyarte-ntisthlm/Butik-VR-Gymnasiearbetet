using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildVersionShower : MonoBehaviour
{
    private void Awake()
    {
        if (TryGetComponent<TMP_Text>(out TMP_Text text))
        {
            text.text = Application.version;
        }
    }
}
