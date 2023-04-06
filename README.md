# Asteroids
<br/>
<p align="center">
  <img src="http://emanuelecarrino.altervista.org/images/portfolio/asteroid_1600x900.png" />
</p>
<br/>

## Development
**Engine:** &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Unity  
**IDE:** &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Microsoft Visual Studio  
**Language:** &nbsp;&nbsp; C#
<br/>
> Singleton Pattern: Game Manager and Audio Manager  
> Observer pattern: Events and Delegates (for UI updates), Coroutines  
> Collision detection, sprite animation, sound effects  
> Single player with score system  

## Features
* Implemented the option to toggle  ***`Debug Mode`***. Debug Mode gives info about the asteroid spawners, draws their positions and shows velocity vectors for player and asteroids  
* There are 8 ***`spawners`*** placed in the corners of the octagone cage. Every 1-2 seconds (min and max delay can be adjusted in the Game Manager script) a random spawner among the eight is selected, that spawner will randomly select an angle between a min and a max so that the asteroid won't point directly into the cage and spawns it, with a starting velocity and a torque.  
* When colliding with a projectile or a cage asteroids get destroyed and, if they are big or medium, two smaller asteroids spawn. If the collision is against the cage, the player won't get any point and the random directions selected for the smaller asteroids point always away from the cage. If the collision is with a projectile, the directions are completely random (360 degree).  
* Implemented a function that, comparing the direction where the player accelerates and the actual velocity direction, set the pitch of the sound effect in function of the ***`dot product`*** betwees these two vectors (normalized).  
* Added a check, based on the distance between the player and the spawner, to prevent asteroids to appear on top of the spaceship, causing an unfair game over.  
* Created a custom toggle prefab to switch between ON/OFF options for Debug Mode and Mute Audio.  

<br/>
<br/>

`Code is a little over-commented just to help anyone interested to navigate it better`  
`The Music and the Font are both a tribute to the 1990 Gameboy Tetris`
