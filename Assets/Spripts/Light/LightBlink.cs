using System.Collections;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    [SerializeField] private Light[] _lights;
    
    
    void Update()
    {
        foreach (Light light in _lights)
        {
            if (Random.value < Constants.DELAY)
            {
                light.enabled = !light.enabled;
                float delay = Random.Range(Constants.MIN_DELAY, Constants.HALF_OF_ONE);
                StartCoroutine(ResetLight(light, delay));
            }
        }
    }

    IEnumerator ResetLight(Light light, float delay)
    {
        yield return new WaitForSeconds(delay);
        light.enabled = !light.enabled;
    }
}
