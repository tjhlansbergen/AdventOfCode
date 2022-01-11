import helpers
import statistics

lines = helpers.list_from_file('2021/inputs/day10')

pairs = {"(": ")","{": "}","[": "]","<": ">"}


# part one

prizes = {")": 3,"]": 57,"}": 1197,">": 25137}
stack = []
prize = 0

for line in lines:
    for i in line:
        if i in pairs.keys():
            stack.append(i)
        elif i == pairs[stack[-1]]:
            stack.pop()
        else:
            prize += prizes[i]   
            break

print(prize)


# part two

prizes = {"(": 1,"[": 2,"{": 3,"<": 4}
stack = []
results = []

def prize(missing):
    score = 0
    missing.reverse()
    for i in missing:
        score *= 5
        score += prizes[i]
    return score

for line in lines:
    next = False
    stack = []
    for i in line:
        if i in pairs.keys():
            stack.append(i)
        elif i == pairs[stack[-1]]:
            stack.pop()
        else:
            next = True
            break
    if next:
        continue
    if len(stack) > 0:
        results.append(prize(stack))

print(statistics.median(results))