# Cannon Challenge

Implement Cannon game in 7 days

## Implementation Plan

- Get References
- Find Basic Assets
- Implement Cannon Movement
- Design and Implement Projectiles
- Implement Trajectory Simulation
- Design and Implement Targets
- Design Game Modes
- Scalable stats/Upgrades
- Score
- Currency
- UI
- Sound
- VFX

## Technical Goals

The main Objective besides having a polished game as much as possible, it is to have a pass in most Unity Engine elements and programming aspects

### Unity Engine Elements

- Scriptable Objects
	- Used for Data Definitions, Events, Static references between scenes and dynamic instantiated objects	
- Use of Layers
	- Minimum Collision Layers matrix configured, used to process specific collisions like cannonball and barrels	
- Use of Tags
	- Allow "Semantic" separation of objects when triggering/colliding with a common layer between different objects - (eg. cannonball)
- Inspector Documentation
	- Used Attribute annotations like Tooltip and Header to document what the exposed attributes are for
- Object Pooling Enemies and VFX
	- For performance sake keep most instantiations during game play under a game object pooling system, which lowers the memory allocation
- Assemble Definitions
	- Group script packages into compiled assemblies, lowering compile time and helping to picture code coupling and dependencies	
- Shader Graph
	- Implement some of the visual effects with Shader Graph which uses more GPU instead of CPU resources to improve performance (eg. Explosion, Smoke)	
- Particle System
	- Simple and quick VFX elements (eg. cannonball impact, sea waves, moon floating particles) 
- Prefab Variants
	- Keep a reference to a basic prefab and extend to variants of it. (eg. Object pooling reference for Normal and Explosive Barrels)
- Animation
	- Explore Animator and Animations for UI and Characters. UI I did most interpolations of properties like scale and position. Characters I imported models from Mixamo and configured the animator tree	
- Physics
	- Used Colliders for physical collision and as triggers. Box Colliders for barrels, and as triggers for KillZ zones 
	- Avoided mesh colliders for performance sake
	- Rigidbodies just when necessary
	- Global settings like Gravity (eg. Moon)
	- Physical Materials to adjust drag and bounciness
	- Translation movement for the boats and Force for explosive barrel
	- Raycasting to find objects - Explosive barrel - Used NonAlloc method for performance
- Timeline
	- Used timeline sequence to cinematic the end of the level
	- used signal to trigger to notify the end of the cinematic	
- CineMachine
	- Used Virtual Camera to do a transition between camera angles in the Level complete Cinematic	
- Additive Scenes
	- Massive unity resource to separate layers of a scene like UI, Environment, Level logic in different assets and load as needed. I used the Score Summary as a asynchronous Additive scene 
- New UI Toolkit
	- Powerful UI tool to create not only Dev tools but in-game UIs now. I implemented the Score display. Whenever a player is awarded the Score display is updated through events.
- Normal UI
	- Menus and Player Notification system	I used the regular UI with TextMeshPro 	
- Material/Shader
	- Wrote a custom shader for Shader Graph Explosion.
	- Had to re-assign a lot of Assets imported from Unity Store for a proper URP lit/unlit Materials since they used shaders for regular pipeline.
	- created and adjusted some materials for the moon rocks, terrain and barrel explosive version 
- Light Baking
	- changed to mixed light mode in order to bake light/occlusion/reflections for performance instead of using real-time
- Post Processor
	- used some basics Post processor effects like bloom
- Test Runner
	- did as small test class just to showcase a editor test.	
- Terrain Tool
	- Imported environmental assets with terrains from asset store. I did some adjustments in the settings and data assets to match the URP needs
- Input System
	- I implemented a layer to allow the legacy and new Input system
- Multithreading
	- I implemented a simple scene with DOTS/ECS whith physics. (see more below)	

### Programming Aspects

- Code Scalability
	- Implemented cannon attributes in a way that allows a upgrade system and a Infinite mode
	- Game core elements like Cannon, Targets, Wave System have data definitions abstracted to scriptable objects to allow scale the game
- Performance
	- Fine adjustments for performance like mentioned in the previous sections, for shadow casting, memory allocation, object pooling, component caching
- Event Driven
	- Reliable event system serialized in Scriptable Objects which allow less code in Monobehaviour Update method, processing code just when needed
	- Separate responsibilities applying SOLID principles and decoupling code.
	- Feedbacks are trigger by events and handled in another layer of implementation
- Data Serialization
	- Show cased data serialization with Json files to save the level best records
- Agnostic to: Input System/persistent/serialization layer 
	- abstracted inputs and serialization aspects of the system, allowing changing it with minimal impact
- Object Oriented Programming
	- Code abstraction, Inheritance, polymorphism and encapsulation of behaviors as OOP dictate (eg. Normal/Explosive Barrel, overloaded and virtual methods, )
	- Interface usage to abstract implementation (eg. FileSerialization, Input Handler)
- Design Patterns
	- Solving common problems with common solutions whenever possible. Some implementations like serialization, input system, pooling, object factory, observer/consumer approach

### Extra

I wanted to show you a little bit about **DOTS ECS**, which, according to Unity's CEO, is the future of the engine. I implemented a simple scene that generates a huge amount of objects per second. I already had previous experience with ECS in my previous project. I believe that really multithreading is something that was missing from the engine.
