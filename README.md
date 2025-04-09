Design Document: https://docs.google.com/document/d/11-whKk_-YXsfpUOFhhTdsZ2soEV-NOZ_rrI_nBf3cGA/edit?usp=sharing
Assignment - 6 
Updated level design:
Added pathway AI zombies with Mechanium body in our city to patrol around certain areas and once you get close they start to attack you.
Added red area light around zombies to signify danger.
Added physics to the road to move faster when near zombies. 
NPC with FSM and AI pathfinding (a* algorithm) to either follow Player or help locate Food items


Updated character design:
Added zombie with 3 animations(idle walk, scream, and run)
Changed starting player with 4 animations(idle, running, agony, and writhing in pain).
Added tutorial player with idle animation.
Tutorial player has dialogue pop up based on user state using FSM
Updated physics:
Fog is part of 3D physics.
Doors implemented used the joint constructs.
Added road where you can run faster(no friction) where zombies are at.
Updated lights: 
Red area light near zombies to signify danger.
