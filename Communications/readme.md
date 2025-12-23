# Communications (Game Framework Implementations)
A package that provides common communication mechanisms for Unity Game Framework-based projects, including event buses and tools to facilitate inter-system communication and interfaces workflow in Unity.

## Communication mechanism philosophy

| Mechanism                             | Use Case                                                               | Scope / Lifetime                   | Pros                                                                   | Cons                                                                            | Examples                                                          |
| ------------------------------------- | ---------------------------------------------------------------------- | ---------------------------------- | ---------------------------------------------------------------------- | ------------------------------------------------------------------------------- | -------------------------------------------------------------------- |
| **Interface**                         | Stable capability or contract (what an entity *is*)                    | Component / prefab, compile-time   | Strong typing, compile-time safety, testable                           | Requires explicit declaration per capability; can explode if many small signals | `IMovementMotor`, `IGroundState`, `IHealth`, `IWeapon`               |
| **Local Event Bus**                   | High-frequency or volatile intra-entity signals (what just *happened*) | Owned by entity, destroyed with it | Decoupled, flexible, allows multiple listeners, no interface explosion | Must enforce scope; no return values; misuse can introduce hidden dependencies  | Footsteps, camera shake, animation events, local VFX/audio triggers  |
| **Global Event Bus**                  | Cross-domain, system-wide notifications (unknown listeners)            | Singleton / global                 | Loose coupling across systems, many-to-many                            | Hidden dependencies, debugging complexity, performance concerns                 | Analytics, quest progression, achievements, global UI notifications  |
| **UnityEvent / Designer-Wired Event** | Designer-authored or disposable logic                                  | Prefab / scene                     | Easy for non-programmers, Inspector-visible, visual wiring             | Reflection-based, slower, less safe for frequent events, hidden contracts       | Cinematics, triggers, one-off level interactions, cutscene animations |

**Rule of thumb:**

1. **Capabilities → Interface**
2. **Local signals → Local Event Bus**
3. **Cross-system → Global Event Bus**
4. **Designer-owned → UnityEvent / Inspector wiring**

> **Interfaces describe identity.
> Local buses describe transitions.
> Global buses broadcast.
> UnityEvents empower designers.**