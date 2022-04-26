using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject potPlantPrefab;
    [SerializeField] private int desiredPlants;
    
    private int _potPlantCount;

    private void Update()
    {
        if (_potPlantCount < desiredPlants)
        {
            SpawnPotPlant();
        }
    }

    public void UpdatePotPlantCount(int adjustment)
    {
        _potPlantCount += adjustment;
    }

    private void SpawnPotPlant()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-30, 30), 5, Random.Range(-30, 30));
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 180), 0);
        Instantiate(potPlantPrefab, randomPosition, randomRotation);
        _potPlantCount += 1;
    }
}