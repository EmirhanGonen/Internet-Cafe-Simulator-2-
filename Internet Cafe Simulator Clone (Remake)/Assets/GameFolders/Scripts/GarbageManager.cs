using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class GarbageManager : MonoBehaviour
{
    [SerializeField] private List<Garbage> _garbages;
    private Garbage GetRandomGarbage() => _garbages[Random.Range(0,_garbages.Count)];

    [SerializeField] private Transform _leftTopCorner, _rightDownCorner;
    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(_rightDownCorner.position.x, _leftTopCorner.position.x);

        float randomZ = Random.Range(_rightDownCorner.position.z, _leftTopCorner.position.z);

        return new(randomX, -.78f, randomZ);
    }


    private void Start()
    {
        Invoke(nameof(Spawn), Random.Range(5, 7));
    }

    [Button("Spawn")]
    private void Spawn()
    {
        _ = Instantiate(GetRandomGarbage(), GetRandomSpawnPosition(), Quaternion.identity);
        Invoke(nameof(Spawn), Random.Range(5, 7));
    }
}