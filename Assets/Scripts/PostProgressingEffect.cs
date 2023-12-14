using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProgressingEffect : MonoBehaviour
{
    [SerializeField] private float _speed = 2f; 
    private PostProcessVolume _processProfile;
    private Vignette _vignette;
    private LensDistortion _lensDistortion;
    private void Awake()
    {
        _processProfile = GetComponent<PostProcessVolume>(); 
    }

    public void SetPostEffects(bool state = false)
    {
        StartCoroutine(ChangePostEffects(state));
    }
    private IEnumerator ChangePostEffects(bool state)
    {
        _processProfile.profile.TryGetSettings(out _vignette);
        float valueVignette = state ? 0.3f : 0;

        _processProfile.profile.TryGetSettings(out _lensDistortion);
        float valueLensDistortion = state ? -50f : 0;
        while (_vignette.intensity.value != valueVignette)
        {
            _vignette.intensity.value = Mathf.MoveTowards(_vignette.intensity.value, valueVignette, Time.deltaTime * _speed);
            _lensDistortion.intensity.value = Mathf.MoveTowards(_lensDistortion.intensity.value, valueLensDistortion, Time.deltaTime * _speed);
            yield return null;
        }
    }
}
