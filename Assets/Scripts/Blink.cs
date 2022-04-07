using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    [SerializeField] private Image image;

    private void Start()
    {
        image.CrossFadeAlpha(0, 0f, true);
    }

    public void Blink_()
    {
        // Start the corutine
        StartCoroutine(BlinkCoroutine());
    }

    public void Blind()
    {
        // Start the corutine
        StartCoroutine(BlindCoroutine());
    }

    IEnumerator BlinkCoroutine(){
        image.color = Color.black;
        // Change the opacity of the image by lerping from 0 to 1 then back to 0, it should in total take 1 second.
        image.CrossFadeAlpha(1, 0.05f, true);
        yield return new WaitForSeconds(0.05f);
        image.CrossFadeAlpha(0, 0.1f, true);
        Debug.Log("Blink");
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator BlindCoroutine(){
        // Turn the image/panel white
        image.color = Color.white;
        // Change the opacity of the image by lerping from 0 to 1 then back to 0, it should in total take 1 second.

        image.CrossFadeAlpha(1, 0.4f, true);
        Debug.Log("Blind");
        yield return new WaitForSeconds(1f);
    }
}
