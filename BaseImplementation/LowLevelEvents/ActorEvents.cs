using GameFramework;

namespace UnityGameFrameworkImplementations.Core.LowLevelEvents
{
    /// <summary>
    /// Events on the local actor bus should inherit from this class.
    /// Should be used for high-frequency or volatile intra-entity signals.
    /// </summary>
    public abstract class ActorEvent 
    {
        public IActor Actor { get; }
        protected ActorEvent(IActor actor) => Actor = actor;
    }

    /// <summary>
    /// Fired when an actor requests an ownership transfer back to the server.
    /// </summary>
    public sealed class OnActorWantsToGiveBackOwnershipEvent : ActorEvent
    {
        public OnActorWantsToGiveBackOwnershipEvent(IActor actor) : base(actor) { }
    }
}