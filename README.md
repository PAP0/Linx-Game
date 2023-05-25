# Linx-Game
Een voorbeeld repository voor het examenwerk

In deze repository vind je de informatie over het examen project.

Omschrijf de examenopdracht evt de klant en wat het doel voor de klant is.
Omschrijf ook beknopt wat het idee van je game is. 
Een complete en uitgebreide beschrijving komt in het functioneel ontwerp (onderdeel van de [wiki](https://github.com/Bjornraaf/Linx-Game/wiki))

# Geproduceerde Game Onderdelen

Bjorn Ravensbergen:
  * x
  * x
  
Patryk Podworny:
  * [Player Input/Controller Scheme](https://docs.unity3d.com/Packages/com.unity.inputsystem@0.2/manual/index.html)
  * [Co-Op](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Components.html)
  * [Player Join Manager](https://github.com/Bjornraaf/Linx-Game/blob/develop/Assets/Code/Scripts/Co-op/PlayerJoinManager.cs)
  * [Prop Placeback](https://github.com/Bjornraaf/Linx-Game/tree/develop/Assets/Code/Scripts/Cleaning/Props)
  * [Player Area Detector](https://github.com/Bjornraaf/Linx-Game/blob/develop/Assets/Code/Scripts/Co-op/PlayerAreaDetector.cs)

Ties Postma:
  * x
  * x

## Player Input/Controller Scheme
![Player Input Scheme](https://github.com/Bjornraaf/Linx-Game/blob/develop/Images/ControllerScheme.png)
<div style="display: flex;">
    <img src="https://github.com/Bjornraaf/Linx-Game/blob/develop/Images/PlayerInput.png" alt="Player Input Scheme" width="400">
    <p style="margin-left: 20px; flex-grow: 1;">We use Unity's New Input System to take care of the player's input and invoke Unity events to call the PlayerController functions.</p>
</div>

## Co-Op
<div style="display: flex;">
    <img src="https://github.com/Bjornraaf/Linx-Game/blob/develop/Images/PlayerInputManager.png" alt="PlayerInputmanager" width="400">
    <p style="margin-left: 20px; flex-grow: 1;">We use Unity's Player Input Manager together with our custom Player Join Manager to take care of Co-Op so that players can join in and play the game together.</p>
</div>

## Player Join Manager
![Player Join Manager](https://github.com/Bjornraaf/Linx-Game/blob/develop/Images/PlayerJoinManager.png)
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
![Player Area Detector](https://github.com/Bjornraaf/Linx-Game/blob/develop/Images/PlayerAreaDetector.png)
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
