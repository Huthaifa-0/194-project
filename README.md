# Comp 194 Project (Ctrl + Alt + Recycle)
 Ctrl + Alt + Recycle: a sustainability VR game
Team members: Huthaifa Mohammad, Irene Gallini, Elma Mostic, Elyse Quigley 

Project description:
    We designed this VR project with the aim to raise awareness about the impact of pollution on the environment. Through a collaborative gameplay, this interactive sorting game will show the users the positive impact of beach cleaning activities through visible environmental restoration. 

How to play: 
    This VR game is designed for Meta Quest and can be run by cloning the repository and opening it on UnityHub. After that it can be built and run in Unity. 

Choosing your environment:
    When the game opens you and another player will be able to choose in which environment you want to play on: a beach or a coral reef. Whoever chooses the coral reef environment will be the host of the server and must select it first, after that the other player can join the beach environment as the guest. 

Recycle in the scene:
    Once you are in the scene you will be able to move around by using the joysticks on the controller. With the right controller you can move back and forward, with the left one you can move left and right while rotating. After this, you can try to reach for the trash, when you move your hand forward the controller will extend to grab the object on the ground. After that you have to sort it in the right bin! Green is for plastic and red is for metal.

What you will see:
    As you sort the trash in the right bin the plants around you will become healthier. In the coral reef the corals will go from white and damaged by the metal trash back to a vibrant red, while in the beach the plants will go from a dry yellow, damaged by the plastic, to a lively green. You can check your progress by turning your palm toward your face to activate the hand menu, here you will see progress bars for both environments and your score.

Remember to collaborate:
    The score you see only shows the pieces of trash you have sorted correctly on your own, but this is a collaborative game! When a player in either scene sorts plastic correctly this will be reflected by the Beach Health bar and the bushes will get greener, the same is true for the metal and corals. Your aim in this game is to collaborate with the other player, even if you can’t talk to them, by cleaning up your scene to benefit both of your environments. 

End:
Once all the trash in the scenes has been collected, the game will end. 

How this was achieved: 
Scene structure:
The players start in the SetUpScene, then they can choose to move to BeachMain or WaterMain
TrashBins and trash objects
    Colliders in the bin and the trash objects allow to check when an object is sorted
    Check if the sorting is correct by seeing if the metal and plastic trash object’s tag matched the bin’s

Color changing plants
    Reduce alpha value of the overlay in the UpdateTransparency() method, called when the collider in the bin is triggered
    Organized in PlantObjectList in the GameManager script
    
Scripts    
    GameManager Script
    Runs through both scenes and connects the actions of both players
    Network variables
    Communicate the value of the health of both environments to update plant transparency and hand menu components

