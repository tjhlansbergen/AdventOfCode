import helpers
import statistics
import math

positions = helpers.split_int_file_on_comma('2021/inputs/day7')
positions.sort()

def fuel(positions, test):
    return sum([abs(test-position) for position in positions])

costs = {}
for i, position in enumerate(positions):
    cost = fuel(positions, position)
    costs[position] = cost

# part one
print(min(costs.values()), "(or ", fuel(positions, statistics.median(positions)), ")")  
# could start closer to the middle of the sorted posistions and stop looping when the price start to rise again
# i'm not sure if median will always work? 

# part two
def triangular(x: int) -> int:
    return int((x*(x+1))/2)

def fuel_2(positions, test):
    return sum([triangular(abs(test-position)) for position in positions])

costs = {}
for i in range(max(positions)):
    cost = fuel_2(positions, i)
    costs[i] = cost

print(min(costs.values())) 

