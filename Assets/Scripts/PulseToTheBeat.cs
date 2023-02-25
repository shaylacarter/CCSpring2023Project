using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseToTheBeat : MonoBehaviour
{

    [SerializeField] bool _useTestBeat;
    [SerializeField] float _pulseSize = 1.15f;
    [SerializeField] float _returnSpeed = 5f;
    private Vector3 _startSize;

    [SerializeField] private AudioClip[] _soundEffects;
    [SerializeField] private AudioSource _audioSource;
    private int _currentSoundEffect;
    public float volume = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _startSize = transform.localScale;
        if (_useTestBeat) {
            StartCoroutine(TestBeat());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _startSize, Time.deltaTime * _returnSpeed);
    }

    public void Pulse(){
        transform.localScale = _startSize * _pulseSize;
        
        if (_soundEffects.Length > 0) {
            _audioSource.PlayOneShot(_soundEffects[_currentSoundEffect], volume);
            _currentSoundEffect = (_currentSoundEffect + 1) % _soundEffects.Length;
        }
    }

    IEnumerator TestBeat() {
        while (true) {
            yield return new WaitForSeconds(1f);
            Pulse();
        }
    }
}
