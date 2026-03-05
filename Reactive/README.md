# Reactive par of Game Framework (Unity implementation)

**Package Name:** `com.awesomeprojection.gameframework.reactive`
**Version:** 1.0.0
**Unity Version:** 2022.3 LTS

Contains a collection of reactive observer / observable components used to bridge data changes with visual transformations and camera behaviors. 

## Overview

This package provides a set of generic base classes to implement the Observer pattern within Unity's `MonoBehaviour` ecosystem. It is designed to decouple game logic and data sources from visual systems like UI, VFX Graphs, Shaders, and Camera effects. 

It is built upon the core `IValueObservable<T>` and `IValueObserver<T>` interfaces from the `com.awesomeprojection.gameframework`.

## Core Components

### `ValueObservableMonoBehaviour<T>`
An abstract base class that exposes a simple value natively through a `MonoBehaviour`. 
- Notifies listeners whenever the value changes via `SetValue(T value)`.
- Defines an optional `Name` channel to allow auto-finding and identification.

### `ValueObserverMonoBehaviour<T>`
An abstract base class for objects reacting to value changes from an `IValueObservable<T>`.
- Can automatically find a source in itself or its parents during `Awake` using the structural hierarchy.
- Provides simple Inspector properties (`tryAutoFindOnAwake`, `exposedSimpleValueChannelName`) to reduce boilerplate wiring and references.
- Implement `OnObservedValueChanged(T newValue)` to apply custom visual or gameplay logic whenever the observed value updates.

### `ValueConverter<T>`
Acts as a bridge component that observes a value, applies some transformation or filtering logic, and exposes the modified result as a new `IValueObservable<T>`.

### Utilities
- **`IValueObservableExtension`**: Extension methods for seamlessly caching and notifying observers. Handles immediate notification of new subscribers with the current value (`SubscribeAndNotify`).
