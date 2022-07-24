using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StardropTools.Pool;

namespace StardropTools.Test
{
    public class TestPool : MonoBehaviour
    {
        [SerializeField] Transform pooled;
        [SerializeField] Transform spawned;
        [Space]
        [SerializeField] GameObject prefab;
        [SerializeField] Pool<Cube> poolCubes;
        [SerializeField] float radius;
        [SerializeField] float spawnTimer = 1;
        [SerializeField] List<Cube> cubes;

        bool isSpawning;
        float time;

        private void Start()
        {
            poolCubes = new Pool<Cube>(prefab, 10, pooled, true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (isSpawning)
                    isSpawning = false;

                else
                {
                    SpawnCube();
                    isSpawning = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                poolCubes.DespawnAll(true);
                cubes = UtilsArray.RemoveEmpty(cubes);
            }

            if (isSpawning)
            {
                time += Time.deltaTime;

                if (time > spawnTimer)
                {
                    SpawnCube();
                    time = 0;
                }
            }
        }

        public void SpawnCube()
        {
            Vector3 pos = Random.insideUnitSphere * radius;
            var cube = poolCubes.Spawn(pos, Quaternion.identity, spawned);

            if (cubes.Contains(cube) == false)
                cubes.Add(cube);
        }
    }
}

