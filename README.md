# BulletRPG

## Overview of Gameplay Loop

The game is played in short increments of combat. 30 seconds to 1 minute.

Combat consists of ranged and melee attacks on a 2d plane between the player and enemy npcs.

There are levels that take place on different battlefields/environments. Within each level are 5 progressively harder stages. The stages are additive and build on the previous stage.

After winning a stage, the player will pick 1 boost from a possible 3. The boosts are generated from a psuedo random pool of possible upgrades. These boosts are permanent character improvements. Example boosts are:

- +1% health
- +2% ranged damage
- +2% damage reduction
- +5% speed

After winning a level, the player will pick 1 equipment from a possible 3. These equipments are permanent and present mechanical alterations to gameplay. These can be changed between levels.

- Teleportation
- Mirrored damage effects
- Added damage to attacks the longer you dont attack
- Reduced damage when nearby an enemy

If a player dies on a stage they will restart on that stage. They can continue to attempt the stage until they run out of resources. If a player wishes, they can return to stage 1 of any level at any time during that level. They lose any boosts and start the level as they did originally. This can be advantagous if the boosts were uneffective or their character lacks certain traits to succeed with a certain set up.

The player continues to beat stages and gains progressively more equipments and skills. Later levels will restrict the available set of equipments as well as broaden the amount of enemies and their difficulty.

Before entering a level the player may adjust their equipments and skills. After entering the level they will be stuck with those setups unless they pay to restart the level.



## Todo List
- Make Utilities function take in a game object and search for slots, returns an array of gearslots available.
- Make Utilities function that returns the location in a model, input gearslot.type output, the gameobject that is the slot for it
- Remake UML for inventory system
- Make more items/gear classes to flesh out the structure
- Make UML for items and their structure
- Make Health/Movement interfaces to be used by the damage class to pass damage effects to
- Make UML for combat/health/effects/movement

- Create utility that can get nearest point on navmesh for any class to use
- Tile system
- Add UI element for shoot timer cooldown
- UI elements for picking skills/equipments/boosts
- System for unlocking new playable characters / growing characters from base eggs
- Build out level terrains, build out level cameras
- UI elements for character screen before levels
- Main Menu
- Flesh out equipment slots on other characters
- Build Characters
- Add Other collectible resources (gold, gems etc)
- Figure out player preferences for saving data

- ~~allow users to drop items to a point on the map~~
- ~~Figure out loading and saving with player+equipments~~
- ~~Reimplement ItemObject and replace all Inventory related objects with Items instead of gear~~
- ~~Dropping stack creates only 1 object on ground, access lootable object and set the amount~~
- ~~Fix Inventory from rotating when equipping, (only delete the spot that was equipped, leave the rest alone)~~
- ~~Serialization on GameEvent on inventory object fails, if you use inventory, then close and restart, the UnityID for the event changes. Just use different delegate system instead of game events~~
- ~~Build enemy animations and attacks~~ always needing more
- ~~Add Items~~ always need more
- ~~Create structure for each item slot and what they add to the player~~
- ~~Create integration for those item structures~~
- ~~Objects for player character stats~~
- ~~UI elements for equipped objects~~
- ~~Decide on inventory size and such to clean up inventory functionality~~
- ~~Fix bug where if you switch weapons before animation function call occurs you get the projectile from the previous weapon (BIG BUG TODO)~~   
- ~~Fix Ice Dagger shooting, original Prefab orientation incorrect~~
- ~~UI elements for inventory~~
- ~~Player shoot~~
- ~~IShoot Interface~~
- ~~IHealth Interface and Healthbar flexibility~~
- ~~Interactor needs to have IHealth and IMove~~
- ~~Build inventory system~~
- ~~Create Item usability (if you're holding equipment, override original attack)~~
