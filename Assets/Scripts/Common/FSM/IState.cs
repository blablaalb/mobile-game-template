/*
    This is the interface that all states must implement.
    The class that implements this interface must also be marked with System.Serializable.
*/

namespace PER.Common.FSM
{
    public interface IState
    {
        string StateName {get;}
        void Enter();
        void OnUpdate();
        void OnFixedUpdate();
        void Exit();
    }
}