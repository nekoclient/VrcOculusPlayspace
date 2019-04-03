hi this is the repository for vrcplayspacemover

this is an attempt to revive playspacemover for vrchat,
~~though it has no 3d "hold button and move controller"
functionality in it yet (i suck at math ok?)~~ it replicates how steamvr's playspacemover functions [example vid](https://b.catgirlsare.sexy/crwT.mp4)

~~basically, hold A to go up, hold X to go down.~~ hold a or x and then move the controller on which you're holding the button pressed to move around

doesn't require any game files to be modified.
(it uses my [UnityAssemblyInjector](https://github.com/avail/UnityAssemblyInjector) to load the code in)

~~will be broken when the game updates, unless
someone feels like doing reflection for the needed functions (OVRInput class)~~ VRChat updated, it is now in a separated (unobfuscated) dll, so it will function when the game updates, unless they decide to obfuscate it again

downloads in releases tab, drag and drop to vrchat's folder in `Oculus\Software\Software\vrchat-vrchat`.