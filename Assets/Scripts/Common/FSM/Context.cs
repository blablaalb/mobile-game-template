namespace PER.Common.FSM
{
    using UnityEngine;

    public class Context<T> : MonoBehaviour where T : IState
    {
        protected T _currentState;

        protected virtual void EnterState(T state)
        {
            if (_currentState != null)
            {
                ExitCurrentState();
            }
            _currentState = state;
            Debug.Log($"{gameObject.name} <color=green>Entered</color> {_currentState.StateName}", gameObject);
            _currentState.Enter();
        }

        protected virtual void Update()
        {
            _currentState?.OnUpdate();
        }

        protected virtual void FixedUpdate()
        {
            _currentState?.OnFixedUpdate();
        }

        public virtual void ExitCurrentState()
        {
            if (_currentState != null)
            {
                _currentState?.Exit();
                Debug.Log($"{gameObject.name} <color=red>Exited</color> {_currentState.StateName}", gameObject);
            }
        }
    }
}