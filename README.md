# Dialog

## Introduction
Dialog is a flexible dialogue system for Unity.
It consists of all the files needed to make the dialogue system work. A Unity Package file is also included.

## How to use Dialog
0. Fix all the errors if there are any
1. Import the Unity Package file into Unity
2. Add the prefab "Dialog Box" to the hierarchy </br>
  ![image](https://user-images.githubusercontent.com/41127485/137631852-bc0e4778-ed4e-4786-99eb-ef45bf792899.png)
3. Add the "Dialog UI" and the "Typewriter Effect" script to the Canvas </br>
  ![image](https://user-images.githubusercontent.com/41127485/137632132-b8426647-c314-4203-9ecb-7f734a32f112.png) </br>
  **Don't forget to bind the variables as shown on the image**
4. Add this code to your PlayerController script and assign all the variables </br>
   ```C#
   public DialogUI dialogUI;
   public DialogUI DialogUI => dialogUI;
   public Interactable interactable {get; set;}
   
   void Update(){
        if(Input.GetKeyDown(KeyCode.E) && !dialogUI.IsOpen){
             Interact();
        }
   }
   
   public void Interact(){
        interactable?.Interact(this);
    }
   ```

## How to get something to trigger a dialogue
1. Add a box collider and mark it as trigger
2. Add the Dialog Activator script to it
   ![image](https://user-images.githubusercontent.com/41127485/137632719-3e67037d-03f7-41b8-9f52-55173d1988aa.png)
3. Create a Dialog Object and assign it to the "Dialog Object" variable (view the image above)

## How to create a Dialog Object
1. Create a Dialog Object in the Project Window of Unity </br>
   Right Click > Create > Dialog > Dialog Object
2. Variables Explanation
   ![image](https://user-images.githubusercontent.com/41127485/137633102-91366af1-44ae-4c3b-a4a6-41742f7d22f2.png) </br>
   1 Put the dialogue text here. Use one Element for each dialogue line (one for every character)  
   2 Put the speaker name here. The first speaker will be assigned to the first dialogue text  
   3 Put the speaker color here. The first color will be assigned to the first speaker  
   4 Put the events that will be invoked at the start of the dialogue  
   5 Put the events that will be invoked at the end of the dialogue  
   6 You can assign a Playable director here. It will be paused during the dialog.  
