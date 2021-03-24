using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _snakes;
    [SerializeField] private GameObject _tail;
    [SerializeField] private int _snakeNumberToSpawn;
    private void Awake()
    {
        Instantiate(_tail, transform.position + new Vector3(0f, 0f, -0.15f), transform.rotation);

        Instantiate(_snakes[_snakeNumberToSpawn], transform.position, transform.rotation);
    }
}
