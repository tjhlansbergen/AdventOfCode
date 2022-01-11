import helpers
import math

lines = helpers.list_from_file('2020/inputs/day3')

# python strings are indexable, that makes traversing the map easy

def one() -> int:
    rows = len(lines)
    columns = len(lines[0])
    x = y = 0
    route = ''

    while y < rows:
        route += lines[y][x]
        x += 3
        y += 1
        if x >= columns:
            x -= columns

    return route.count('#')

def two():

    def trees_in_route(right: int, down: int):
        rows = len(lines)
        columns = len(lines[0])
        x = y = 0
        route = ''

        while y < rows:
            route += lines[y][x]
            x += right
            y += down
            if x >= columns:
                x -= columns

        result = route.count('#')
        #print(f"r{right}, d{down}: {result}")
        return result

    results = [trees_in_route(r, d) for (r, d) in [(1, 1), (3, 1), (5,1), (7,1), (1,2)]]
    return math.prod(results)

print(f"one: {one()}")
print(f"two: {two()}")