# Unity LPC Character Animator
Dynamically animate characters using Liberated Pixel Cup sprites

I started this project as an attempt to familiarize myself with 2D game development. Since I'm a pretty lousy pixel artist and I'm reusing so much free art, I'm going to maintain this as an independent asset for the OpenGameArt/Liberated Pixel Cup communities.

For the most part, this asset will allow you to animate a character by layering LPC art at runtime. Given that Unity 5+ doesn't allow for you to change the sprites being used within an Animation at runtime, I had to recreate my own LPC animator to handle the swapping of sprites. The alternative was to generate thousands of prefabs and animations, which would have bloated the project's size beyond anything I would feel is realistic for a simple 2D game.

Given that I'm relatively new to Unity and Game Development, I would love your feedback on how I could improve this plugin. Whether it be from better practices for Unity or how I could structure my classes, I am eager to get feedback from more experienced game developers.

##OpenGameArt and the Liberated Pixel Cup
OpenGameArt (OGA) is a community for users to share a variety of free/open source art. The Liberated Pixel Cup (LPC) was a contest kickstarted by OGA to create free art that was judged to ensure it was of the appropriate quality and matched a particular art style.

* OpenGameArt Website: http://opengameart.org/
* Liberated Pixel Cup Homepage: http://lpc.opengameart.org/

##Special Credit
This goes to the Online Universal LPC Spritesheet project and the Universal LPC Spritesheet Unity Importer project, as I would have given up after trying to manually import the beastly number of spreadsheets.

 * All of the artists contributing to LPC! A running list of those who are responsible for the sprites reused in this project can be found in authors-sprites.txt
 * Online LPC Character Generator Tool: http://gaurav.munjal.us/Universal-LPC-Spritesheet-Character-Generator/
  * Github: https://github.com/jrconway3/Universal-LPC-spritesheet
 * Unity LPC Spritesheet Importer: https://bitcula.com/universal-lpc-spritesheet-unity-importer/
  * Github: https://github.com/bitcula/Universal-LPC-Spritesheet-Unity-Importer

##v0.1 Roadmap:
  * ~~Reduce the number of duplicate sprites by updating the material color dynamically~~
  * ~~Mechanism for loading more sprites upfront~~
  * ~~Pack LPC sprites appropriately into larger spritesheets (if needed from prior step)~~
  * ~~Create caches to store/lookup independent parts of the LPC character sprites (torso vs body vs weapon)~~  
  * ~~In memory tests (how much memory is used with the above changes)~~
  * ~~File system tests (measure how large the game is with above changes)~~
  * ~~Simple GUI with drop downs that will allow you to change the CharacterDNA at runtime~~
  * Use reflection on classes referencing each DNA block
  * Apply design patterns / clean up hacked up code
  * Clean up / finish TODOs in code
  * Look for LPC compliant assets that were not included in the online generator tool.
  * Create instructions/tutorial in a document or YouTube video.
  
##v0.2 Roadmap
  * Unit tests for v0.1... :)

  * Keep a running list of "imported" sprites in either a flat file or DB for eventual mapping.
  * Drop down selectors and color pallettes for the Demo scene.
  * Create non-playable character controller that is integrated with the LPC animator
  * Have NPCs/guards walk the perimiter of the sceen using the LPC animator.
  
  * Packing and unpacking of "DNA" for network serialization.

##v0.3 Roadmap:
 
