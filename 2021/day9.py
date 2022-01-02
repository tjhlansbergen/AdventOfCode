import helpers
import math

# read input and covert to int's on the go
grid = [[int(c) for c in r] for r in helpers.list_from_file('inputs/day9')]  

width = len(grid[0])
height = len(grid)


# part one

results = []

for r in range(height):
    for c in range(width):        
        if r-1 > -1:
            if grid[r][c] >= grid[r-1][c]:
                continue
        if r+1 < height:
            if grid[r][c] >= grid[r+1][c]:
                continue
        if c-1 > -1:
            if grid[r][c] >= grid[r][c-1]:
                continue
        if c+1 < width:
            if grid[r][c] >= grid[r][c+1]:
                continue

        results.append(grid[r][c] + 1)    

print(sum(results))



# part two

lows = set()

# get low points (like above but this time get their position)
for r in range(height):
    for c in range(width):        
        if r-1 > -1:
            if grid[r][c] >= grid[r-1][c]:
                continue
        if r+1 < height:
            if grid[r][c] >= grid[r+1][c]:
                continue
        if c-1 > -1:
            if grid[r][c] >= grid[r][c-1]:
                continue
        if c+1 < width:
            if grid[r][c] >= grid[r][c+1]:
                continue

        lows.add((r,c))   


# for each low, recursively scan around to get basin
basin_members = set()

def visit(r, c):

    basin_members.add((r,c))

    if r-1 > -1 and grid[r-1][c] != 9 and (r-1,c) not in basin_members:
        visit(r-1, c)        
    if r+1 < height and grid[r+1][c] != 9 and (r+1,c) not in basin_members:
        visit(r+1, c)
    if c-1 > -1 and grid[r][c-1] != 9 and (r,c-1) not in basin_members:
        visit(r, c-1)
    if c+1 < width and grid[r][c+1] != 9 and (r,c+1) not in basin_members:
        visit(r, c+1)

results = []

for low in lows:
    basin_members = set()
    visit(low[0], low[1])
    results.append(len(basin_members))

print(math.prod(sorted(results, reverse=True)[0:3]))
