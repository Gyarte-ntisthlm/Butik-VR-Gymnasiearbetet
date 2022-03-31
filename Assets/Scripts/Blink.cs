using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    [SerializeField] private Image image;


    public void Blink_()
    {
        // Start the corutine
        StartCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine(){
        // Change the opacity of the image by lerping from 0 to 1 then back to 0, it should in total take 1 second.
        image.CrossFadeAlpha(1, 0.2f, true);
        yield return new WaitForSeconds(0.2f);
        image.CrossFadeAlpha(0, 0.5f, true);
        Debug.Log("Blink");
        yield return new WaitForSeconds(0.5f);
    }
}
