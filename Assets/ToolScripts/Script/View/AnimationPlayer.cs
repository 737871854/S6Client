using System;
using UnityEngine;
using System.Collections.Generic;


public enum AnimationID
{
    idle = 0,
    ready = 1,
    run = 2,
    is_null,
}

public class AnimationPlayer : MonoBehaviour
{
	public delegate void AnimationCallBack(object arg);
    public delegate bool AnimationEndEventCallBack(object arg);

    protected string kAnimatorStateParam = "state";

    private void Awake()
	{
        Init();
    }

    public void Init()
    {
        _animator = GetComponent<Animator>();
        if (_animator != null) {
            if (_animator.runtimeAnimatorController == null) {
                Debugger.LogWarning("{0} animator not exist", gameObject.name);
                _animator = null;
            }
        }
        int idCount = (int)AnimationID.is_null;
        _id2Name = new string[idCount];
        for (int id = 0; id < idCount; ++id)
        {
            _id2Name[id] = "Base Layer." + ((AnimationID)id).ToString();
        }
    }

    public void SetAttackSpeed(float speed)
    {
        if (_animator != null) {
            _animator.speed = speed;
        }
    }

    public void ResetAttackSpeed(float speed = 1.0f)
    {
        if (_animator != null) {
            _animator.speed = speed;
        }
    }

    public float GetAttackSpeed()
    {
        if (_animator != null) {
            return _animator.speed;
        }
        return 0f;
    }

	public void Play(AnimationID id, AnimationCallBack callback = null, object arg = null, AnimationEndEventCallBack endEventCallBack = null, object endEventArg = null)
    {
        //if (!gameObject.activeInHierarchy) return;
		_callback = callback;
        _endEventCallBack = endEventCallBack;
        _endEventArg = endEventArg;
		//_stateHash = Animator.StringToHash(_id2Name[(int)id]);
		_arg = arg;
        if (_animator != null) {
            //_animator.SetInteger(kAnimatorStateParam, (int)id);
            _animator.Play(id.ToString());
        }
	}

    public void PlayImmediate(AnimationID id, AnimationCallBack callback, object arg, AnimationEndEventCallBack endEventCallBack, object endEventArg)
    {
  //      //if (!gameObject.activeInHierarchy) return;
		//_callback = callback;
		//_arg = arg;
  //      _endEventCallBack = endEventCallBack;
  //      _endEventArg = endEventArg;
		////_stateHash = Animator.StringToHash(_id2Name[(int)id]);
  //      if (_animator != null) {
		//    _animator.SetInteger(kAnimatorStateParam, (int)id);
  //          _animator.CrossFade(_id2Name[(int)id], 0);
  //      }
    }

	public bool IsPlaying(AnimationID Id, bool inTransition = true)
    {
        if (_animator != null) {
            if (_animator.IsInTransition(0))
                return inTransition;
            AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName(_id2Name[(int)Id]);
        } else {
            return false;
        }
	}

	void OnActionEvent()
	{
		if( _callback != null ){
			AnimationCallBack cb = _callback;
			object arg = _arg;
			_callback = null;
			_arg = null;
			cb(arg);
		}
	}

	void OnActionEndEvent()
    {
        if (_animator != null) {
            if (_endEventCallBack != null) {
                _endEventCallBack(_endEventArg);
                // None
            }
        }
	}

	private Animator _animator = null;
	private AnimationCallBack _callback = null;
	private object _arg = null;
    private AnimationEndEventCallBack _endEventCallBack = null;
    private object _endEventArg = null;
    private string[] _id2Name = null;
}

