using UnityEngine;

public class Node : MonoBehaviour
{
    private GameObject child;
    [SerializeField]private Transform spawnPoint;
    
    
    
    public void SetChild(GameObject child)
    {
        this.child = child;
        child.transform.parent = spawnPoint;
    }
}
