# Game Template Documentation

## Modules Overview

### Core.BehaviourTree
Behavior Trees are used to structure the decision-making process of your game's NPCs, making complex AI behaviors more manageable and modular.

#### Node
Abstract base class for all behavior nodes. It manages child nodes, state evaluations, and node data propagation.

#### Selector
A specific type of node that executes its children nodes sequentially and returns success at the first child node that succeeds. If all children fail, it returns failure.

#### Tree
Encapsulates a complete behavior tree with a single root node. It evaluates the tree starting from the root node.

**Usage:**
```csharp
// Create a selector with its child nodes
Node root = new Selector(new List<Node>{ /* child nodes here */ });

// Create the behavior tree with the root node
Tree behaviorTree = new Tree(root);

// Evaluate the behavior tree
NodeState result = behaviorTree.Evaluate();
```

### Core.GameBootstrap
Contains foundational classes needed to bootstrap and run the game, integrating closely with Unity's lifecycle methods to manage the game's initialization and state transitions.

#### Game
Main entry point for the game loop, responsible for initializing components and managing state transitions based on Unity's lifecycle events like `Awake`, `Update`, and `FixedUpdate`.

#### ICoroutineRunner
Interface to allow non-MonoBehaviour classes to start coroutines, facilitating the execution of asynchronous tasks in a decoupled way.

#### SceneLoader
Manages the asynchronous loading of scenes with callback support, enhancing scene management by allowing other game components to respond to load completion events.

**Usage:**
```csharp
// Inside a Unity MonoBehaviour that acts as a game starter
public class GameStarter : MonoBehaviour
{
    private void Start()
    {
        // Initialize the scene loader with this MonoBehaviour as the coroutine runner
        SceneLoader loader = new SceneLoader(this);
        loader.Load("MainScene", success => {
            if(success) Debug.Log("Scene loaded successfully!");
        });
    }
}
```
### Core.StateMachine.GameLoop
Manages the states of the game using a state machine, ideal for controlling game flow from the startup phase through various gameplay phases and other states.

#### BaseStateMachine
An abstract base class for state machines, managing transitions between various game states and updating them based on game logic and physics updates.

#### GameStateMachine
A specific implementation of `BaseStateMachine` tailored for the game's states, managing the lifecycle and transitions of states like bootstrapping, level loading, and the main game loop.

#### BootstrapState
The initial state that registers all necessary services and transitions the game to the `LevelLoadState`. It sets up the foundational components for the game.

#### GameLoopState
Manages the ongoing updates during the main gameplay loop, handling logic and physics updates specific to the game's active state.

#### LevelLoadState
Handles the loading of game levels and sets up the game environment before transitioning to the main game loop.

**Usage:**
```csharp
// Initialize the state machine with services and a container for NPCs
GameStateMachine stateMachine = new GameStateMachine(AllServices.Container, transform);

// Enter the bootstrap state to start the game setup process
stateMachine.Enter<BootstrapState>();
```
### Core.Services
This module provides core functionality across the game, which can be easily accessed through the service locator, facilitating modularity and reusability of common game services.

#### AllServices
Acts as the central registry for all services in the game, using a service locator pattern to manage dependencies. It allows for registering and retrieving services dynamically throughout the game's lifecycle.

#### ResourceLoader
Handles the asynchronous and synchronous loading of resources such as textures, models, and data files, abstracting away the details of resource management within Unity.

#### GameFactory
Responsible for creating game objects dynamically, such as players or NPCs. It utilizes the `ResourceLoader` to load prefabs before instantiating them in the game world.

#### InputService
Abstracts the input handling by providing a consistent interface for querying user input, making it easy to adapt to different input devices or test input handling.

**Usage:**
```csharp
// Registering a service
AllServices.Container.RegisterSingle<IInputService>(new StandaloneInputService());

// Retrieving a service
var inputService = AllServices.Container.Single<IInputService>();

// Using the service to get input
bool isPressing;
Vector2 movement = inputService.GetMovementAxis(out isPressing);
```
### Data
This module is responsible for defining and managing the data structures used within the game, including configurations, settings, and runtime data. It ensures data integrity and provides easy access to persistent game settings.

#### GameData
Stores runtime settings that can be persisted across game sessions, such as player preferences or game state.

#### WorldConfig
Contains static configurations for the game world, which can include settings like gravity, level parameters, or other environment variables.

#### WorldConfigHolder
A `ScriptableObject` in Unity that holds a reference to `WorldConfig`, making it easy to manage and access game configuration through Unity's inspector and asset management system.

**Usage:**
```csharp
// Loading game data
IGameDataService gameDataService = AllServices.Container.Single<IGameDataService>();
GameData gameData = gameDataService.GameData;

// Accessing configuration
IResourceLoader resourceLoader = AllServices.Container.Single<IResourceLoader>();
WorldConfig config = resourceLoader.Load<WorldConfigHolder>("path/to/config").Config;
```
