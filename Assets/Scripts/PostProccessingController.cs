using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PostProccessingController : MonoBehaviour
{
    private PostProcessVolume m_PostProcessVolume;

    [Header("Time before Respond")]
    [SerializeField] public float respondTime = 1f;

    float colorGrade = 60f;
    float high;
    ColorGrading colorGrading = null;
    LensDistortion lensDistortion = null;
    ChromaticAberration chromaticAberration = null;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.PlayerLostLife += lifeLost;
        GameEvents.current.PlayerKilled += noLifes;

        m_PostProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        m_PostProcessVolume.profile.TryGetSettings(out lensDistortion);
        m_PostProcessVolume.profile.TryGetSettings(out colorGrading);
        m_PostProcessVolume.profile.TryGetSettings(out chromaticAberration);

    }

    void Update()
    {
        high = GameStates.current.High;

        colorGrading.temperature.value = high * colorGrade;
        colorGrading.tint.value = high * colorGrade;
        colorGrading.hueShift.value += .1f* high;

        lensDistortion.intensity.value = 1 + (high * colorGrade);
        chromaticAberration.intensity.value = high;
    }

    void lifeLost(int liveLeft)
    {
        if(liveLeft > 0)
            StartCoroutine("blindPlayer", 0.01f);
    }

    void noLifes(int life)
    {
        Debug.Log("its the end");
        StartCoroutine("EndPlayer", 0.01f);
    }

    IEnumerator blindPlayer(float waitTime)
    {
        yield return new WaitForSeconds(.2f);

        while (lensDistortion.scale.value >= 0.01f)
        {
            lensDistortion.scale.value -= 0.05f;
            yield return new WaitForSeconds(waitTime);
        }
        yield return new WaitForSeconds(.5f);

        while (lensDistortion.scale.value <= 1.35f)
        {
            lensDistortion.scale.value += 0.05f;
            yield return new WaitForSeconds(waitTime);
        }
    }
    IEnumerator EndPlayer(float waitTime)
    {
        yield return new WaitForSeconds(respondTime);

        while (lensDistortion.scale.value >= 0.01f)
        {
            lensDistortion.scale.value -= 0.05f;
            yield return new WaitForSeconds(waitTime);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
