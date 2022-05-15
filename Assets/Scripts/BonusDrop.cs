using UnityEngine;

public class BonusDrop : MonoBehaviour
{
    [SerializeField] private GameObject BonusPrefab;
    
    public void OnEntityDeath(Entity entity)
    {
        Instantiate(BonusPrefab, transform.position, transform.rotation);
    }

}
