# Cinders
A VR Game built for the Meta Quest 2 in Unity based on a previous project that was originally built for Vicon.

# DevLog

#### 8/15/23
Fixed GitHub version to exclude unneeded/ overly large files (Library, Assets/Oculus, etc.).

#### 8/13/23
Created Spawner script and prefab, reworked Enemy movement:
- Spawner has adaptable ways to spawn enemies inside a contained space including spawning by name or random enemies.
- Enemies will now ignore objects on the "Enemy Igore" layer and will move smoothly towards the campfire.

#### 8/12/23
Created Enemy script and prefab:
- Enemy script will serve as parent class of all enemies from which specialized enemies will inherit. Enemy moves towards campfire and can be destroyed by Firebolts.

#### 8/8/23
Created the project and the Firebolt and FlameHand scripts:
- FlameHand script handles the creation of the Firebolt on trigger held and the throwing of the Firebolt on release based on controller velocity
- Firebolt script and prefab that moves to the players hand on creation and launches when told to
