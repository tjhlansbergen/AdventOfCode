import helpers

blocks = helpers.split_file_on_blanklines('inputs/day13')

paper = [[int(line.split(',')[0]), int(line.split(',')[1])] for line in blocks[0].split('\n')]
folds = [(line.replace('fold along ', '').split('=')[0], int(line.replace('fold along ', '').split('=')[1])) for line in blocks[1].split('\n')]

def fold(paper, along, by):
    i = 0 if along == 'x' else 1

    for dot in paper:
        if dot[i] > by:
            dot[i] =  dot[i] - ((dot[i] - by) * 2)

    # to merge the dots transform them to tuples so the list can be hashed en turned into a set, which gives the distinct dots, and then back to list_of_list
    return [list(dot) for dot in set([tuple(unmerged_dot) for unmerged_dot in paper])]



# part one
folded = fold(paper, folds[0][0], folds[0][1])
print(len(folded))


# part two

def print_paper(paper):
    print('\n', end='')

    max_x = max([dot[0] for dot in paper]) + 1
    max_y = max([dot[1] for dot in paper]) + 1

    for y in range(max_y):
        for x in range(max_x):
            if [x, y] in paper:
                print('#', end='')
            else:
                print(' ', end='')
        print('\n', end='')

for folder in folds:
    paper = fold(paper, folder[0], folder[1])

print_paper(paper)




