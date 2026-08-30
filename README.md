# Trust-In-Everyone
_Trust in Everyone_ is a Project for Brackeys Gamejam 16. A gamejam is an event where game developers get together to make a game according to a specific theme in a specific amount of time. This theme was

`Trust No One`

# The Game
In _Trust in Everyone,_ you play as Coal - an anxious young Dragonborn who is struggling to master the elemntal powers he inherited from his father. When a mysterious indivdual offers to help Coal places his trust in him and gets his powers taken. Returning home in tears, he learns he must Trust No One (Person), but Trust his entire clan to help him out. You have four allies that attack on their own but you can upgrade and issue commands too.

# The Jam
The game jam was 1 week in length and this section describes the reflection on the jam.

## What Went Well
One of the major elements I wanted to learn during this jam was AI controllers, and interfacing with a pathfinding algorithm. This endevor was a success, the enemies have states that can be overriden with a unique state if they requrie a unique attack pattern, like the Magmancers later in the game. The Magmancers are actually one of the harder enemies to fight due to their AI, since they will attempt to keep distance from you at all times.

Additionally, I became very familiar with Curotines within the Unity Engine. Before beginning this project, I only had a surface level understanding of them, but I needed to emply them for the BossAI (which was a last minute addition to the game, and I didn't even set out to learn Curotines). The BossAI was very engaging to program, and was also very successful.

## What Didn't Go So Well
Scope. As always. The Scope on this project was slightly too much for my skill and time. Numerous quality of life features and visual smoothness are not pressent in the game jam release of the game.

Sound Effects where an interesting topic. Following me making a sound effect for _Planet Game_ that went well, I was estactic to try again. The sound effects are not of the quality I was intenting, but with little time left, they became the game jam release sounds. (Also I found out my laptop speakers are broken and I needed to redo audio work with headphones).

The Tilemap art style is, alright at best. I am fairly competent at creating a good looking sprite. And making a good looking tilemap. But I struggle to make a sprite and tilemap that work well together.

Finally, the game jam release has a major bug where sometimes the wave dosen't end believing that there is still 1 enemy left. This is most likely cuased by a race condition between events since this output is not garunteed among runs.
