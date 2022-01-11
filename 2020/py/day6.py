import helpers
from typing import List
from typing import DefaultDict

groups = helpers.split_file_on_blanklines('2020/inputs/day6')

# part one
sum_of_counts = sum([len(set(group.replace('\n',''))) for group in groups])
print(sum_of_counts)

#part two
def count_answers(group: str) -> int:
    
    group_size = len(group.split("\n"))
    
    counts = {}
    for a in group.replace("\n", ""):
        if a not in counts:
            counts[a] = 1
        else:
            counts[a] += 1
    #print(counts, group_size)

    return len([val for val in counts.values() if val == group_size])

group_counts = [count_answers(group) for group in groups]
print(sum(group_counts))