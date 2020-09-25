
using UnityEngine;

public class CloudSimulation : MonoBehaviour
{
    [SerializeField] private float simulationTime = 15;
    void Start()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Simulate(15);
        particleSystem.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
