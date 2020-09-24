using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    [SerializeField] GameObject cube;

    float plainX;
    float plainZ;

    public int columns = 10;
    public int rows = 10;

    private float startPosX = -5;
    private float startPosZ = -5;

    private List<Transform> cubes = new List<Transform>();
    private float speed;


    MeshRenderer meshRend;

    private void Awake()
    {
        meshRend = GetComponent<MeshRenderer>();
        plainX = meshRend.bounds.size.x;
        plainZ = meshRend.bounds.size.z;
    }

    void Start()
    {

        RandomGenerate();

    }
    private void Update()
    {
        MoveCity();
    }

    void RandomGenerate()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                GameObject newCube = Instantiate(cube, this.transform);
                newCube.transform.localPosition = new Vector3(startPosX + x * (plainX / (columns * transform.localScale.x)), 0, startPosZ + z * (plainZ / (rows * transform.localScale.z)));
                newCube.transform.localScale = new Vector3(0.5f, Random.Range(100, 150), 0.5f);
                /*
                Color color = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f)
                    );

                newCube.transform.GetComponent<MeshRenderer>().material.color = color;
                */

                /*
                if (newCube.transform.localPosition.x == 0)
                {
                    Destroy(newCube);
                }
                else
                {
                    cubes.Add(newCube.transform);
                }
                */
                cubes.Add(newCube.transform);
            }
        }
    }

    void MoveCity()
    {
        speed = GameManager.Instance.worldSpeed;
        foreach (Transform cube in cubes)
        {
            cube.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
            if (cube.localPosition.z < -5)
                cube.localPosition = new Vector3(cube.localPosition.x, cube.localPosition.y, 5);
        }
    }


}
