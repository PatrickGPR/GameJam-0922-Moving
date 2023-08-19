using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestruction : MonoBehaviour
{
    [SerializeField] float explosionForce;
    [SerializeField] Rigidbody[] childRBs;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyPackage(Vector3 explosionPosition)
    {
        
        
        for (int i = 0; i < childRBs.Length; i++)
        {
            childRBs[i].AddExplosionForce(explosionForce, explosionPosition - new Vector3(0f, 1f, 0f), 5f);
        }
    }
}
