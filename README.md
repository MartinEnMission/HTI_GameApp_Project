# HTI_GameApp_Project
Little game created as an introduction course to Unity at Télécom SudParis. The goal si to create a sort of endless runner game using Blender3D X Unity workflow to learn basic notions of Unity

Features of the game

The game will be in First Person View
By entering on a floor you'll will not have other choice than going forward, you will have to jump, sneek, speed up, slow, or straff your way out of the obstacles that come toward you
Floor difficulty changes between different floors, the standard speed remains the same
The game would be upgradable to multiplayer mode with eventually a ranking system with the players' time for completing a floor.

Floor organisation : 

The hub: 

    They all have the same starting hub
    In the starting hub every player can navigate as they want 
    In the hub the players are in "spectate mode"
    The hub has an empty doorframe that leads to the actual level
    When a player passes through the doorframe he goes into "play mode"
    The hub includes a respawn point, when failing to complete the floor, each player respawn at the hub

The "play mode" :

    The player has a constant speed in the front direction
    The player is forced to go forward
    The obstacles do not move, only the player do so
    The player can avoid the obstacles by modifying its speed and trajectory by crouching, straffing, jumping, speeding up or slowing down

The level part : 

    Composed of several sections corresponding to a difficulty level
    Each section has differents types of obstacles
    Currently the obstacles are horizontal or vertical bars the player has to avoid. There are also doors the player has to go through as well as fields filled with rotating bars.

Expected difficulties : 

    Implement a script to control the player in FPV
    Implementing the interaction where the player collides and dies in order to be respawned in the hub
    Implementing and tuning the jumps and crouch height, the navigation speed to calibrate the difficulty
    Learn the hard surface modelling techniques in Blender to create the 3D assets 
