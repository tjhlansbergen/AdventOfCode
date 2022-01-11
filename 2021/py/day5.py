import helpers

def dict_add_or_inc(dct, key):
    if key in dct:
        dct[key] += 1
    else:
        dct[key] = 1

lines = helpers.list_from_file('2021/inputs/day5')
segments = [[[int(coord) for coord in point.split(",")] for point in line.split(" -> ")] for line in lines]


# part one

grid = {}

for segment in segments:
    xs = segment[0][0], segment[1][0]
    ys = segment[0][1], segment[1][1]
    
    # x==x (vertical)
    if xs[0] == xs[1]:
        for i in range(min(ys), max(ys)+1):
            dict_add_or_inc(grid, (xs[0],i))
            
    # y==y (horizontal)
    if ys[0] == ys[1]:
        for i in range(min(xs), max(xs)+1):
            dict_add_or_inc(grid, (i,ys[0]))
    
print(len([overlap for overlap in list(grid.values()) if overlap > 1]))


# part two

grid = {}

for segment in segments:
    xs = segment[0][0], segment[1][0]
    ys = segment[0][1], segment[1][1]
    
    # x==x (vertical)
    if xs[0] == xs[1]:
        for i in range(min(ys), max(ys)+1):
            dict_add_or_inc(grid, (xs[0],i))
            
    # y==y (horizontal)
    if ys[0] == ys[1]:
        for i in range(min(xs), max(xs)+1):
            dict_add_or_inc(grid, (i,ys[0]))

    # diagonal (45*)
    if abs(xs[0]-xs[1]) == abs(ys[0]-ys[1]):            # determine wheter the angle is 45*
        x = xs[0]                                       # pick random starting point out of the two segment ends
        y = ys[0]
        for _ in range(abs(xs[0]-xs[1])+1):             # segment length, inclusive       
            dict_add_or_inc(grid, (x,y))
            x += 1 if xs[0] < xs[1] else -1             # left/right depending on starting point
            y += 1 if ys[0] < ys[1] else -1             # up/down depending on starting point

print(len([overlap for overlap in list(grid.values()) if overlap > 1]))