# Day 1: The Tyranny of the Rocket Equation
I started the challenge around 12pm. I would rate both puzzles easy and to solve them you had to use pretty straight forward math. Actually the function for calculating the values was basicly written down in the assignment.
Unfortunatly I was not fast enough today to score any points in the public leaderboard.

### Personal Stats for the Day
**Part 1**

 Time                  | Rank | Score 
-----------------------|------|-------
 06:56:28              | 9718 | 0     

**Part 2**

 Time                  | Rank | Score 
-----------------------|------|-------
 07:33:24              | 9138 | 0     

## Puzzle 1
#### Assignment
The Elves quickly load you into a spacecraft and prepare to launch.

At the first Go / No Go poll, every Elf is Go until the Fuel Counter-Upper. They haven't determined the amount of fuel required yet.

Fuel required to launch a given module is based on its mass. Specifically, to find the fuel required for a module, take its mass, divide by three, round down, and subtract 2.

For example:

    For a mass of 12, divide by 3 and round down to get 4, then subtract 2 to get 2.
    For a mass of 14, dividing by 3 and rounding down still yields 4, so the fuel required is also 2.
    For a mass of 1969, the fuel required is 654.
    For a mass of 100756, the fuel required is 33583.

The Fuel Counter-Upper needs to know the total fuel requirement. To find it, individually calculate the fuel needed for the mass of each module (your puzzle input), then add together all the fuel values.

What is the sum of the fuel requirements for all of the modules on your spacecraft?

#### Solution
I created a simple .NET Core Console application, wrote some interactions for the user with the console and created the method for calculating the required fuel for a given mass. After manualy entering some values and testing that the calculator was working, propably the "hardest" of the day had to be done. 

I thought to import the `puzzle inputs` with a `HttpClient` Get call. As I was done building `ImportMasses.GetMassesFromWeb(string url)` and started the application, the client throws a `BadRequest (400)`.

Well, I didn't continue researching why and if there would have been a workaround for this. It surely must have been some kind of permission issue. But the solution for was to just add a textfile to the project which contained the `puzzle inputs`. After switching from a web query to read the values from the added file, it worked!

The output was *`3420719`* and it was the correct answer to the puzzle.

## Puzzle 2
#### Assignment
During the second Go / No Go poll, the Elf in charge of the Rocket Equation Double-Checker stops the launch sequence. Apparently, you forgot to include additional fuel for the fuel you just added.

Fuel itself requires fuel just like a module - take its mass, divide by three, round down, and subtract 2. However, that fuel also requires fuel, and that fuel requires fuel, and so on. Any mass that would require negative fuel should instead be treated as if it requires zero fuel; the remaining mass, if any, is instead handled by wishing really hard, which has no mass and is outside the scope of this calculation.

So, for each module mass, calculate its fuel and add it to the total. Then, treat the fuel amount you just calculated as the input mass and repeat the process, continuing until a fuel requirement is zero or negative. For example:

    A module of mass 14 requires 2 fuel. This fuel requires no further fuel (2 divided by 3 and rounded down is 0, which would call for a negative fuel), so the total fuel required is still just 2.
    At first, a module of mass 1969 requires 654 fuel. Then, this fuel requires 216 more fuel (654 / 3 - 2). 216 then requires 70 more fuel, which requires 21 fuel, which requires 5 fuel, which requires no further fuel. So, the total fuel required for a module of mass 1969 is 654 + 216 + 70 + 21 + 5 = 966.
    The fuel required by a module of mass 100756 and its fuel is: 33583 + 11192 + 3728 + 1240 + 411 + 135 + 43 + 12 + 2 = 50346.

What is the sum of the fuel requirements for all of the modules on your spacecraft when also taking into account the mass of the added fuel? (Calculate the fuel requirements for each module separately, then add them all up at the end.)

#### Solution
Well the solution to this puzzle was mainly copy & paste from the previous puzzle and the addion of an additional calculation method. My solution was a simple while loop, which calculated the fuel for the fuel until the fuel remainder was less then or equal to 0.

The fuel of the fuel of the fuel of the fuel of the fuel..... break; was added to the fuel of the mass and the sum of those was *`5128195`*. This ended up to be the correct answer to second puzzle of the day.

