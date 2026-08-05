using UnityEngine;
using System.Collections;

public class AlarmLightBlink : MonoBehaviour
{
    public Light alarmLight;

    public float flashDuration = 0.1f;
    public float flashGap = 0.1f;
    public float pauseDuration = 0.8f;

    private void Start()
    {
        if (alarmLight == null)
            alarmLight = GetComponent<Light>();

        StartCoroutine(BlinkPattern());
    }

    IEnumerator BlinkPattern()
    {
        while (true)
        {
            alarmLight.enabled = true;
            yield return new WaitForSeconds(flashDuration);

            alarmLight.enabled = false;
            yield return new WaitForSeconds(flashGap);

            alarmLight.enabled = true;
            yield return new WaitForSeconds(flashDuration);

            alarmLight.enabled = false;
            yield return new WaitForSeconds(pauseDuration);
        }
    }
}