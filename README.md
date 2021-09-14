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

- Tile system
- ~~Build enemy animations and attacks~~ always needing more
- Write guide to constructing new characters (animations, movement, interactor, animation event, triggering attacks, all required scripts)
- ~~Build inventory system~~
- Add Items
- Fix Ice Dagger shooting, original Prefab orientation incorrect, TOP PRIORITY
- Flesh out equipment slots on other characters
- Build Characters
- Add Other collectible resources (gold, gems etc)
- ~~Create Item usability (if you're holding equipment, override original attack)~~
- Figure out player preferences for saving data
- Figure out loading and saving with player+equipments
- UI elements for picking skills/equipments/boosts
- Objects for player character stats
- System for unlocking new playable characters / growing characters from base eggs
- Build out level terrains, build out level cameras
    - Level 2
    - Level 3
    - Level 4
    - Level 5
- UI elements for character screen before levels
- Main Menu
- ~~Player shoot~~
- ~~IShoot Interface~~
- ~~IHealth Interface and Healthbar flexibility~~
- ~~Interactor needs to have IHealth and IMove~~

