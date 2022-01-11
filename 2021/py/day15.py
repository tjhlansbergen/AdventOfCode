import helpers

lines = helpers.list_from_file('2021/inputs/day15test')
grid = [[int(column) for column in row] for row in lines]

# to (tuple(r,c))/length(int)
scores = {}

for row in range(len(grid)):
    for col in range(len(grid[0])):
        scores[(row, col)] = float('inf')

scores[(0,0)] = 0
visited = []

def adjecent(point):
    left = (point[0],point[1]-1)
    right = (point[0],point[1]+1)
    top = (point[0]-1,point[1])
    bottom = (point[0]+1,point[1])
    return [left, right, top, bottom]

def print_scores(grid, scores):
    for row in range(len(grid)):
        for col in range(len(grid[0])):
            score = scores[(row, col)]
            if score == float('inf'):
                print(' .. ', end='')
            else:
                print(f' {score:02} ', end='')
        print('\n')

def visit(point):
    
    #print(point)
    #print_scores(grid, scores)

    visited.append(point)
    my_score = scores[point]

    # calculate adjecent scores
    for a in adjecent(point):
        if a in scores:     # in bounds?
            new_score = my_score + grid[a[0]][a[1]]
            if new_score < scores[a]:
                scores[a] = new_score
                if a in visited:
                    visited.remove(a)

    # and move to smallest not visited a
    [visit(a) for a in adjecent(point) if a in scores and a not in visited]
    

visit((0,0))
print(scores[(9,9)])

