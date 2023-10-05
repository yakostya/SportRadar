# SportRadar

Assumptions:

1. Synchronization wasn't take into considiration, assumed that work with library is happening from a single thread.
2. UpdateScore - can decrease score, can be helpfull if someone made a mistake during updating score.
3. Multiple matches with the same teams can be started simultaniosly for now. (not sure that team names are unique and there can not be two teams with same names)

Task Description:
You are working in a sports data company, and we would like you to develop a new Live Football
World Cup Scoreboard library that shows all the ongoing matches and their scores.
The scoreboard supports the following operations:
  1. Start a new match, assuming initial score 0 – 0 and adding it the scoreboard.
This should capture following parameters:
  a. Home team
  b. Away team
2. Update score. This should receive a pair of absolute scores: home team score and away team score.
3. Finish match currently in progress. This removes a match from the scoreboard.
4. Get a summary of matches in progress ordered by their total score. The matches with the
same total score will be returned ordered by the most recently started match in the
scoreboard.
For example, if following matches are started in the specified order and their scores
respectively updated:
a. Mexico 0 - Canada 5
b. Spain 10 - Brazil 2
c. Germany 2 - France 2
d. Uruguay 6 - Italy 6
e. Argentina 3 - Australia 1
The summary should be as follows:
1. Uruguay 6 - Italy 6
2. Spain 10 - Brazil 2
3. Mexico 0 - Canada 5
4. Argentina 3 - Australia 1
5. Germany 2 - France 2

