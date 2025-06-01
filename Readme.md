## **Unity Game **

### **Game Overview**

**Game Designer:** Najib Ahmed  
 **Game Title:** *Crazy Breakout*  

---

### **Elevator Pitch**

My game is a chaotic twist on the classic breakout. Blast through colorful bricks using a bouncing ball and chaotic power-ups like chaos ball storms, one-hit break modes, and full directional control over your ball. Use the power-ups to beat each level before your 3 lives run out\! Try to beat all 5 levels.

---

### **Theme Interpretation**

The game embraces the theme of **"Strange Powerups"**. While players begin with classic breakout mechanics where there is a ball bouncing off a paddle and trying to break all the bricks, my game introduces various strange power-up bricks that do different things, turning the game chaotic and making every level a unique challenge. This theme is shown through the “pink” power-up, where you can directly control the ball with the arrow keys, and the “purple” power-up, where it spawns a bunch of different balls that shotgun into the surrounding bricks, destroying them instantly, along with other power-ups.

---

### **Controls**

| Action | Key |
| ----- | ----- |
| Move Paddle | `←` / `→` |
| Control Ball (Powerup) | Arrow Keys |
| Launch Ball | Automatic after reset |

---

### 

### **MDA Framework**

#### **Aesthetics**

* **Challenge** – The player must skillfully control the paddle, manage multiple power-ups, and avoid missing the paddle while progressing through increasingly difficult levels. With only 3 lives per level, success requires focus, timing, and adaptability.

* **Discovery** – Players uncover new types of power bricks (e.g., pink, purple, black) with surprising effects. These discoveries encourage experimentation and replay as players learn how each mechanic affects gameplay.

* **Abnegation (Submission)** – Like many arcade games, this brick breaker offers a flow-state experience where players can lose track of time. The game provides simple, repetitive actions that are relaxing yet engaging.

#### **Dynamics**

* Players adjust their strategy as different colored bricks are destroyed.

* Power bricks (purple, pink, black) change gameplay dynamics, like overwhelming the screen or freezing the paddle.

* Players engage in risk-reward choices, for example, trying to trigger pink brick without dying.

#### **Mechanics**

* **Ball Movement:** Consistent speed with physics-based trajectory.

* **Paddle Bounce:** Angle depends on contact point.

* **Brick Destruction:** Color-based hierarchy unless hit by power mode or chaos balls.

* **Lives & Reset:** 3 lives per level; game restarts from Level 1 if lost.

* **Levels:** 5 levels, with a "You Win" screen on Level 5\.

* **Power-ups:**

  * **Black Brick** \= One-hit kill mode for 15s.

  * **Purple Brick** \= Spawns 20 balls for chaos.

  * **Pink Brick** \= Player controls ball.

  * **Unbreakable Bricks** \= Cannot be destroyed.

---

### **External Resources**

#### **Assets**

* **Pop Sound Effect** — [https://freesound.org/people/Vilkas\_Sound/sounds/463394/](https://freesound.org/people/Vilkas_Sound/sounds/463394/)

* **Cool Background** — [FreePik](https://www.freepik.com/free-vector/purple-alien-space-planet-game-cartoon-background_41371868.htm#fromView=keyword&page=1&position=0&uuid=6d46d4cd-a166-4246-b059-7c8ba4e198e6&query=2d+Game+Background+Space)

#### **Code**

* **Base Brick Breaker i**nspired by:  
   [Zigurous Brick Breaker GitHub](https://github.com/zigurous/unity-brick-breaker-tutorial)  
  * I used the base brick breaker physics, such as the paddle and ball bouncing, but I changed how the bricks worked and added a bunch of different power-ups. 


* **Custom Features Implemented:**  
  * Brick design/shape

  * Different Brick Power ups

  * Ball control mechanic

  * Persistent level and life logic

  * Visual effects and UI enhancements

