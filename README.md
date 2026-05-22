# Humans vs Monsters (Turn-Based Strategy Game)
This is a turn based player vs player board game that compete to defeat the opponent's base.

Features
- Mouse-based unit movement
- Math-based combat system
- Currency and units management
  

This is my first project I've built in my freetime before university.
I implemented a combat system that takes difference of two units' power, add a random modifier of few powers to include slight randomness, and transform the victorious unit to a new one that corresponds to the difference.
I built a currency system that you can spend the coin to train units (the more coins are used, the stronger the trained units become)


Balancing First vs Second Player

Through development, I encountered a problem which the player that starts the turn first (Player1) has an advantage over the other player (Player2).
However, giving the Player2 an extra initial coin gave Player2 more significant advantage than I thought.
I solved this problem by introducing neutral units that drop coins. The game will indicate where a neutral unit will spawn on Player1's turn, but only spawn at the start of Player2's turn. After this change, the win rate of 50 trials settled around 50%.
