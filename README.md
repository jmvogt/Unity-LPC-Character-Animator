# Unity LPC Character Animator
Dynamically animate characters using Liberated Pixel Cup sprites

This asset allows you to animate a character by layering LPC art at runtime. Given that Unity 5+ doesn't allow for you to change the sprites being used within an Animation at runtime, I had to recreate my own LPC animator to handle the swapping of sprites. The alternative was to generate thousands of prefabs and animations, which would have bloated the project's size beyond anything I would feel is realistic for a 2D game.

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
  * ~~Create caches to store/lookup independent parts of the LPC character sprites (chest vs body vs helm)~~ 
  * ~~Simple GUI that will allow you to change the character models at runtime~~
  * ~~Move variables for sprite layers (chest vs body vs helm) into dictionaries to shorten code~~
  * ~~Create a character state to represent racial configurations and equipement to generate animations at runtime~~
  * Look for LPC compliant assets that were not included in the online generator tool.
  * Create instructions/tutorial in a document or YouTube video.
  
##v0.2 Roadmap
  * Unit tests for v0.1... :)
  * Exception handling?
  * Race Creator Tool: Generate new LPC spritesheets (body, eyes) automatically using a color picker!
  * Improved UI: Drop down selectors and color pallettes for the Demo scene.
  * New Character Controller: Create non-playable character controller that is integrated with the LPC animator
  * Improved Demo Scene: Have NPCs/guards walk the perimiter of the sceen using the LPC animator.
  * New UI Functionality: Provide UI to spawn a new LPC character that walks the perimiter using presets/controls
  
##v0.3 Roadmap:
  * Network/Multiplayer: Packing and unpacking of "DNA" for network serialization.
  * Network/Multiplayer: LAN multiplayer request system
  * Network/Multiplayer: Network character controller that can be used during multiplayer
  * Network/Multiplayer: Ability to connect clients and see each other.
  * Animation: Improve animation speed for all action types (some are too fast..)
  
 
