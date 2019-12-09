# Day 4: Secure Container
In theory the tasks for the day was easy and fun. We had to get the possible combinations of a 6-digit passcode based on a few simple rules. In practise I had some problems getting my loops to to work, but after a lot of debugging I go my solution to work. It ended up to be a pretty straight forward brute force solution, not much cleverness involved.

### Personal Stats for the Day
**Part 1**

 Time                  | Rank | Score 
-----------------------|------|-------
 03:09:48              | 8513 | 0     

**Part 2**

 Time                  | Rank | Score 
-----------------------|------|-------
 03:25:21              | 7297 | 0     

## Puzzle 1
#### Assignment
You arrive at the Venus fuel depot only to discover it's protected by a password. The Elves had written the password on a sticky note, but someone threw it out.

However, they do remember a few key facts about the password:

- It is a six-digit number.
- The value is within the range given in your puzzle input.
- Two adjacent digits are the same (like `22` in `122345`).
- Going from left to right, the digits **never decrease**; they only ever increase or stay the same (like `111123` or `135679`).

Other than the range rule, the following are true:

- `111111` meets these criteria (double `11`, never decreases).
- `223450` does not meet these criteria (decreasing pair of digits `50`).
- `123789` does not meet these criteria (no double).

**How many different passwords** within the range given in your puzzle input meet these criteria?

#### Solution
I've created two methods containing loops where one was checking that reading from left to right the right value was never smaller then the left value next to it and the other checked for adjacents. The correct answer for me was **`1764`**.

## Puzzle 2
#### Assignment
An Elf just remembered one more important detail: the two adjacent matching digits **are not part of a larger group of matching digits**.

Given this additional criterion, but still ignoring the range rule, the following are now true:

- `112233` meets these criteria because the digits never decrease and all repeated digits are exactly two digits long.
- `123444` no longer meets the criteria (the repeated `44` is part of a larger group of `444`).
- `111122` meets the criteria (even though `1` is repeated more than twice, it still contains a double `22`).

**How many different passwords** within the range given in your puzzle input meet all of the criteria?

#### Solution
Well this was basicly the same as puzzle 1 with a simple change to the adjacent rule. The rule had to be changed to check for adjacents that did count exactly `2`. The correct answer for this puzzle was **`1196`**.
