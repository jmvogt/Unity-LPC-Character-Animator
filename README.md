# Unity-LPC-Character-Animator
Dynamically animate characters using Liberated Pixel Cup sprites

This is a project I've started as an attempt to familiarize myself with 2D game development. Since I'm a pretty lousy pixel artist, I would like this to be an independent component that could be given back to the OpenGameArt/Liberated Pixel Cup communities.

v0.1 Roadmap:
  * Pack LPC sprites appropriately into larger spritesheets
  * Create caches to store independent parts of the LPC character
  * Garbage collection to cleanup unused objects from caches
  * Explore asset bundles and whether they would improve performance
  * In memory tests (how much memory is used with the above changes)
  * File system tests (measure how large the game is with above changes)
  * Functions to update a particular block of character DNA and have it propagate down to the animation. As of right now, the CharacterDNA is translated to AnimationDNA at the start.
  * Use reflection on classes referencing each DNA block
  * Start planning v0.2 or my next game component
  
