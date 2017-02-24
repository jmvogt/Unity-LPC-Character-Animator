# Unity LPC Character Animator
Dynamically animate characters using Liberated Pixel Cup sprites 

This asset allows you to animate a character by layering LPC art at runtime. Given that Unity 5+ doesn't allow for you to change the sprites being used within an Animation at runtime, I had to recreate my own LPC animator to handle the swapping of sprites. The alternative was to generate thousands of prefabs and animations, which would have bloated the project's size beyond anything I would feel is realistic for a 2D game.

I made this asset in an attempt to gain more knowledge about 2D gaming and Unity. I'm pretty new to Game Development in general so any tips or recommendations would be greatly appreciated! See my contact info at the bottom.

##Demo
There is not much error handling in the demo, so the model name will need to match a string from the available model list. If you break it, fix your typos and regenerate. This will be improved with the implementation of drop downs / color pickers in v0.2.

[Demo](http://jordanvogt.com/LPC-Animation-Demo.zip) - [Models Available for the Demo](https://raw.githubusercontent.com/jmvogt/Unity-LPC-Character-Animator/master/model-list.txt)

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

##Quick Instructions
(until I get a youtube video up)
  1. Open the project in unity. It will take a bit of time to import the assets as there are ~52k sprites. This will only happen the first time you open the project.
  2. Open the demo scene from the root Assets folder.
  3. From the Hierarchy window, select the AtlasManager GameObject. You should see an Atlas Manager script component for this GameObject in the Inspector window. Click the "Load" button. The first time this may take 15-20 seconds but the next time it should be much faster.
  4. Once all the models have loaded, click on play. All sprites will be packed the first time you play, but the second time this will be skipped.
  5. Replace any slot on the left hand side with a value from the Atlas Manager script's Model List attribute. Additionally, you can provide a RGBT value on the right hand side to color the model.
  6. Click generate

##v0.1 Roadmap (Core Functionality):
  * ~~Mechanism for loading a large number of spritesheets up front~~
  * ~~Create caches to store/lookup independent parts of the LPC character sprites (chest vs body vs helm)~~ 
  * ~~Create a character state to represent racial configurations and equipement~~
  * ~~Render animations whenever the character state has changed or player input~~
  * ~~Reduce the number of duplicate spritesheets by updating the material color dynamically~~
  * ~~Simple GUI to change the character models at runtime~~
  * ~~Move variables for sprite layers (chest vs body vs helm) into dictionaries to shorten code~~
  * ~~Look for more LPC compliant assets that were not included in the online generator tool~~
  * Create instructions/tutorial in a document or YouTube video.
  
##v0.2 Roadmap (UI Tools & Improved Demo)
  * Sprite Support: Add more support for sprites such as weapons (knife/mace/short sword) and children.
  * Sprite Support: Create appendage character types to handle things like lizard/drake fin, tail, horns, etc.
  * Race Creator Tool: Generate new LPC spritesheets (body, eyes) automatically using a color picker!
  * Improved GUI: Drop down selectors and color pallettes for the Demo scene.
  * New Character Controller: Create non-playable character controller that is integrated with the LPC animator
  * Improved Demo Scene: Have NPCs/guards walk the perimiter of the sceen using the LPC animator.
  * New GUI Functionality: Spawn a LPC NPC that walks the perimiter using GUI presets/controls
  
##v0.3 Roadmap (Multiplayer & Animation)
  * Network/Multiplayer: Packing and unpacking of "DNA" for network serialization.
  * Network/Multiplayer: LAN multiplayer request system
  * Network/Multiplayer: Network character controller that can be used during multiplayer
  * Network/Multiplayer: Ability to connect clients and see each other.
  * Animation: Improve animation speed for all action types (some are too fast..)
  
##Contact Info
Jordan
Fanoen@gmail.com
