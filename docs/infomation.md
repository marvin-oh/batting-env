## Batting Env Information

- Movement of Ball

  <img src="https://user-images.githubusercontent.com/62216628/207944237-3ce35867-89f5-4d8d-a52f-7edeecdd2bb6.png" width="70%">


- Robot Arm Manipulation

  <img src="https://user-images.githubusercontent.com/62216628/207948545-f16a2662-05a8-426f-8024-ad0ee4f29443.png" width="70%">


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
  - -1.0: if robot didn't hit the ball (episode ends).
  - +(`ball distance` / 50): if robot hitted the ball, calculate `ball distance` after 5s.
  - +1.0: if ball goes too far (position.magnitude > 50) by hitting (episode ends).
  - -1.0: if ball out of foul line when calculating (episode ends).
