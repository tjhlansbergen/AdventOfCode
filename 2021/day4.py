import helpers
from typing import List, Union

input = helpers.split_file_on_blanklines('inputs/day4')

balls = [int(ball) for ball in input[0].split(",")]

# covert the boards into list-of-list-of-list[int,bool]], all in one go,
# by getting all but the first block of text, split on newlines, remove padding, split in space, convert to int and pack in list (since tuples are immutable), the bool indicates if striped through,
# that way the original number is kept, which is likely needed for part two...
boards = [[[[int(number), False] for number in row.strip().replace("  ", " ").split(" ")] for row in board.split("\n")] for board in input[1:len(input)]]

# mark a number on a list of boards
def mark(ball_number: int, boards: List[List[List[List[Union[str, int]]]]]):
    for board in boards:
        for row in board:
            for board_number in row:
                if board_number[0] == ball_number:
                    board_number[1] = True

# return the first winning board if any
def bingo(boards: List[List[List[List[Union[str, int]]]]]) -> List[List[List[Union[str, int]]]]: 
    for board in boards:
        # horizontal
        for row in board:
            if all(number[1] for number in row):
                return board
        # vertical
        rotated = list(zip(*reversed(board)))
        for column in rotated:
            if all(number[1] for number in column):
                return board
    return None

# returns the score for a (winning) board
def score(board: List[List[List[Union[str, int]]]]) -> int:
    # manually flatten the board to keep the most-inner list
    return sum([sum([number[0] for number in row if number[1] == False]) for row in board])
    

# part one
for ball in balls:
    mark(ball, boards)
    winner = bingo(boards)
    if winner is not None:
        print(ball * score(winner))
        break


# part two

# finds all winners (in order of appearance)
def bingo(boards: List[List[List[List[Union[str, int]]]]]) -> List[List[List[List[Union[str, int]]]]]: 
    winners = []
    for board in boards:

        next_board = False

        # horizontal
        for row in board:
            if all(number[1] for number in row):
                winners.append(board)
                next_board = True
                break

        if next_board:
            continue

        # vertical
        rotated = list(zip(*reversed(board)))
        for column in rotated:
            if all(number[1] for number in column):
                winners.append(board)
                break
            
    return winners
    
# restart boards:
boards = [[[[int(number), False] for number in row.strip().replace("  ", " ").split(" ")] for row in board.split("\n")] for board in input[1:len(input)]]

ball_nr = 0
while len(boards) > 0:      # assuming all boards get to win eventually
    mark(balls[ball_nr], boards)
    last_winners = bingo(boards)

    # pop winners
    for x in last_winners:
        boards.remove(x)

    ball_nr += 1

print(balls[ball_nr-1] * score(last_winners[-1]))
    



