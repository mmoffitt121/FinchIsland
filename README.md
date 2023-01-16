# Setting up 
This section details how to install and set up your environment for development. Get the free/educational/personal version of everything.
## For General Development
### Step 1
First, download and install:
- [Unity Hub](https://unity.com/download)
- Unity 2021.3.x (LTS)
  - From Within Hub
  - The x doesn't matter, newer is nicer.
- [Visual Studio](https://visualstudio.microsoft.com/free-developer-offers/) (2019 or 2022, doesn't matter, newer is nicer)
  - When Running the Visual Studio Installer, make sure "Game Development With Unity" is selected. This will make your coding experience a lot nicer.
  - NOT Visual Studio Code.
- Git client of your choice.
  - Recommend [Github Desktop](https://desktop.github.com/), but whatever you prefer works just as well.
### Step 2
Now that you have Unity installed, you should be able to access the project.
- Clone this repository
  - Get the link on the repository's frontpage here:
![Image](https://i.imgur.com/0DzUZzk.png)
- Open Unity Hub
- Click 'Open' and navigate to the base folder of the repository. Click Open.
![Image](https://i.imgur.com/Wj5xUw4.png)
### Step 3
You've now opened the project. There might not be anything in the scene in front of you. Probably because there's not a scene open. To open a scene, in the project window at the bottom, navigate to Assets -> Scenes, and Open "Debug." (Note: This scene may not be there if it was moved/replaced/renamed.)

Click the Play Icon at the top, and you'll be able to see how the project plays so far.
### Step 4
Get Visual Studio working with Unity. Guide [here](https://learn.microsoft.com/en-us/visualstudio/gamedev/unity/get-started/getting-started-with-visual-studio-tools-for-unity?pivots=windows).

Make sure that double-clicking scripts in the project window opens the scripts in VS, and that your VS is autocompleting Unity classes/functions for you. This will make development a lot easier.
## For AI Development
This is a bit tricker. 

### Step 1
Follow the instructions at [this link](https://www.immersivelimit.com/tutorials/unity-ml-agents-setup). This will help you setup Unity's MLAgents repository, and help you get to know MLAgents. Do the [second part too](https://www.immersivelimit.com/tutorials/ml-agents-python-setup-anaconda).

If you find issues, make sure you're using Python 3.7.9 64-bit.
