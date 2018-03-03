//
// Auto-generated by Unity FSM Code Generator:
//     https://github.com/justonia/UnityFSMCodeGenerator
//
// ** Do not modify, changes will be overwritten. **
//

using System.Collections;
using System.Collections.Generic;

namespace UnityFSMCodeGenerator.Examples
{
    // This FSM controls the volume of the phone.
    public class TelephoneVolumeFSM :  UnityFSMCodeGenerator.BaseFsm,
        UnityFSMCodeGenerator.IFsmIntrospectionSupport,
        UnityFSMCodeGenerator.IFsmDebugSupport
    {
        public readonly static string GeneratedFromPrefab = "Assets/UnityFSMCodeGenerator/UnityFSMCodeGenerator/Examples/Telephone/TelephoneVolumeFSM.prefab";
        public readonly static string GeneratedFromGUID = "b81d0d14e94579c4c85b7b09730d97dd";
        
        public enum State
        {
            WaitForEvent,
            VolumeUp,
            VolumeDown,
        }
    
        public const State START_STATE = State.WaitForEvent;
    
        public enum Event
        {
            VolumeChanged,
            VolumeDown,
            VolumeUp,
        }
    
        public interface IContext : UnityFSMCodeGenerator.IFsmContext
        {
            State State { get; set; }
            UnityFSMCodeGenerator.Examples.IAudioControl AudioControl { get; }
        }
    
        #region Public Methods
    
        public IContext Context { get { return context; }}
        
        // TelephoneVolumeFSM is completely stateless when events are not firing. Bind() sets
        // the current context but does nothing else until you call SendEvent().
        // Instances of this class may be re-used and shared by calling Bind() in-between
        // invocations of SendEvent().
        public void Bind(IContext context)
        {
            if (isFiring) {
                throw new System.InvalidOperationException("Cannot call TelephoneVolumeFSM.Bind(IContext) while events are in-progress");
            }
    
            this.context = context;
        }
    
        // Send an event, possibly triggering a transition, an internal action, or an 
        // exception if the event is not handled in the current state. If an event is in
        // process of firing, the event is queued and then sent once firing is done.
        public void SendEvent(Event _event)
        {
            if (eventPool.Count == 0) {
                eventPool.Enqueue(new QueuedEvent());
            }
            var queuedEvent = eventPool.Dequeue();
            queuedEvent._event = _event;
            InternalSendEvent(queuedEvent);
        }
        
        public static IContext NewDefaultContext(
            UnityFSMCodeGenerator.Examples.IAudioControl audioControl,
            State startState = START_STATE)
        {
            return new DefaultContext{
                State = startState,
                AudioControl = audioControl, 
            };
        }
    
        
        // Convenience so you can use the State enum in a Dictionary and not worry about
        // garbage creation via boxing: new Dictionary<State, Foo>(new StateComparer());
        public struct StateComparer : IEqualityComparer<State>
        {
            public bool Equals(State x, State y) { return x == y; }
            public int GetHashCode(State obj) { return obj.GetHashCode(); }
        }
    
        #endregion
    
        #region Private Variables
           
        public override UnityFSMCodeGenerator.IFsmContext BaseContext { get { return context; }}
        
        private class QueuedEvent
        {
            public Event _event;
        }
    
        readonly Queue<QueuedEvent> eventQueue = new Queue<QueuedEvent>();
        readonly Queue<QueuedEvent> eventPool = new Queue<QueuedEvent>();
        private bool isFiring;
        private IContext context;
    
        private class DefaultContext : IContext
        {
            public State State { get; set; }
            public UnityFSMCodeGenerator.Examples.IAudioControl AudioControl { get; set; }
            
        }
    
        #endregion
    
        #region Private Methods
        
        private void InternalSendEvent(QueuedEvent _event)
        {
            if (isFiring) {
                eventQueue.Enqueue(_event);
                return;
            }
    
            try {
                isFiring = true;
    
                SingleInternalSendEvent(_event);
    
                while (eventQueue.Count > 0) {
                    var queuedEvent = eventQueue.Dequeue();
                    SingleInternalSendEvent(queuedEvent);
                    eventPool.Enqueue(queuedEvent);
                }
            }
            finally {
                isFiring = false;
                eventQueue.Clear();
            }
        }
    
        
        private void SingleInternalSendEvent(QueuedEvent _eventData)
        {
            Event _event = _eventData._event;
            State from = context.State;
        
            switch (context.State) {
            case State.WaitForEvent:
                switch (_event) {        
                case Event.VolumeUp:
                    if (TransitionTo(State.VolumeUp, from)) {
                        SwitchState(from, State.VolumeUp);
                    }
                    break;        
                case Event.VolumeDown:
                    if (TransitionTo(State.VolumeDown, from)) {
                        SwitchState(from, State.VolumeDown);
                    }
                    break;        
                default:
                    if (!HandleInternalActions(from, _event)) {
                        throw new System.Exception(string.Format("Unhandled event '{0}' in state '{1}'", _event.ToString(), context.State.ToString()));
                    }
                    break;
                }
                break;
        
            case State.VolumeUp:
                switch (_event) {        
                case Event.VolumeChanged:
                    if (TransitionTo(State.WaitForEvent, from)) {
                        SwitchState(from, State.WaitForEvent);
                    }
                    break;        
                default:
                    if (!HandleInternalActions(from, _event)) {
                        throw new System.Exception(string.Format("Unhandled event '{0}' in state '{1}'", _event.ToString(), context.State.ToString()));
                    }
                    break;
                }
                break;
        
            case State.VolumeDown:
                switch (_event) {        
                case Event.VolumeChanged:
                    if (TransitionTo(State.WaitForEvent, from)) {
                        SwitchState(from, State.WaitForEvent);
                    }
                    break;        
                default:
                    if (!HandleInternalActions(from, _event)) {
                        throw new System.Exception(string.Format("Unhandled event '{0}' in state '{1}'", _event.ToString(), context.State.ToString()));
                    }
                    break;
                }
                break;
        
            }
        }
    
        
        
        private bool HandleInternalActions(State state, Event _event)
        {
            // no states have internal actions, intentionally empty
            return false;
        }
    
    
        private void SwitchState(State from, State to)
        {
            context.State = to;
            DispatchOnExit(from);
            DispatchOnEnter(to);
        }
        
        private bool TransitionTo(State state, State from)
        {
            // TODO: Guard conditions might hook in here
            return true;
        }
    
        
        private void DispatchOnEnter(State state)
        {    
            if (onEnterBreakpoints.Contains(state)) {
                UnityEngine.Debug.LogFormat("{0}.OnEnter breakpoint triggered for state: {1}", GetType().Name, state.ToString());
                if (onBreakpointHit != null) {
                    onBreakpointHit(this, state);
                }
                // IMPORTANT: This is not the same as setting a breakpoint in Visual Studio. This method
                // will continue executing and the editor will pause at some point later in the frame.
                UnityEngine.Debug.Break();
            }
        
            switch (state) {
            case State.WaitForEvent:
                break;
            case State.VolumeUp:
                context.AudioControl.VolumeUp();
                break;
            case State.VolumeDown:
                context.AudioControl.VolumeDown();
                break;
            }
        }
    
        
        private void DispatchOnExit(State state)
        {
            switch (state) {
            case State.WaitForEvent:
                break;
            case State.VolumeUp:
                break;
            case State.VolumeDown:
                break;
            }
        }
    
        #endregion
        
        #region IFsmIntrospectionSupport
        
        string IFsmIntrospectionSupport.GeneratedFromPrefabGUID { get { return GeneratedFromGUID; }}
        
        private Dictionary<State, string> introspectionStateLookup = new Dictionary<State, string>(new StateComparer()){
            { State.WaitForEvent, "Wait For Event" },
            { State.VolumeUp, "Volume Up" },
            { State.VolumeDown, "Volume Down" },
        };
        private List<string> introspectionStringStates = new List<string>(){
            "Wait For Event",
            "Volume Up",
            "Volume Down",
        };
        private Dictionary<string, State> stateNameToStateLookup = new Dictionary<string, State>(){
            { "Wait For Event", State.WaitForEvent },
            { "Volume Up", State.VolumeUp },
            { "Volume Down", State.VolumeDown },
        };
        
        string UnityFSMCodeGenerator.IFsmIntrospectionSupport.State { get { return context != null ? introspectionStateLookup[context.State] : null; }}
        
        string UnityFSMCodeGenerator.IFsmIntrospectionSupport.StateFromEnumState(object state) { return introspectionStateLookup[(State)state]; }
        
        List<string> UnityFSMCodeGenerator.IFsmIntrospectionSupport.AllStates { get { return introspectionStringStates; }}
        
        object UnityFSMCodeGenerator.IFsmIntrospectionSupport.EnumStateFromString(string stateName) { return stateNameToStateLookup[stateName]; }
        
        #endregion
    
        
        #region IFsmDebugSupport
        
        private UnityFSMCodeGenerator.BreakpointAction onBreakpointSet = null;
        private UnityFSMCodeGenerator.BreakpointAction onBreakpointHit = null;
        private UnityFSMCodeGenerator.BreakpointsResetAction onBreakpointsReset = null;
        private HashSet<State> onEnterBreakpoints = new HashSet<State>(new StateComparer());
        
        event UnityFSMCodeGenerator.BreakpointAction UnityFSMCodeGenerator.IFsmDebugSupport.OnBreakpointSet { add { onBreakpointSet += value; } remove { onBreakpointSet -= value; }}
        event UnityFSMCodeGenerator.BreakpointAction UnityFSMCodeGenerator.IFsmDebugSupport.OnBreakpointHit { add { onBreakpointHit += value; } remove { onBreakpointHit -= value; }}
        event UnityFSMCodeGenerator.BreakpointsResetAction UnityFSMCodeGenerator.IFsmDebugSupport.OnBreakpointsReset { add { onBreakpointsReset += value; } remove { onBreakpointsReset -= value; }}
        
        int UnityFSMCodeGenerator.IFsmDebugSupport.OnEnterBreakpointCount { get { return onEnterBreakpoints.Count; }}
        
        void UnityFSMCodeGenerator.IFsmDebugSupport.SetOnEnterBreakpoint(object _state)
        {
            var state = (State)_state;
            onEnterBreakpoints.Add(state);
            if (onBreakpointSet != null) {
                onBreakpointSet(this, _state);
            }
        }
        
        void UnityFSMCodeGenerator.IFsmDebugSupport.ResetBreakpoints()
        {
            onEnterBreakpoints.Clear();
            if (onBreakpointsReset != null) {
                onBreakpointsReset(this);
            }
        }
        
        #endregion
    
    }
    
}