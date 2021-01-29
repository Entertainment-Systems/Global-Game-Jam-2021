using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProccessingController : MonoBehaviour
{
    private PostProcessVolume m_PostProcessVolume;
    float colorGrade = 100f;
    float high;
    ColorGrading colorGrading = null;
    LensDistortion lensDistortion = null;

    // Start is called before the first frame update
    void Start()
    {
        m_PostProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        m_PostProcessVolume.profile.TryGetSettings(out lensDistortion);
        m_PostProcessVolume.profile.TryGetSettings(out colorGrading);

    }

    // Update is called once per frame
    void Update()
    {
        high = GameStates.current.High;

        colorGrading.temperature.value = high * colorGrade;
        colorGrading.tint.value = high * colorGrade;
        colorGrading.hueShift.value = high * colorGrade;
        lensDistortion.scale.value = 1 - high;
    }
}
