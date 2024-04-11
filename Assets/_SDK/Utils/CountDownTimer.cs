using System;
using UnityEngine;

namespace _SDK.Utils
{
    public class CountDownTimer
    {
        private Action _doneAction;
        private float _time;

        public void Start(Action doneAction, float time)
        {
            _doneAction = doneAction;
            _time = time;
        }

        public void Execute()
        {
            if (_time > 0)
            {
                _time -= Time.deltaTime;
                if (_time <= 0)
                {
                    Exit();
                }
            }
        }

        private void Exit()
        {
            _doneAction?.Invoke();
            
            Cancel();
        }

        public void Cancel()
        {
            _doneAction = null;
            _time = -1;
        }
    }
}