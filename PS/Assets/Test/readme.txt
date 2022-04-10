The Test Project

1. Setup/Install

Note: This assumes you have Unity installed on your machine. Any recent version of Unity should do.

Create a new Unity project and in the assets folder, create a new Test folder and copy the files from this zip into it.
Create a GameObject in your Unity scene and add the TestMain MonoBehaviour as a component to it. (AddComponent).
You should be able press Play in Unity and see the project in action. You might need to switch to Scene view or click Gizmos so they are visible from your camera.

2. Overview

The test is a simple framework that consists of World, Entity and EntityBehaviour classes. It uses the Unity debug rendering through Debug.Line to avoid any asset requirements. The test focuses on code and less on the use of the Unity engine, so please do not introduce any assets or asset dependencies other than any needed for settings/configuration of the system.

The default initial state of the project is that there are two entities in a single world, and one is orbiting the other (think of planetary bodies).

The test consists of fixing and refactoring the existing source code as well as writing additional code to achieve the tasks outlined below. 

3. Core Test Tasks

 a. Fix an issue. The entity doesn't appear to orbit the "planet". The desired behaviour is to orbit the "planet". 
 b. Many moons. Add multiple entities to the world at different positions and velocities. Make them visually different.
 c. Simple space ship. Make one entity be controlled by the user - single button - just apply thrust along its current vector.
 d. Smart satellites. Make an autonomous entity behaviour that tries to apply thrust (positive or negative) along its forward axis to keep a perfect stable orbit. 

4. What to focus on

Please consider the test project as a production development project. We are looking to get your version of a clean/neat code solution that can scale and be maintainable in the future. You should demonstrate understanding of object oriented programming principles, think about the class relationships and how the architecture can be improved. Where possibly, motivate your thinking and intentions in comments.

Consider that your changes will be peer-reviewed by your fellow team members. 

5. Time management

You can spend as long as you like on the test. However, we would like your record the time spent on each task and provide it along with your test results back to us.

6. Impress us with a "show off" task

If you have completed the core tasks above and still have time to spare, we invite you to come up with an enhancement or a feature that you think will show off your skills and impress us. As before, please, focus on quality of code and record the time you spend on it. 

We would much rather this task is of your own design but if you are struggling for ideas here are some simple examples:

    - Implement code that lets entities that come closer than certain distance apply gravity to each other, affecting each other's orbits.
    - Implement visual trails so that we can see the path space bodies have travelled. 
    - Allow switching player control between entities (returning them back to autonomous when not controlled by player) so we can tinker with orbits.  
    - Implement a solar system with moons orbiting planets and planets orbiting suns.
    
7. Send us the Result

Zip the Test folder (the folder this file is in) and send it back to us. Please, do not send the entire Unity project.
Provide task timings and any notes/comments that would help us understand the motivation for your technical decisions.


Best of luck and we look forward to hearing from you soon!
