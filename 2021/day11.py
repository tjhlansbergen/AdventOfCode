import helpers

lines = helpers.list_from_file('inputs/day11')
grid = [[int(x) for x in line] for line in lines]
count = 0
size = len(grid) * len(grid[0])

def increment_grid(grd):
    return [[column + 1  for column in row] for row in grid]

def increment(x,y, grd):
    if x < 0 or y < 0:
        return
    if x >= len(grd[0]) or y >= len(grd):
        return

    if grd[x][y] > 0:
        grd[x][y] += 1
        if grd[x][y] > 9:
            flash(x,y,grd)

def flash(x, y, grd):
    global count
    count += 1
    grd[x][y] = 0

    increment(x-1,y-1,grd)
    increment(x,y-1,grd)
    increment(x+1,y-1,grd)
    increment(x+1,y,grd)
    increment(x+1,y+1,grd)
    increment(x,y+1,grd)
    increment(x-1,y+1,grd)
    increment(x-1,y,grd)

def flash_grid(grd):
    for y in range(len(grd)):
        for x in range(len(grd[0])):
            if grd[x][y] > 9:
                flash(x,y,grd)
    return grd

def step(grd):
    # increment all by 1
    grd = increment_grid(grd)

    # flash
    grd = flash_grid(grd)

    return grd


# part one

for i in range(100):
    grid = step(grid)    

print(count)

# part two

# reset
grid = [[int(x) for x in line] for line in lines]
count = 0
stp = 1

while(True):
    grid = step(grid)    
    
    if count == size:
        print(stp)
        break

    count = 0
    stp += 1
