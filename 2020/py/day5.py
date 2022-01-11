from typing import Iterable, Iterator
import helpers

def partition(size: int, route: str) -> int:
    
    route = route.replace('R', 'B')
    start = 0

    for r in route:
        size = size / 2
        
        if r == 'B':
            start += size
    
    return start

def find_seat(route: str) -> int:
    row = partition(128, route[0:7])
    col = partition(8, route[7:10])

    seat = (row*8)+col
    return int(seat)

# part one
routes = helpers.list_from_file('2020/inputs/day5')
seats = [find_seat(route) for route in routes]
print(max(seats))

# part two
missing = set(range(min(seats), max(seats))) - set(seats)
{print(x) for x in missing}

# seats.sort()
# for i in range(len(seats)-1):
#     if seats[i]+1 != seats[i+1]:
#         print(seats[i]+1)
#         break


