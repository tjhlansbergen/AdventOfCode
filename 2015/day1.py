import helpers

lines = helpers.list_from_file('2015/inputs/day1')

# part one
for line in lines:
    print(line.count('(') - line.count(')'))

# part two
for line in lines:
    floor = 0
    index = 0

    for i in line:
        index += 1
        floor += 1 if i == "(" else -1
        if floor == -1:
            print(index)
            exit()
    
