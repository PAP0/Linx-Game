# TidyTeam
You play as a clean-up crew from an illegal organization to clean up a murder. But The cops caught wind of the murder and are making their way over right now. It's your and another player's job to clean up the place as much as possible and escape before the timer runs out. Every Player is different but you have to work together and communicate to make sure you don't disappoint the boss or get caught by the police.

# Goal
The end product of the game needs to possess the following elements:
* Asymmetric: In the same game environment, players have different roles or gameplay types.
* Co-op: Cooperative, players must work together to achieve a common goal.
* Social elements: Players need to communicate with each other to achieve their common goal.

# Gameplay
TidyTeam is played locally, with two players. Both players have different abilities in the game. One of the players is a Vacuum, whose goal is to clean up all the garbage that is scattered around. The Vacuum has the ability to suck up garbage and move heavy stuff. The second player plays a Mop, whose goal is to clean up all the blood and murder weapons left behind. The Mop has the ability to clean up blood and it is much smaller than the other player to get in all those nooks and crannies. The players need to communicate with each other, place back objects and find hidden garbage before the timer runs out.

*If you want more information about the game I recommend going to our [Wiki](https://github.com/Bjornraaf/Linx-Game/wiki/Functional-Design) or [Functional Design](https://github.com/Bjornraaf/Linx-Game/wiki/Functional-Design).*

# Engine
We decided to make our game in Unity, since our team is experienced in it

The version we use in unity is: 2021.3.18f1 LTS

# Produced Game Elements

Bjorn Ravensbergen:
  * [PlayerController](https://github.com/Bjornraaf/Linx-Game/blob/develop/Assets/Code/Scripts/Player/Controller/PlayerController.cs)
  * [CleanableSpot](https://github.com/Bjornraaf/Linx-Game/blob/13ecb2bdaa95c14a757eb0b8bfdb7a0f5fe7029e/Assets/Code/Scripts/Cleaning/Cleanable.cs)
  * [PushCheck](https://github.com/Bjornraaf/Linx-Game/blob/develop/Assets/Code/Scripts/Player/Controller/PushCheck.cs)
  * [Stamina](https://github.com/Bjornraaf/Linx-Game/blob/develop/Assets/Code/Scripts/Player/Stamina/Stamina.cs)
  * [FilthSpawner](https://github.com/Bjornraaf/Linx-Game/blob/develop/Assets/Code/Scripts/Cleaning/FilthSpawner.cs)
  
Patryk Podworny:
  * [Player Input/Controller Scheme](https://docs.unity3d.com/Packages/com.unity.inputsystem@0.2/manual/index.html)
  * [Co-Op](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Components.html)
  * [Player Join Manager](https://github.com/Bjornraaf/Linx-Game/blob/develop/Assets/Code/Scripts/Co-op/PlayerJoinManager.cs)
  * [Prop Placeback](https://github.com/Bjornraaf/Linx-Game/tree/develop/Assets/Code/Scripts/Cleaning/Props)
  * [Player Area Detector](https://github.com/Bjornraaf/Linx-Game/blob/develop/Assets/Code/Scripts/Co-op/PlayerAreaDetector.cs)
  * [Cleanup mechanic](https://github.com/Bjornraaf/Linx-Game/blob/develop/Assets/Code/Scripts/Cleaning/FilthStain/FilthStain.cs)

Ties Postma:
  * [Camera script](https://github.com/Bjornraaf/Linx-Game/blame/develop/Assets/Code/Scripts/Camera/CameraController.cs)
  * [Cleanup mechanic](https://github.com/Bjornraaf/Linx-Game/blob/develop/Assets/Code/Scripts/Cleaning/FilthStain/FilthStain.cs)
  * [Timer + some other UI elements](https://github.com/Bjornraaf/Linx-Game/blob/develop/Assets/Code/Scripts/UI/Timer.cs)

## PlayerController
![PlayerControllerCharacter](https://github.com/Bjornraaf/Linx-Game/blob/develop/Images/PlayerControllerCharacter.JPG)
~~~mermaid
flowchart TD;
A(Start) --> B(Update)
B --> D[Stamina <= 1?]
D -- No --> E(UseEnergy)
D -- Yes --> F(Revive)
F --> G[Colliders present?]
G -- Yes --> H(Revive)
G -- No --> I(AutoRevive)
H --> J(WaitForRevive)
I --> J
J --> K(UseEnergy)
K --> B
B --> L[Movement >= Threshold?]
L -- Yes --> M(Set IsRunning to true)
M --> N(MovePlayer)
L -- No --> O(Set IsRunning to false)
~~~
This script encompasses several essential aspects of character control and gameplay mechanics. Through responsive movement, stamina management, animation triggering, and revival handling, it actively contributes to creating an engaging and dynamic player experience within the game.

## CleanableSpot
~~~mermaid
flowchart TD;
B --> C(Update)
C --> D(GetDirtAmount)
D --> E[Is Dirt Amount <= 0?]
E -- No --> F(Set Active to false)
E -- Yes --> G(Find Player)
G --> H(Raycast)
H --> I[Hit Ground?]
I -- Yes --> J(Get Texture Coordinate)
I -- No --> H
J --> K(Calculate Paint Pixel Position)
K --> L[Paint Pixel Distance < Max Distance?]
L -- Yes --> C
L -- No --> M(Update Last Paint Position)
M --> N(Paint in Dirt Mask)
N --> C
~~~
The Cleanable script manages the cleaning functionality of an object in the game. It enables players to interact with the object and progressively clean it using a dirt brush texture. The script initializes textures and materials, calculates the total dirt amount, and updates the object based on the cleaning progress. It tracks the player's position, detects contact points, and applies cleaning based on raycasting and texture mapping. The script ensures realistic cleaning by preventing close painting. It iterates over brush pixels, removes dirt, and updates the DirtMaskTexture accordingly. The cleaned texture is applied, and the remaining dirt is calculated as a percentage.

## PushCheck
~~~mermaid
flowchart TD;
A(Start) --> B(Update)
B --> C(RangeCheck)
C --> D[Colliders present?]
D -- Yes --> E(Enable Capsule Collider)
D -- No --> F(Disable Capsule Collider)
E --> C
F --> C
~~~
By dynamically enabling or disabling colliders based on proximity, this script provides a mechanism for controlling the interaction between the game object and nearby objects. It allows for precise control over object interactions within a specified range, enhancing gameplay mechanics and enabling various gameplay scenarios such as pushing & blocking.

## Stamina
~~~mermaid
flowchart TD;
    B[UseEnergy]
    C[IsDraining]
    D[CurrentStamina != 0]
    E[CurrentStamina <= MaxStamina]
    F[Decrease Stamina]
    G[Increase Stamina]
    
    B --> C
    C --> D
    C --> E
    D -- Yes --> F
    D -- No --> B
    E -- Yes --> G 
    E -- No --> B
~~~
Based on player actions, such as using skills, this script dynamically adjusts the stamina level. When the player engages in actions that drain stamina, the script ensures a gradual decrease in stamina over time, simulating the exertion or depletion of energy. This introduces an element of strategy and resource management, as players need to be mindful of their stamina consumption to avoid exhausting themselves.

## FilthSpawner
![FilthSpawnerImage](https://github.com/Bjornraaf/Linx-Game/blob/develop/Images/FilthSpawner.JPG)
~~~mermaid
flowchart TD;
A(Start) --> B(Set Score.TotalObjects to 0)
B --> C(Spawn)
C --> D[Loop: x += Distance]
D --> E[Loop: z += Distance]
E --> F(Raycast)
F --> G[Hit Ground?]
G -- Yes --> H(SpawnChance > RandomRange?)
G -- No --> E
H -- Yes --> I(Instantiate Object)
H -- No --> E
I --> J[Update Score.TotalObjects]
J --> K[Select Random Index]
K --> C
~~~
This script automates the process of spawning objects in the game scene based on predetermined settings. It ensures that objects are spawned within specified constraints, using raycasting to ensure they are placed on valid surfaces. This adds dynamism to the game world and introduces variety by spawning objects at different positions and orientations.

## Player Input/Controller Scheme
![Player Input Scheme](https://github.com/Bjornraaf/Linx-Game/blob/develop/Images/ControllerScheme.png)
<div style="display: flex;">
    <p style="margin-left: 20px; flex-grow: 1;">We use Unity's New Input System to take care of the player's input and invoke Unity events to call the PlayerController functions.</p>
</div>

## Co-Op
<div style="display: flex;">
    <p style="margin-left: 20px; flex-grow: 1;">We use Unity's Player Input Manager together with our custom Player Join Manager to take care of Co-Op so that players can join in and play the game together.</p>
</div>

## Player Join Manager
~~~mermaid
flowchart TD;
    A((Start))
    B[Set Player Prefab]
    C[Set Player Position]
    D[Update Current Prefab Index]
    E[Check if First Player Joined]
    F[Turn Off Prompt for Player 1]
    G[Check if Second Player Joined]
    H[Turn Off Prompt for Player 2]
    I[Start Game and Timer]
    
    A --> B
    B --> C
    C --> D
    D --> E
    E -- Yes --> F
    F --> G
    G -- Yes --> H
    H --> I
    G -- No --> G
    E -- No --> E
~~~

The PlayerJoinManager manages the joining of players in a game, assigns player prefabs and spawn points, and controls the visibility of HUD elements.

## Prop Placeback
![Prop Placeback](https://github.com/Bjornraaf/Linx-Game/blob/develop/Images/PropPlaceback.png)
~~~mermaid
flowchart TD;
    A((Start))
    B[Turn Off Solid Object]
    C[Set Alpha Value]
    D[Assign Alpha Value]
    E[Check Target Object]
    F[Set IsInPlace to True]
    G[Start FadeOut Coroutine]
    H[Check Alpha Value]
    I[Update Alpha Value]
    J[Set Material Alpha]
    K[Turn On Solid Object]
    L[Destroy Target Object]
    M[Check if Target Object Enters Collider]
    
    A --> B
    B --> C
    C --> D
    D --> E
    E -- Yes --> F
    F --> G
    G --> H
    H -- Yes --> I
    I --> J
    J --> K
    K --> L
    H -- No --> L
    M --> E
~~~
The PropPlaceback and PropPlacebackVariables scripts are being used for handling the placement and replacement of furniture objects in the game.

## Player Area Detector
<div style="display: flex;">
    <img src="https://github.com/Bjornraaf/Linx-Game/blob/develop/Images/PlayerAreaDetector.png" alt="PlayerInputmanager" width="400">
</div>

~~~mermaid
flowchart TD;
    A((Start))
    B[Check Collider Enter]
    C[Is Player in Collider]
    D[Add Player to List]
    E[Check Collider Exit]
    F[Is Player in Collider]
    G[Remove Player from List]
    H[Check Number of Players]
    I[Enough Players in Area]
    J[Display All players are in the area]
    K[Display Not enough players in the area]
    
    A --> B
    A --> E
    B --> C
    C -- Yes --> D
    D --> H
    E --> F
    F -- No --> G
    G --> H
    H --> I
    I -- Yes --> J
    I -- No --> K
    K --> H
~~~

The PlayerAreaDetector script detects how many players are inside the collider area and depending on the total amount of players, detects if all players in the getaway spot, so that the game can end.

## Camera Movement script
the CameraController Script is used to follow the players, allowing them to both constantly be on screen to make sure the players know where they are.

![EntryRoom](https://github.com/Bjornraaf/Linx-Game/blob/main/Images/MainEntrance.png)

~~~mermaid
flowchart TD;
        A((Start))
        B[Check if Targets exist]
        C[No targets found]
        D[End]
        E[Targets found] 
        F[Calculate center position]
        G[Move the camera]
        H[Zoom the camera]
        
        A --> B
        B --> C
        C --> D
        B --> E
        E --> F
        F --> G
        G --> H
        H --> D
~~~

## Filth cleanup script
The "FilthStain" script represents a filth object in a game. It provides functionality to interact with the stain using vacuum and mop players. The script allows for the detection of nearby vacuum and mop players within a specified range and performs actions based on the type of stain (blood or garbage), the script also makes it so the blood stain and filth stain have to be cleaned up using both players to initiate teamwork. It also includes functionality for animation, score tracking, and object destruction.

![Hallway](https://github.com/Bjornraaf/Linx-Game/blob/main/Images/Hallway.png)

~~~mermaid
flowchart TD;
    Start(Start) --> Initialize(Initialize FilthStain)
    Initialize --> Update(Update)
    Update --> CheckVacuum(Check distance to vacuumGuy)
    Update --> CheckMop(Check distance to mopGuy)
    CheckMop --> Brushed(Update IsBrushed)
    CheckVacuum --> CheckGarbagePatch(Check IsGarbagePatch)
    CheckGarbagePatch --> DestroyGarbage(Destroy gameObject)
    CheckVacuum --> CheckBloodStain(Check IsBloodStain)
    CheckBloodStain --> Soaped(Update IsSoaped)
    CheckMop --> CheckBloodStain2(Check IsBloodStain)
    CheckBloodStain2 --> Fade(Fade animation)
    Fade --> UpdateScore(Update ScoreHolder)
    Fade --> DestroyGarbage
    DestroyGarbage --> End(End)

    subgraph FilthStain
        Initialize
        Update
        CheckVacuum
        CheckMop
        CheckGarbagePatch
        CheckBloodStain
        CheckBloodStain2
        Fade
    end

    subgraph ScoreScriptableObject
        UpdateScore
    end

    subgraph Animator
        Soaped
    end

    subgraph GameObject
        vacuumGuy
        mopGuy
    end

    subgraph Gizmos
        OnDrawGizmos
    end

    subgraph Coroutine
        DestroyGarbage
    end

    Start --> FilthStain
    FilthStain --> Coroutine
    Coroutine --> ScoreScriptableObject
    Coroutine --> Animator
    FilthStain --> GameObject
    FilthStain --> Gizmos
    Gizmos --> End

 ~~~
 
 ## Timer + some other UI elements
A countdown timer adds a sense of urgency and time-based challenge to a game. It creates a time constraint that the players must consider when making decisions and taking actions in the level. The score UI element provides players with immediate feedback on their progress in cleaning the level.

![BedRoom](https://github.com/Bjornraaf/Linx-Game/blob/main/Images/Bedroom.png)

~~~mermaid
flowchart TD;
  A((start))
  B[update]
  C[check if TimerOn is true]
  D[Open the elevator doors]
  E[Timer ticks down every second]
  F[Is there more than 0 seconds left?]
  G[Are you in the escape zone?]
  H[Timer stops and you lose]
  I[Timer stops and you win]
  
  A --> B
  B --> C
  C -- No --> B
  C -- Yes --> D
  D --> E
  E --> F
  F -- Yes --> E
  F -- No --> G
  G -- Yes --> I
  G -- No --> H
  ~~~
