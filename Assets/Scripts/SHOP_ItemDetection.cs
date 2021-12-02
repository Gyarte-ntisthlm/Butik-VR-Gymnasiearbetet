using System.Collections.Generic;
using UnityEngine;

public class SHOP_ItemDetection : MonoBehaviour
{
    // Due to now knowing in what oder the items are removed in, the GameObject is both the reference and the value. 
    public Dictionary<GameObject,GameObject> items = new Dictionary<GameObject, GameObject>(); 

    Material m_Material;

    [SerializeField]
    Color hasItemsColor = Color.green;
   
    [SerializeField]
    Color emptyColor = Color.red;

    private void Start() {
        m_Material = GetComponent<MeshRenderer>().sharedMaterial;
        m_Material.SetColor("_Color", emptyColor);
    }

    private void OnTriggerEnter(Collider other) {
        items.Add(other.gameObject, other.gameObject);
        
        if (items.Count == 1)
            m_Material.SetColor("_Color", hasItemsColor);
    }

    private void OnTriggerExit(Collider other) {
        items.Remove(other.gameObject);

        if (items.Count == 0)
            m_Material.SetColor("_Color", emptyColor);
    }
}
