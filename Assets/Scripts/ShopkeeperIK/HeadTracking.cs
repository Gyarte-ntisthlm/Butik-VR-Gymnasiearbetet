using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HeadTracking : MonoBehaviour
{
    public Rig headRig;
    public Transform target;
    public float radius = 10f;
    List<HeadIKPointOfINtrest> POIs;

    // Skips sqr root calculation inherited from magnitude.
    private float sqrRadius;

    // Start is called before the first frame update
    void Start()
    {
        POIs = FindObjectsOfType<HeadIKPointOfINtrest>().ToList();
        sqrRadius = radius * radius;
    }

    // Update is called once per frame
    void Update()
    {
        Transform tracking = null;
        foreach (HeadIKPointOfINtrest poi in POIs)
        {
            Vector3 delta = poi.transform.position - transform.position;
            if (delta.sqrMagnitude < sqrRadius)
            {
                tracking = poi.transform;
                break;
            }
        }

        Vector3 fallBackTarget = transform.position + (transform.forward * 2f);
        
        // ngl, this shit is ugly. could be seperated into a few lines but eh, felt like a ternary today.
        target.position = Vector3.Lerp(target.position, tracking != null ? tracking.position : fallBackTarget, Time.deltaTime * 5f);

        // Head influence stuff.
        float rigWeight = 0f;
        rigWeight = tracking != null ? 1f : 0f;
        headRig.weight = Mathf.Lerp(headRig.weight, rigWeight, Time.deltaTime * 5f);
        
    }



    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    #endif
}
