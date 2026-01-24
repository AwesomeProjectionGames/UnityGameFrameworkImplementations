using AwesomeProjectionCoreUtils.Extensions;
using GameFramework;
using GameFramework.Dependencies;
using GameFramework.Events;
using UnityEngine;

namespace UnityGameFrameworkImplementations.Core.Utils
{
    /// <summary>
    /// Utility to transfer the ownership of a actor to the controller of the last actor that interacted with it.
    /// Like a "push" a new actor into possession of the controller, and then "pop" the previous actor back into possession.
    /// Usefull for objects that need to be controlled temporarily by another player.
    /// Think of a vehicle that a player can enter and exit.
    /// TransferOwnershipToController should be the main entry point to transfer possession.
    /// Some actor can raise OnActorWantsToGiveBackOwnershipEvent so the possession is given back automatically.
    /// </summary>
    public class ControllerTransferOwnershipPossession : MonoBehaviour, IActorComponent
    {
        public IActor Actor { get; set; }
        
        private IActor _lastActorThatInteracted;
        
        private void Start()
        {
            Actor.EventDispatcher.Subscribe<OnActorWantsToGiveBackOwnershipEvent>(OnActorWantsToGiveBackOwnershipEvent);
        }

        private void OnDestroy()
        {
            Actor.EventDispatcher.Unsubscribe<OnActorWantsToGiveBackOwnershipEvent>(OnActorWantsToGiveBackOwnershipEvent);
        }

        /// <summary>
        /// Transfer the ownership of this object to the controller of the interacting actor.
        /// </summary>
        /// <param name="actorInteracting">The actor that is interacting with this object (should have a controller).</param>
        public void TransferOwnershipToController(IActor actorInteracting)
        {
            if(!actorInteracting.Controller.IsAlive()) return;
            _lastActorThatInteracted = actorInteracting;
            actorInteracting.Controller?.PossessActor(Actor);
        }
        
        /// <summary>
        /// Ask the controller to repossess the last actor that interacted with this object.
        /// Actor interacting must be still alive and the actor of this gameobject must have the controller.
        /// </summary>
        public void RestoreLastInteractorControllerPossession()
        {
            if(!_lastActorThatInteracted.IsAlive()) return;
            if(!Actor.Controller.IsAlive()) return;
            Actor.Controller?.PossessActor(_lastActorThatInteracted);
        }

        private void OnActorWantsToGiveBackOwnershipEvent(OnActorWantsToGiveBackOwnershipEvent evt)
        {
            RestoreLastInteractorControllerPossession();
        }
    }
}