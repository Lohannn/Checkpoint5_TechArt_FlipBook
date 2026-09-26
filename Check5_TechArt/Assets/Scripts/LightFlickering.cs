using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public Light[] fireLights;

    public float minMultiplier = 0.2f;
    public float maxMultiplier = 1.8f;
    public float flickerSpeed = 3.5f;

    public float actionFps = 12f;

    public float moveRadius = 0.15f; 
    public float moveSpeed = 2.0f; 

    private float[] baseIntensities;
    private Vector3[] basePositions;
    private float randomOffset;

    void Start()
    {
        randomOffset = Random.Range(0f, 100f);
        
        baseIntensities = new float[fireLights.Length];
        basePositions = new Vector3[fireLights.Length];

        for (int i = 0; i < fireLights.Length; i++)
        {
            if (fireLights[i] != null)
            {
                baseIntensities[i] = fireLights[i].intensity;
                basePositions[i] = fireLights[i].transform.localPosition; 
            }
        }
    }

    //Otimização da IA para deixar a movimentção e flick da luz menos fluída 
    // "tem como fazer ser um pouco menos fluido as movimentações e flickering?"
    void Update()
    {
        float steppedTime = Mathf.Floor(Time.time * actionFps) / actionFps;

        float intensityNoise = Mathf.PerlinNoise(steppedTime * flickerSpeed + randomOffset, 0f);
        float currentMultiplier = Mathf.Lerp(minMultiplier, maxMultiplier, intensityNoise);

        float timeMove = steppedTime * moveSpeed + randomOffset;
        
        float offsetX = (Mathf.PerlinNoise(timeMove, 0f) - 0.5f) * 2f;
        float offsetY = (Mathf.PerlinNoise(0f, timeMove) - 0.5f) * 2f;
        float offsetZ = (Mathf.PerlinNoise(timeMove, 100f) - 0.5f) * 2f; 
        
        Vector3 movementOffset = new Vector3(offsetX, offsetY, offsetZ) * moveRadius;

        for (int i = 0; i < fireLights.Length; i++)
        {
            if (fireLights[i] != null)
            {
                fireLights[i].intensity = baseIntensities[i] * currentMultiplier;
                fireLights[i].transform.localPosition = basePositions[i] + movementOffset;
            }
        }
    }
}
