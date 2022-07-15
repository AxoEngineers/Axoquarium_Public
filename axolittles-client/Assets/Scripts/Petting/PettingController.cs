using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PettingController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public AxoClickHandler axoClickHandler;
    [Header("Mood Progress Bar")] public GameObject moodBarGameobject;
    public Slider moodIndicatorSlider;
    public Image fill;
    public Gradient gradient;
    public Sprite[] moodImages;
    private Image _smileImage;

    [NonSerialized] public float lerpedValue;


    private readonly float moodPointLongAnimation = 2f;
    private float _currentMoodValue = 2f;
    private readonly float maxMoodValue = 10f;


    public Animator Anim { get; private set; }

    private bool _firstCoroutineStarted;
    private bool _secondCoroutineStarted;

    private Camera _mainCamera;
    private Axolittle _axolittleScript;

    //Rotation fields
    private Vector3 _cameraRotation;
    private Quaternion _targetRotation;
   

    private CapsuleCollider _axoCollider;

    //Animator parameters
    private readonly int _first = Animator.StringToHash("First");
    private readonly int _second = Animator.StringToHash("Second");
    private readonly int _moving = Animator.StringToHash("Moving");
    private readonly int _petting = Animator.StringToHash("Petting");


    public CurrentSlider currentSlider { get; set; }

    private void Start()
    {
        _mainCamera = Camera.main;

        //Set Collider
        _axoCollider = GetComponent<CapsuleCollider>();
        _axoCollider.center = new Vector3(0, 0.72f, 0);
        _axoCollider.radius = 0.38f;
        _axoCollider.height = 1.78f;

        moodBarGameobject = transform.GetChild(3).gameObject; //Don't touch!
        moodIndicatorSlider = moodBarGameobject.GetComponent<MoodContainer>().moodSlider;
        fill = moodBarGameobject.GetComponent<MoodContainer>().moodFill;
        _cameraRotation = new Vector3(_mainCamera.transform.rotation.x, transform.rotation.y,
            _mainCamera.transform.rotation.z);
        _axolittleScript = GetComponent<Axolittle>();
        navMeshAgent = _axolittleScript.agent;
        navMeshAgent.baseOffset = -0.045f; // Make axollitle walk on the floor
        var smile = moodIndicatorSlider.transform.GetChild(2).gameObject;
        _smileImage = smile.GetComponent<Image>();

        Anim = GetComponent<Animator>();

        fill.color = gradient.Evaluate(moodIndicatorSlider.normalizedValue);
        moodBarGameobject.SetActive(false);
    }

    private void Update()
    {
        if (AquariumManager.Mode != GamemodesTypes.Petting)
        {
            Anim.SetBool(_petting, false);
            moodBarGameobject.SetActive(false);
            navMeshAgent.enabled = true;
            _axolittleScript.enabled = true;
        }

        if (AquariumManager.Mode == GamemodesTypes.Petting)
        {
            navMeshAgent.ResetPath();
        
            moodBarGameobject.SetActive(true);
            Anim.SetBool(_moving, false);
            Anim.SetBool(_petting, true);
            _axolittleScript.enabled = false;
            //navMeshAgent.enabled = false;


            moodBarGameobject.transform.LookAt(_mainCamera.transform.position);
            moodBarGameobject.transform.rotation = Quaternion.identity;

            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                Anim.SetBool(_second, false);

                _firstCoroutineStarted = false;
                _secondCoroutineStarted = false;
            }

            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Happy"))
                if (!_firstCoroutineStarted)
                {
                    var animationLength = Anim.GetCurrentAnimatorStateInfo(0).length;
                    currentSlider = CurrentSlider.First;
                    StartCoroutine(RadialSlider(animationLength));
                    _firstCoroutineStarted = true;
                }

            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Turning"))
                if (!_secondCoroutineStarted)
                {
                    SetMood(moodPointLongAnimation);
                    var animationLength = 0.8f + Anim.GetCurrentAnimatorStateInfo(0).length;

                    currentSlider = CurrentSlider.Second;
                    StartCoroutine(RadialSlider(animationLength)); // FIRST
                    _secondCoroutineStarted = true;
                    axoClickHandler.StartDelay();
                }

            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Win"))
            {
                Anim.SetBool(_first, false);
            }
        }
    }

    private IEnumerator RotateToCamera()
    {
        var targetRotation = Quaternion.LookRotation(_mainCamera.transform.position - transform.position);
        while (true)
        {
            transform.localRotation =
                Quaternion.Lerp(transform.localRotation, targetRotation, 4f * Time.deltaTime);
            yield return null;
            if (transform.localRotation.y - targetRotation.y <= 0.02f &&
                transform.localRotation.y - targetRotation.y >= -0.02f)
                yield break;
        }
    }

    private IEnumerator RadialSlider(float animationLength) // Filling sliders
    {
        float startSliderValue = 0;
        var sliderMaxValue = 1;
        float timer = 0;

        while (timer < 1f && currentSlider != CurrentSlider.None)
        {
            lerpedValue = Mathf.Lerp(startSliderValue, sliderMaxValue, timer += Time.deltaTime / animationLength);


            yield return null;
        }

        lerpedValue = 0;
    }


    private void SetMood(float mood)
    {
        if (_currentMoodValue >= maxMoodValue) return;
        _currentMoodValue += mood;

        moodIndicatorSlider.value = _currentMoodValue;
        fill.color = gradient.Evaluate(moodIndicatorSlider.normalizedValue);

        if (moodIndicatorSlider.value >= 3) ChangeMoodSprite(moodImages[1]);

        if (moodIndicatorSlider.value > 6) ChangeMoodSprite(moodImages[2]);
    }

    private void ChangeMoodSprite(Sprite sprite)
    {
        _smileImage.sprite = sprite;
    }

    public CurrentSlider GetCurrentSlider()
    {
        return currentSlider;
    }

    public enum CurrentSlider
    {
        First,
        Second,
        None
    }
}