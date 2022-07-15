using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class AxoClickHandler : MonoBehaviour, IPointerDownHandler,IDragHandler
{
    [SerializeField] private PettingSliderController pettingSliderController;

    [SerializeField] private float animationDelay;
    [SerializeField]private static float _animationCurrentDelay;
    private AxoModelGenerator _axoModelGenerator;
    private PettingController _pettingController;
    private Animator _anim;
    private Camera _mainCamera;

    private float _shortClickTime;
    private float _clicksTimer;
    
    private bool _isClicked;
    private static bool IsAbleToStart;
    private LayerMask _axoLayer;
   
    private RaycastHit _lastHit;
   


    private void Start()
    {
        IsAbleToStart = true;
       
        _mainCamera = Camera.main;
        _axoModelGenerator = GetComponent<AxoModelGenerator>();
        _shortClickTime = _axoModelGenerator.GetClickTimer();
        _axoLayer = _axoModelGenerator.HitMask;
    }
  
    public void OnPointerDown(PointerEventData eventData)
    {
        EventClick();
    }

    private void EventClick()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 900))
        {
            if (hit.collider != _lastHit.collider)

            {
                if (_anim != null)
                    ResetClick();
                if (hit.collider.TryGetComponent(out PettingController newPettingController))
                {
                    _pettingController = newPettingController;
                    pettingSliderController.SetNewSlider(_pettingController);
                    _anim = _pettingController.Anim;
                }
                else return;
            }

            _lastHit = hit;
        }
    }

    private void Update()
    {
        if (_pettingController == null) return;
        if(!IsAbleToStart)return;
        if (Input.GetKey(KeyCode.Mouse0))
            _isClicked = AxoHit();
        else _isClicked = false;
           
        if (_clicksTimer >= _shortClickTime)// SIMPLE click
        {
            ResetClick();
                
            
        }
        if (_isClicked)// SIMPLE click
        {
            StartFirstAnimation();
        }
        _clicksTimer += Time.deltaTime;
    }
    private bool AxoHit()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 900,_axoLayer ))
        {
            return true;
        }
     
        return false;
    }
    public void StartFirstAnimation()
    {
        if (_anim == null) return;
        _anim.SetBool("First", true);
        _clicksTimer = 0;
    }
    private void ResetClick()
    {
        if (_anim == null) return;
        _clicksTimer = 0f;
        _anim.SetBool("First", false);
        if(_pettingController.GetCurrentSlider()==PettingController.CurrentSlider.First)
            _pettingController.currentSlider = PettingController.CurrentSlider.None;
        _pettingController. lerpedValue = 0f;
    }

    public void StartDelay()
    {
        if (IsAbleToStart)
            StartCoroutine(InputDelayTimer());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="winClipTimeOffset">Length of animation Win</param>
    /// <param name="turnClipTimeOffset">Length of animation Turning</param>
    /// <returns></returns>
    private  IEnumerator InputDelayTimer()
    {
        IsAbleToStart = false;
        _animationCurrentDelay = 0;
        

        while(_animationCurrentDelay < 1.454167+2.222222+animationDelay)
        {
            Debug.Log(_animationCurrentDelay);
            _animationCurrentDelay += Time.deltaTime;
            yield return null;
        }
        IsAbleToStart = true;
        

    }
    public void OnDrag(PointerEventData eventData)
    {
        EventClick();
    }
}