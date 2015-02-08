using UnityEngine;
using System.Collections;

public class AOEAttackScript : MonoBehaviour {
    
    public GameObject aoeMesh;

    void Start()
    {
        if (aoeMesh == null)
        {
            UnityEngine.Debug.LogError("No mesh given to AOE!");
            this.enabled = false;
        }
    }

    //spawns an AOE trigger collider with the stated paramiters
    public void aoeAttack(Transform trans, float damage, float range, Color color)
    {
        GameObject aoe = Instantiate(aoeMesh, trans.position, Quaternion.AngleAxis(90, Vector3.left)) as GameObject;
        aoe.transform.parent = trans;
        aoe.transform.localPosition = new Vector3(0, -trans.collider.bounds.extents.y, 0);
        aoe.transform.localScale = Vector3.one * range;

        aoe.AddComponent<MeshCollider>();
        MeshCollider meshCollider = aoe.GetComponent<MeshCollider>();        

        meshCollider.isTrigger = true;


    }
}
