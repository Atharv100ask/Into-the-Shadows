# Into The Shadows

Into The Shadows is a third-person survival horror game set in a dystopian city overrun by zombies. Sunlight carries harmful radiation that gradually infects the player, forcing careful movement through shadows while managing resources, avoiding or fighting zombies, and ultimately locating the cure before complete infection.

---

## Game Overview

- **Genre:** Survival Horror / Action  
- **Perspective:** Third-person  
- **Platform:** PC  
- **Engine:** Unity  
- **Version Control:** GitHub  
- **Issue Tracking:** GitHub Issues  

---

## Core Gameplay Concept

A scientific failure has caused sunlight to emit dangerous radiation. Exposure increases infection over time, eventually turning survivors into zombies. Players must navigate urban environments strategically, using shadows, buildings, and limited resources to survive long enough to reach the cure.

---

## Gameplay Mechanics

### Infection System
- Exposure to sunlight increases infection over time  
- Staying in shadows slows or halts infection  
- Full infection results in player death  

### Zombies and AI
- AI-driven zombies using finite state machines and A* pathfinding  
- Patrol, chase, and attack behaviors  
- Noise and proximity can attract additional zombies  

### Combat and Resources
- Melee and ranged weapons  
- Limited ammunition and consumables  
- Food restores stamina  
- Infection stabilizers slow infection progression  

### Environment and Navigation
- Urban city layout with multiple routes  
- Buildings provide shade and interior loot areas  
- Fog and lighting enhance atmosphere and visibility constraints  
- World boundaries prevent out-of-bounds traversal  

---

## Objective

- Survive zombie encounters  
- Minimize sunlight exposure  
- Explore the city to locate the cure  
- Reach the final objective before infection becomes fatal  

---

## Controls (Default)

| Action            | Key        |
|-------------------|------------|
| Move              | WASD       |
| Run               | Shift      |
| Interact          | E          |
| Switch Items      | 1–6        |
| Open Map          | M          |
| Pause / Menu      | Esc        |

---

## Implemented Features

- Sunlight-based infection system  
- Zombie AI with patrol and chase behavior  
- NPCs with contextual dialogue and guidance  
- Inventory hotbar with visual feedback  
- Animated player and zombie characters  
- Audio cues for danger, pursuit, and interaction  
- In-game map for navigation  
- Multiple city sections with escalating difficulty  

---

## Development Team

- **Hieu Cao** – Project Manager  
- **Alejandro Bravo** – Programmer  
- **Atharv Kokate** – Tech Lead / Designer  

---

## Development Notes

- Initially designed as a multiplayer co-op experience  
- Refactored into a single-player game due to multiplayer complexity and balancing challenges  
- Iterative development informed by alpha and beta playtesting feedback  
- Strong emphasis on visual cues, animation quality, and environmental storytelling  

---

## Known Limitations and Future Work

- Improved UI clarity and player guidance  
- Additional zombie types with unique behaviors  
- Expanded level variety (e.g., underground environments)  
- Enhanced visual and audio feedback for infection and damage states  

---

## How to Run the Project

Download the .zip file --> Execute the .exe game file

OR

1. Clone the repository:
   ```bash
   git clone https://github.com/Hqcao1/Into-The-Shadows.git
   ```
2. Open the project in Unity Hub

3. Use the Unity version specified during development

4. Load the main scene and press Play
