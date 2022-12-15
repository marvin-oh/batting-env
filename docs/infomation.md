## Batting Env Information

- Movement of Ball

  <img src="https://user-images.githubusercontent.com/62216628/206841171-370b1d00-43e5-4094-9efb-26cc169edb55.png" width="70%">
 
- Robot Arm Manipulation

  <img src="https://user-images.githubusercontent.com/62216628/206841099-6aa5acb5-9677-4382-9598-7443d6da9444.png" width="70%">

- Observation

  |info|description|size|
  |-|-|-|
  |Ball position|(x, y, z)|3|
  |Ball velocity|(x, y, z)|3|
  |JointZ_1 angle|localRotation.z|1|
  |JointZ_2 angle|localRotation.z|1|
  |JointZ_3 angle|localRotation.z|1|
  |JointY_1 angle|localRotation.y|1|
  |JointY_2 angle|localRotation.y|1|
  |JointY_3 angle|localRotation.y|1|
  |TOTAL|total vector observation|12|
  
- Reward Function
  - +(`ball distance` / 100): if robot hitted the ball, calculate `ball distance` after 5s.
  - -1.0: if robot didn't hit the ball (episode ends).
  - +1.0: if ball goes too far (position.magnitude > 50) by hitting (episode ends).
